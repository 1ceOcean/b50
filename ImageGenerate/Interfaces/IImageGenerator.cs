using MaimaiBasic.Classes.Song;
using System.Collections.Frozen;

namespace ImageGenerate;

public interface IImageGenerator
{
    Stream GenerateImage(B50Info info, Dictionary<int, Stream> covers, in FrozenDictionary<int, SongInfo> songInfos);
}
