namespace b50.Interface;

public interface IGetCover
{
    Task<Stream> GetCoverAsStream(int id, CancellationToken token);
}
