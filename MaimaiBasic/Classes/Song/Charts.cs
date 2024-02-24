using System.Text.Json.Serialization;

namespace MaimaiBasic.Classes.Song;
public class Charts
{
	[JsonPropertyName("dx")]
	public List<Song>? Dx { get; set; }
	
	[JsonPropertyName("sd")]
	public List<Song>? Sd { get; set; }
}