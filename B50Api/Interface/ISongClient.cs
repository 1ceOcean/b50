using B50;

namespace B50Api.Interface;

public interface ISongClient
{
    HttpClient Client { get; }

    ApiSetting ApiSetting { get; }

    Task<Stream> GetUserB50Async(UseQQ value, CancellationToken token);

    Task<Stream> GetUserB50Async(UseUserName value, CancellationToken token);

    Task<Stream> GetCover(int coverId, CancellationToken token);

    Task<Stream> GetSongList(CancellationToken token);
}
