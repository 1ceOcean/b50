
using System.Text.Json.Serialization;

namespace MaimaiBasic.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SongType
{
	DX,
	SD
}