using DevViewer.Components;
using Microsoft.AspNetCore.Builder;
using System.Collections.Concurrent;
using System.Text.Json;
using System.Text;

[assembly: System.Reflection.Metadata.MetadataUpdateHandler(typeof(HotReloadManager))]




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddHttpClient();
builder.Services.AddServerSideBlazor();
var app = builder.Build();




// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStaticFiles();

app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

var client = new HttpClient();

app.MapGet("/b50img.png", async () =>
{
    var request = new HttpRequestMessage(HttpMethod.Post,
          "http://localhost:5141/api/B50/ScoreImageQQ");
    var payload = new { qq = "77180082", b50 = true };
    var payloadContent = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

    request.Content = payloadContent;

    var response = await client.SendAsync(request);

    return Results.File(await response.Content.ReadAsByteArrayAsync(), "image/png");
});

app.Run();




internal static class HotReloadManager
{
    public static void ClearCache(Type[]? types) 
    {
        PropertyCache._types.Clear();
    }
    public static void UpdateApplication(Type[]? types) 
    {
        PropertyCache.GetProperties<ImageGenerate.ImageGenerator>();
    }
}

static class PropertyCache
{
    internal static readonly ConcurrentDictionary<Type, string> _types = new();

    public static string GetProperties<T>()
        => _types.GetOrAdd(typeof(T),
            type => string.Join(",", type.GetProperties().Select(p => p.Name)));
}

