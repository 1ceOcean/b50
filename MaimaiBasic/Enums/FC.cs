using MaimaiBasic.Utils;
using System.Runtime.Serialization;

namespace MaimaiBasic.Enums;

[System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverterEx<FC>))]
public enum FC
{	
	[EnumMember(Value = "")]
	None,
	[EnumMember(Value = "fc")]
	FC,
	[EnumMember(Value = "fcp")]
	FCPlus,
	[EnumMember(Value = "ap")]
	AP,
	[EnumMember(Value = "app")]
	APPlus,
}