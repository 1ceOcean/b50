using MaimaiBasic.Enums;
using MaimaiBasic.Extensions;

using Rating = (MaimaiBasic.Enums.Rate rate, double ds);


namespace MaimaiBasic.Classes.B50;
public static class RatingTable 
{

    /// <summary>
    /// Only Contains greater than SS score Rating.
    /// </summary>
    public static Rating?[,] SSUpTable { get; private set; }

    static RatingTable()
    {
        SSUpTable = GenerateSSUpTable();
    }

    public static Rating? GetGreaterThanRatingMinRating(int rateIndex, int rating)
    {
        var index = rateIndex - 10;
        Rating? ret = SSUpTable[index, rating];
        for (int i = rating + 1; i < 338; i++)
        {
            if (SSUpTable[index, i] is not null) 
            {
                ret = SSUpTable[index, i];
                break;
            }
        }

        return ret;
    }

    internal static Rating?[,] GenerateSSUpTable() 
    {
        var result = new (Rate, double)?[4, 338];

        var offset = 10;

        var table = GenerateFullTable();
        for (int i = 0; i<table.Count; i++)
        {
            result[(int)Rate.SSSPlus - offset,table[i][(int)Rate.SSSPlus]] = (Rate.SSSPlus, table[i].Ds);
            result[(int)Rate.SSS - offset,table[i][(int)Rate.SSS]] = (Rate.SSS, table[i].Ds);
            result[(int)Rate.SSPlus - offset,table[i][(int)Rate.SSPlus]] = (Rate.SSPlus, table[i].Ds);
            result[(int)Rate.SS - offset,table[i][(int)Rate.SS]] = (Rate.SS, table[i].Ds);
        }

        return result;
    }

    internal static List<RatingLine> GenerateFullTable()
    {
        var list = new List<RatingLine>();

        for (int ds = 150; ds >= 10; ds--)
        {
            var rl = new RatingLine(ds);
            list.Add(rl);
        }

        return list;
    }
}

public class RatingLine
{
    public double Ds { get; set; }

    private Dictionary<Rate, int> _dict;

    public RatingLine(int ds) 
    {   
        _dict = Extension.RateArray.ToDictionary(rate => rate, rate => rate.ToAchievement().Rating(ds) / 10);
        Ds = ds / 10d;
    }

    public int this[int rateIndex] 
    {
        get => _dict[(Rate)rateIndex];
        set => _dict[(Rate)rateIndex] = value;
    }

    public int this[Rate rate]
    {
        get => _dict[rate];
        set => _dict[rate] = value;
    }
}

