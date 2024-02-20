using MaimaiBasic.Utils;
using System.Runtime.Serialization;


namespace MaimaiBasic.Enums;

[System.Text.Json.Serialization.JsonConverter(typeof(JsonStringEnumConverterEx<Rate>))]
public enum Rate
{
    [EnumMember(Value = "d")]
    D = 0,
    [EnumMember(Value = "c")]
    C = 80,
    [EnumMember(Value = "b")]
    B = 96,
    [EnumMember(Value = "bb")]
    BB = 112,
    [EnumMember(Value = "bbb")]
    BBB = 120,
    [EnumMember(Value = "a")]
    A = 136,
    [EnumMember(Value = "aa")]
    AA = 152,
    [EnumMember(Value = "aaa")]
    AAA = 168,
    [EnumMember(Value = "s")]
    S = 200,
    [EnumMember(Value = "sp")]
    SPlus = 203,
    [EnumMember(Value = "ss")]
    SS = 208,
    [EnumMember(Value = "ssp")]
    SSPlus = 211,
    [EnumMember(Value = "sss")]
    SSS = 216,
    [EnumMember(Value = "sssp")]
    SSSPlus = 224,
}