using MaimaiBasic.Enums;
using MaimaiBasic.Utils;
using System.Text.Json.Serialization;

namespace MaimaiBasic.Classes.Song;


[JsonConverter(typeof(SongInfoConverter))]
public class SongInfo
{

    public int Id { get; set; }
    public string? Title { get; set; }
    public SongType Type { get; set; }
    public SongCharts Charts { get; set; } = new();
    public string? Artist { get; set; }
    public string? Genre { get; set; }
    public int Bpm { get; set; }
    public DateTime? ReleaseDate { get; set; }
    public string? From { get; set; }
    public bool IsNew { get; set; }
}

public class SongCharts
{
    public SingleChart Basic { get; set; } = new();
    public SingleChart Advanced { get; set; } = new();
    public SingleChart Expert { get; set; } = new();
    public SingleChart Master { get; set; } = new();
    public SingleChart? ReMaster { get; set; }

    public SingleChart? this[int index] => index switch 
    {
        0 => Basic,
        1 => Advanced,
        2 => Expert,
        3 => Master,
        4 => ReMaster,
        _ => throw new AccessViolationException(),
    };
  
}

public class SingleChart
{
    public int Id { get; set; }
    public Note Note { get; set; }
    public double DS { get; set; }
    public string? Level { get; set; }
    public string? Artist { get; set; }

    public int MaxScore => 3 * (Note.Tap + Note.Hold + Note.Slide + (Note.Touch ?? 0) + Note.Break);
}

public struct Note
{
    public int Tap { get; set; }
    public int Hold { get; set; }
    public int Slide { get; set; }
    public int? Touch { get; set; }
    public int Break { get; set; }
}



