using MaimaiBasic.Enums;
using MaimaiBasic.Extensions;

using Rating = (MaimaiBasic.Enums.Rate rate, double ds);


namespace MaimaiBasic.Classes.B50;
public static class RatingTable 
{

    /// <summary>
    /// Only Contains greater than S score Rating.
    /// </summary>
    public static Rating?[,] SUpTable { get; private set; }

    static RatingTable()
    {
        
        SUpTable = GenerateSupTable();
    }

    public static Rating? GetGreaterThanRatingMinRating(Rate rate, int rating)
    {
        var index = rate switch
        {
            Rate.S => 5,
            Rate.SPlus => 4,
            Rate.SS => 3,
            Rate.SSPlus => 2,
            Rate.SSS => 1,
            Rate.SSSPlus => 0,
            _ => -1,
        };

        if (index < 0) return null;

        Rating? ret = SUpTable[index, rating];
        for (int i = rating + 1; i < 338; i++)
        {
            if (SUpTable[index, i] is not null) 
            {
                ret = SUpTable[index, i];
                break;
            }
        }

        return ret;
    }

    internal static Rating?[,] GenerateSupTable() 
    {
        var res = new (Rate, double)?[6, 338];

        var table = GenerateFullTable();
        for (int i = 0; i<table.Count; i++)
        {
            for (int j = RateExtensions.RateArray.Length - 1; j >= RateExtensions.RateArray.Length - 1 - 5; j--)
            {
                res[RateExtensions.RateArray.Length - 1 - j, table[i][RateExtensions.RateArray[j]]] = (RateExtensions.RateArray[j], table[i].Ds);
            }
        }

        return res;
    }

    internal static List<RatingLine> GenerateFullTable()
    {
        var list = new List<RatingLine>();

        for (int ds = 150; ds >= 10; ds--)
        {
            var rl = new RatingLine();

            for (int i = 0; i < RateExtensions.RateArray.Length; i++)
            {
                rl[RateExtensions.RateArray[i]] = RateExtensions.RateArray[i].ToAchievement().Rating(ds) / 10;
            }

            rl.Ds = ds / 10d;
            list.Add(rl);
        }

        return list;
    }
}

public class RatingLine
{
    public double Ds { get; set; }
    public int SSSPlus { get; set; }
    public int SSS { get; set; }
    public int SSPlus { get; set; }
    public int SS { get; set; }
    public int SPlus { get; set; }
    public int S { get; set; }
    public int AAA { get; set; }
    public int AA { get; set; }
    public int A { get; set; }
    public int BBB { get; set; }
    public int BB { get; set; }
    public int B { get; set; }
    public int C { get; set; }
    public int D { get; set; }

    public int this[Rate rate]
    {
        get => rate switch
        {
            Rate.D => this.D,
            Rate.C => this.C,
            Rate.B => this.B,
            Rate.BB => this.BB,
            Rate.BBB => this.BBB,
            Rate.A => this.A,
            Rate.AA => this.AA,
            Rate.AAA => this.AAA,
            Rate.S => this.S,
            Rate.SPlus => this.SPlus,
            Rate.SS => this.SS,
            Rate.SSPlus => this.SSPlus,
            Rate.SSS => this.SSS,
            Rate.SSSPlus => this.SSSPlus,
            _ => throw new Exception()
        };


        set
        {
            switch (rate)
            {
                case Rate.D:
                    this.D = value;
                    break;
                case Rate.C:
                    this.C = value;
                    break;
                case Rate.B:
                    this.B = value;
                    break;
                case Rate.BB:
                    this.BB = value;
                    break;
                case Rate.BBB:
                    this.BBB = value;
                    break;
                case Rate.A:
                    this.A = value;
                    break;
                case Rate.AA:
                    this.AA = value;
                    break;
                case Rate.AAA:
                    this.AAA = value;
                    break;
                case Rate.S:
                    this.S = value;
                    break;
                case Rate.SPlus:
                    this.SPlus = value;
                    break;
                case Rate.SS:
                    this.SS = value;
                    break;
                case Rate.SSPlus:
                    this.SSPlus = value;
                    break;
                case Rate.SSS:
                    this.SSS = value;
                    break;
                case Rate.SSSPlus:
                    this.SSSPlus = value;
                    break;
            }
        }
    }
}

