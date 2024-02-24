using System.Net.Http;
using b50.Interface;
using B50;
using B50Api.Interface;

namespace b50.InterfaceImpl;

public class ImageServiceImpl : IImageService
{
    private readonly ApiSetting _apiSetting;
    private readonly ISongClient _client;

    private readonly HttpClient _httpclient;

    public ImageServiceImpl(ApiSetting apiSetting, ISongClient client, HttpClient httpClient)
    {
        _apiSetting = apiSetting;
        _client = client;
        _httpclient = httpClient;
    }
    public async Task<Stream> GetCoverAsStream(int id, CancellationToken token)
    {
        if (_apiSetting.UseLocalCover)
        {
            return File.OpenRead($"{_apiSetting.CoverLocalPath}/{id:D5}.png");
        }
        else
        {
            return await _client.GetCover(id, token);
        }
    }

    public async Task<Stream> GetQQAvatarAsStream(string qq, CancellationToken token)
    {
        if(string.IsNullOrEmpty(qq))
            return Stream.Null;
        var reponse = await _httpclient.GetAsync(_apiSetting.QQAvatarUrl?.Replace("<qq>",qq));
        if(!reponse.IsSuccessStatusCode)
            return Stream.Null;
        return await reponse.Content.ReadAsStreamAsync();
    }
}
