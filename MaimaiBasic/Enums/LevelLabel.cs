using MaimaiBasic.Utils;
using System.Runtime.Serialization;


namespace MaimaiBasic.Enums;

[System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverterEx<LevelLabel>))]
public enum LevelLabel 
{
	[EnumMember(Value = "")]
	None = -1,
	Basic = 0,
	Advanced = 1,
	Expert = 2,
	Master = 3,
	[EnumMember(Value = "Re:MASTER")]
	ReMaster = 4
}