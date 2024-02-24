using b50.Interface;
using b50.InterfaceImpl;
using b50.Services;
using B50;
using B50Api.Interface;
using ImageGenerate;
using MaimaiBasic.Utils;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);


var setting = builder.Configuration.GetSection("B50Setting:ApiSetting").Get<ApiSetting>()!;

builder.Services.AddSingleton(setting);

#if DEBUG
builder.Services.AddHttpClient<ISongClient, DummyClient>("DummyClient");
#else
builder.Services.AddHttpClient<ISongClient,DivingFishClient>("DivingFish", c =>
{
    c.BaseAddress = new Uri(setting.BaseAddress!);
    c.Timeout = TimeSpan.FromMilliseconds(setting.Timeout);
});
#endif

builder.Services.AddHealthChecks();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IGetCover, GetCoverImpl>();
builder.Services.AddScoped<IImageGenerator, ImageGenerator>();


builder.Services.AddSingleton<IFetchSongList, FetchSongListImpl>(p =>
{
    var jsonOpt = new JsonSerializerOptions();
    jsonOpt.Converters.Add(new SongInfoConverter());
    var res = new FetchSongListImpl(p.GetService<ISongClient>()!, p.GetService<ApiSetting>()!, jsonOpt);
    return res;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
//before run app, fetch song list ahead.
var songList = app.Services.GetService<IFetchSongList>();
await songList!.UpDateDict();


app.Run();

