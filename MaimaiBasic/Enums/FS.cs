using MaimaiBasic.Utils;
using System.Runtime.Serialization;


namespace MaimaiBasic.Enums;

[System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverterEx<FS>))]
public enum FS
{
	[EnumMember(Value = "")]
	None,
	[EnumMember(Value = "fs")]
	FS,
	[EnumMember(Value = "fsp")]
	FSPlus,
	[EnumMember(Value = "fsd")]
	FDX,
	[EnumMember(Value = "fsdp")]
	FDXPlus,
}