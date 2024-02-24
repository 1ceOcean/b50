using MaimaiBasic.Utils;
using System.Runtime.Serialization;

namespace MaimaiBasic.Enums;

[System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverterEx<Rate>))]
public enum Rate
{
    [EnumMember(Value = "d")]
    D = 0,
    [EnumMember(Value = "c")]
    C,
    [EnumMember(Value = "b")]
    B,
    [EnumMember(Value = "bb")]
    BB,
    [EnumMember(Value = "bbb")]
    BBB,
    [EnumMember(Value = "a")]
    A,
    [EnumMember(Value = "aa")]
    AA,
    [EnumMember(Value = "aaa")]
    AAA,
    [EnumMember(Value = "s")]
    S,
    [EnumMember(Value = "sp")]
    SPlus,
    [EnumMember(Value = "ss")]
    SS,
    [EnumMember(Value = "ssp")]
    SSPlus,
    [EnumMember(Value = "sss")]
    SSS,
    [EnumMember(Value = "sssp")]
    SSSPlus,
}