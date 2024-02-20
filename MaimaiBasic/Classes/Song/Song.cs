using MaimaiBasic.Enums;
using System.Text.Json.Serialization;

namespace MaimaiBasic.Classes.Song;

public class Song
{
    public int Id { get; set; }
    [JsonPropertyName("achievements")]
    public double Achievements { get; set; }

    [JsonPropertyName("ds")]
    public double DS { get; set; }

    [JsonPropertyName("dxScore")]
    public int DxScore { get; set; }

    [JsonPropertyName("fc")]
    public FC FC { get; set; }

    [JsonPropertyName("fs")]
    public FS FS { get; set; }

    [JsonPropertyName("level")]
    public string? Level { get; set; }

    [JsonPropertyName("level_index")]
    public int LevelIndex { get; set; }

    [JsonPropertyName("level_label")]
    public string? LevelLabel { get; set; }

    [JsonPropertyName("ra")]
    public int Ra { get; set; }

    [JsonPropertyName("rate")]
    public Rate Rate { get; set; }

    [JsonPropertyName("song_id")]
    public int SongId { get; set; }

    [JsonPropertyName("title")]
    public string? Title { get; set; }

    [JsonPropertyName("type")]
    public SongType Type { get; set; }
}
