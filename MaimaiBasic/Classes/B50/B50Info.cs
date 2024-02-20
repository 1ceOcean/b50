using MaimaiBasic.Classes.Song;
using System.Text.Json.Serialization;

namespace ImageGenerate;
public class B50Info 
{	
	[JsonPropertyName("additional_rating")]
	public int AddtionalRating { get; set; }
	[JsonPropertyName("charts")]
	public Charts? Charts {get; set;}

	[JsonPropertyName("nickname")]
	public string? NickName {get;set;}

	[JsonPropertyName("plate")]
	public string? Plate {get; set;}
	
	[JsonPropertyName("rating")]
	public int Rating {get; set;}

	[JsonPropertyName("user_general_data")]
	public object? UserGeneralData {get;set;}
	
	[JsonPropertyName("username")]
	public string? UserName {get;set;}
}