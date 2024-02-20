using MaimaiBasic.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaimaiBasic.Extensions;

public static class RateExtensions
{
    public static double ToAchievement(this Rate rate) => rate switch
    {
        Rate.D => 50.000d,
        Rate.C => 60.0000d,
        Rate.B => 70.0000d,
        Rate.BB => 75.0000d,
        Rate.BBB => 80.0000d,
        Rate.A => 90.0000d,
        Rate.AA => 94.0000d,
        Rate.AAA => 95.0000d,
        Rate.S => 97.0000d,
        Rate.SPlus => 98.0000d,
        Rate.SS => 99.0000d,
        Rate.SSPlus => 99.5000d,
        Rate.SSS => 100.0000d,
        Rate.SSSPlus => 100.5000d,
        _ => 0d
    };

    public static Rate ToRate(this double achievement) => achievement switch
    {
        < 0.0000d => throw new InvalidOperationException(),
        < 50.000d => Rate.D,
        < 60.0000d => Rate.C,
        < 70.0000d => Rate.B,
        < 75.0000d => Rate.BB,
        < 80.0000d => Rate.BBB,
        < 90.0000d => Rate.A,
        < 94.0000d => Rate.AA,
        < 97.0000d => Rate.AAA,
        < 98.0000d => Rate.S,
        < 99.0000d => Rate.SPlus,
        < 99.5000d => Rate.SS,
        < 100.0000d => Rate.SSPlus,
        < 100.5000d => Rate.SSS,
        > 101.0000d => throw new InvalidOperationException(),
        _ => Rate.SSSPlus,
    };

    public static int Rating(this double achievement, double ds)
    {
        int c = (int)Rate.SSSPlus;
        for (int i = 0; i < RateArray.Length - 1; i++)
        {
            if (ToRate(achievement) < RateArray[i + 1])
            {
                c = (int)RateArray[i];
                break;
            }
        }

        return (int)(c * ds * Math.Min(100.5, achievement) / 1000);
    }

    public readonly static Rate[] RateArray = Enum.GetValues<Rate>();
}
