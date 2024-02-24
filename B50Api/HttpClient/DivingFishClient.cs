using B50Api.Interface;
using System.Reflection;
using System.Runtime.InteropServices;

namespace B50;

public class DivingFishClient : ISongClient
{
    public HttpClient Client { get; private set; }

    ApiSetting ISongClient.ApiSetting => _apiSetting;

    private readonly ApiSetting _apiSetting;

    private readonly MemoryStream _streamCover;

    public DivingFishClient(HttpClient client, IConfiguration configuration)
    {
        Client = client;

        _apiSetting = configuration.GetSection("B50Setting:ApiSetting").Get<ApiSetting>()!;

        var assembly = Assembly.GetExecutingAssembly();
        using var fileStreamcover = assembly.GetManifestResourceStream("B50Api.LocalFiles.00000.png");
        _streamCover = new MemoryStream();
        fileStreamcover!.CopyTo(_streamCover);
    }

    public async Task<Stream> GetUserB50Async(UseQQ value, CancellationToken token)
    {
        var respon = await Client.PostAsJsonAsync(_apiSetting.playerInfoPath, value, token);

        if(!respon.IsSuccessStatusCode) 
        {
            return Stream.Null;
        }

        return await respon.Content.ReadAsStreamAsync();
    }

    public async Task<Stream> GetUserB50Async(UseUserName value, CancellationToken token)
    {
        var respon = await Client.PostAsJsonAsync(_apiSetting.playerInfoPath, value, token);
        if (!respon.IsSuccessStatusCode)
        {
            return Stream.Null;
        }
        return await respon.Content.ReadAsStreamAsync();
    }

    public async Task<Stream> GetCover(int coverId, CancellationToken token)
    {   
        if(_apiSetting.UseLocalCover) 
        {
            try
            {
                var bytes = await File.ReadAllBytesAsync($"{_apiSetting.CoverLocalPath}/{_apiSetting.CoverPrefix}{coverId:D5}{_apiSetting.CoverSuffix}.png");
                var stream = new MemoryStream(bytes);
                return stream;
            }
            catch (FileNotFoundException)
            {   
                _streamCover.Position = 0;
                return _streamCover;
            }
        }

        var respon = await Client.GetAsync($"{_apiSetting.CoverUrl}/{coverId:D5}.png", token);
        return await respon.Content.ReadAsStreamAsync();
    }

    public async Task<Stream> GetSongList(CancellationToken token)
    {
        var respon = await Client.GetAsync(_apiSetting.SongListPath, token);
        return await respon.Content.ReadAsStreamAsync();
    }


}
