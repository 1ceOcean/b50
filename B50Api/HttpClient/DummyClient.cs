using System.Reflection;
using B50Api.Interface;

namespace B50;

public class DummyClient : ISongClient
{ 
    public HttpClient Client { get; private set; }

    public ApiSetting ApiSetting => _apiSetting;

    private readonly ApiSetting _apiSetting;

    private readonly MemoryStream _streamB50;
    private readonly MemoryStream _streamAllsong;
    private readonly MemoryStream _streamCover;

    public DummyClient(HttpClient client, IConfiguration configuration)
    {
        Client = client;

        _apiSetting = configuration.GetSection("B50Setting:ApiSetting").Get<ApiSetting>()!;

        var assembly = Assembly.GetEntryAssembly();

        using var fileStreamAllsong = assembly.GetManifestResourceStream("B50Api.LocalFiles.AllSongs.json");
        using var fileStreamb50 = assembly.GetManifestResourceStream("B50Api.LocalFiles.UserB50.json");
        using var fileStreamcover = assembly.GetManifestResourceStream("B50Api.LocalFiles.00000.png");


        // using var fileStreamb50 = new FileStream(@"E:\Projects\b50\B50Api\LocalFiles\UserB50.json", FileMode.Open);
        // using var fileStreamcover = new FileStream(@"E:\Projects\b50\ImageGenerate\assets\images\00000.png", FileMode.Open);
        // using var fileStreamAllsong = new FileStream(@"E:\Projects\b50\B50Api\LocalFiles\AllSongs.json", FileMode.Open);

        _streamB50 = new MemoryStream();
        _streamAllsong = new MemoryStream();
        _streamCover = new MemoryStream();
        fileStreamb50.CopyTo(_streamB50);
        fileStreamcover.CopyTo(_streamCover);
        fileStreamAllsong.CopyTo(_streamAllsong);
    }

    public Task<Stream> GetUserB50Async(UseQQ value, CancellationToken token)
    {
        _streamB50.Position = 0;
        return Task.FromResult<Stream>(_streamB50);
    }

    public Task<Stream> GetUserB50Async(UseUserName value, CancellationToken token)
    {
        return GetUserB50Async(new UseUserName("",true),CancellationToken.None);
    }

    public async Task<Stream> GetCover(int coverId, CancellationToken token)
    {
        if (_apiSetting.UseLocalCover)
        {
            try 
            {
                var bytes = await File.ReadAllBytesAsync($"{_apiSetting.CoverLocalPath}/{_apiSetting.CoverPrefix}{coverId:D5}{_apiSetting.CoverSuffix}.png");
                var stream = new MemoryStream(bytes);
                return stream;
            }
            catch(FileNotFoundException) 
            {
                _streamCover.Position = 0;
                return _streamCover;
            }
            
        }
        _streamCover.Position = 0;
        return _streamCover;
    }

    public Task<Stream> GetSongList(CancellationToken token)
    {
        _streamAllsong.Position = 0;
        return Task.FromResult<Stream>(_streamAllsong);
    }
}
