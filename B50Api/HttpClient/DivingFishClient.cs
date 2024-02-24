using B50Api.Interface;
using System.Runtime.InteropServices;

namespace B50;

public class DivingFishClient : ISongClient
{
    public HttpClient Client { get; private set; }

    ApiSetting ISongClient.ApiSetting => _apiSetting;

    private readonly ApiSetting _apiSetting;

    public DivingFishClient(HttpClient client, IConfiguration configuration)
    {
        Client = client;

        _apiSetting = configuration.GetSection("B50Setting:ApiSetting").Get<ApiSetting>()!;
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
                return Stream.Null;
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
