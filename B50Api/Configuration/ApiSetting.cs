namespace B50;

public class ApiSetting
{
    public string? BaseAddress { get; set; }
    public int Timeout { get; set; }
    public string? playerInfoPath { get; set; }
    public bool UseLocalCover { get; set; }
    public string? CoverUrl { get; set; }
    public string? CoverLocalPath { get; set; }

    public string? CoverPrefix { get; set; }
    public string? CoverSuffix { get; set; }

    public TimeOnly? UpdateSongListTime { get; set; }
    public string? SongListPath { get; set; }
}
