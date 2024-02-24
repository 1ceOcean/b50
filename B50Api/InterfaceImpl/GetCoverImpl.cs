using b50.Interface;
using B50;
using B50Api.Interface;

namespace b50.InterfaceImpl;

public class GetCoverImpl : IGetCover
{
    private readonly ApiSetting _apiSetting;
    private readonly ISongClient _client;

    public GetCoverImpl(ApiSetting apiSetting, ISongClient client)
    {
        _apiSetting = apiSetting;
        _client = client;
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
}
