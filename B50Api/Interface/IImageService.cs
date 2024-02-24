namespace b50.Interface;

public interface IImageService
{
    Task<Stream> GetCoverAsStream(int id, CancellationToken token);
    Task<Stream> GetQQAvatarAsStream(string qq, CancellationToken token);
}
