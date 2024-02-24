using MaimaiBasic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace MaimaiBasic.Extensions;

public static class Extension
{
    private static readonly Range[] _achievements = [00_0000..49_9999, 50_0000..59_9999,   60_0000..69_9999,  70_0000..74_9999,
                                                    75_0000..79_9999, 80_0000..89_9999,   90_0000..93_9999,  94_0000..94_9999,
                                                    95_0000..96_9999, 97_0000..97_9999,   98_0000..98_9999,  99_0000..99_4999,
                                                    99_5000..99_9999, 100_0000..100_4999, 100_5000..101_0000];

    private static readonly int[] _coeffs = [0, 80, 96, 112, 120, 136, 152, 168, 200, 203, 208, 211, 216, 224];
    public static int ToAchievement(this Rate rate) => _achievements[(int)rate + 1].Start.Value;

    public static Rate ToRate(this int achievement) => RateArray[Array.FindIndex(_achievements,range => range.InRange(achievement)) - 1];

    public static int Rating(this int achievement, double ds)
    {
        int c = _coeffs[(int)Rate.SSSPlus];
        for (int i = 0; i < RateArray.Length - 1; i++)
        {
            if (achievement.ToRate() < RateArray[i + 1])
            {
                c = _coeffs[i];
                break;
            }
        }

        return (int)(c * ds * Math.Min(100.5, achievement / 10000d) / 1000);
    }

    public static bool InRange(this Range range, int index) => (index >= range.Start.Value) && (index <= range.End.Value);

    public readonly static Rate[] RateArray = Enum.GetValues<Rate>();
}

