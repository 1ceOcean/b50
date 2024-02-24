using b50.Interface;
using b50.Services;
using B50Api.Interface;
using ImageGenerate;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.IO.Pipes;
using System.Text;
using System.Text.Json;

namespace B50;

[ApiController]
[Route("api/[controller]")]
public class B50Controller : ControllerBase
{
    private readonly ISongClient _client;
    private readonly IImageService _imageService;
    private readonly IImageGenerator _imageGenerator;
    private readonly IFetchSongList _songInfos;

    public B50Controller(ISongClient client, IImageService imageService, IImageGenerator imageGenerator, IFetchSongList songInfos)
    {
        _client = client;
        _imageService = imageService;
        _imageGenerator = imageGenerator;
        _songInfos = songInfos;
    }


    [HttpPost("ScoreImageQQ")]
    public async Task<IActionResult> ScoreImage([FromBody] UseQQ payload)
    {
        var stream = await _client.GetUserB50Async(payload, CancellationToken.None);

        if(stream.Length == 0) 
        {
            return BadRequest();
        }

        var result = JsonSerializer.Deserialize<B50Info>(stream);
        
        if(result is null)
            return NotFound($"qq: {payload.qq} not found b50.");

        using var qqAvatarStream = await _imageService.GetQQAvatarAsStream(payload.qq,CancellationToken.None);
        
        result.QQAvatar = qqAvatarStream;

        var dict = new Dictionary<int, Stream>();

        var tasks = new List<Task<Stream>>();
        var ids = new List<int>();

        foreach (var item in result?.Charts?.Dx!)
        {
            tasks.Add(_client.GetCover(item.SongId, CancellationToken.None));
            ids.Add(item.SongId);
        }
        foreach (var item in result?.Charts?.Sd!)
        {
            tasks.Add( _client.GetCover(item.SongId, CancellationToken.None));
            ids.Add(item.SongId);
        }

        var streams = await Task.WhenAll(tasks);

        for (int i = 0; i < streams.Length; i++)
        {
            dict.TryAdd(ids[i], streams[i]);
        }


        var s = _imageGenerator.GenerateImage(result!, dict, _songInfos.SongInfos);
        return File(s, "image/png");
    }

    [HttpPost("ScoreImageUsername")]
    public async Task<IActionResult> ScoreImage([FromBody] UseUserName payload)
    {
        var stream = await _client.GetUserB50Async(payload, CancellationToken.None);
        var result = JsonSerializer.Deserialize<B50Info>(stream);
        var dict = new Dictionary<int, Stream>();

        var tasks = new List<Task<Stream>>();
        var ids = new List<int>();

        foreach (var item in result.Charts.Dx)
        {

            tasks.Add(_client.GetCover(item.SongId, CancellationToken.None));
            ids.Add(item.SongId);

        }
        foreach (var item in result.Charts.Sd)
        {
            tasks.Add(_client.GetCover(item.SongId, CancellationToken.None));
            ids.Add(item.SongId);

        }

        var streams = await Task.WhenAll(tasks);

        for (int i = 0; i < streams.Length; i++)
        {
            dict.TryAdd(ids[i], streams[i]);
        }


        var s = _imageGenerator.GenerateImage(result!, dict, _songInfos.SongInfos);
        return File(s, "image/png");
    }
}