using B50;
using B50Api.Interface;
using MaimaiBasic.Classes.Song;
using System.Collections.Frozen;
using System.Text.Json;

namespace b50.Services
{
    public class FetchSongListImpl : IFetchSongList
    {

        public FrozenDictionary<int, SongInfo>? SongInfos { get; private set; }

        public FetchSongListImpl(ISongClient client, ApiSetting setting, JsonSerializerOptions opt)
        {
            _client = client;
            _setting = setting;
            _opt = opt;

            _timer = new Timer(async c => await UpDateDict(), null, (TimeSpan)(TimeSpan.FromDays(1) - DateTime.Now.TimeOfDay + _setting.UpdateSongListTime?.ToTimeSpan())!, TimeSpan.FromDays(1));
        }

        public async Task UpDateDict()
        {

            var stream = await _client.GetSongList(CancellationToken.None);
            var result = JsonSerializer.Deserialize<Dictionary<int, SongInfo>>(stream, _opt);
            SongInfos = result?.ToFrozenDictionary();

        }


        private ISongClient _client;
        private ApiSetting _setting;
        private Timer _timer;
        private JsonSerializerOptions _opt;
    }

    public interface IFetchSongList
    {
        FrozenDictionary<int, SongInfo>? SongInfos { get; }

        Task UpDateDict();

    }
}
