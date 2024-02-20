using SkiaSharp;
using System.Collections.Frozen;

namespace ImageGenerate.Utils;

internal static class SongAssets
{
    private static FrozenDictionary<string, SKBitmap> _imgDict;
    private static FrozenDictionary<string, SKTypeface> _fontDict;

    public static string RootPath
    {
        get => @"E:\Projects\b50\ImageGenerate\assets";
    }

    public static SKBitmap DefaultSongCover { get => _imgDict[nameof(DefaultSongCover)]; }
    public static SKBitmap BackgroundImg { get => _imgDict[nameof(BackgroundImg)]; }

    public static SKBitmap LevelLabelNone { get => _imgDict["LevelLabelNone"]; }
    public static SKBitmap LevelLabelBasic { get => _imgDict["LevelLabel0"]; }
    public static SKBitmap LevelLabelAdvanced { get => _imgDict["LevelLabel1"]; }
    public static SKBitmap LevelLabelExpert { get => _imgDict["LevelLabel2"]; }
    public static SKBitmap LevelLabelMaster { get => _imgDict["LevelLabel3"]; }
    public static SKBitmap LevelLabelReMaster { get => _imgDict["LevelLabel4"]; }

    public static SKBitmap RateD { get => _imgDict["RateD"]; }
    public static SKBitmap RateC { get => _imgDict["RateC"]; }
    public static SKBitmap RateB { get => _imgDict["RateB"]; }
    // sadly we not have bb and bbb rate.
    //public static SKBitmap RateBB { get => _imgDict["RateBB"]; }
    //public static SKBitmap RateBBB { get => _imgDict["RateBBB"]; }

    public static SKBitmap RateBB { get => RateB; }
    public static SKBitmap RateBBB { get => RateB; }
    public static SKBitmap RateA { get => _imgDict["RateA"]; }
    public static SKBitmap RateAA { get => _imgDict["RateAA"]; }
    public static SKBitmap RateAAA { get => _imgDict["RateAAA"]; }
    public static SKBitmap RateS { get => _imgDict["RateS"]; }
    public static SKBitmap RateSPlus { get => _imgDict["RateSPlus"]; }
    public static SKBitmap RateSS { get => _imgDict["RateSS"]; }
    public static SKBitmap RateSSPlus { get => _imgDict["RateSSPlus"]; }
    public static SKBitmap RateSSS { get => _imgDict["RateSSS"]; }
    public static SKBitmap RateSSSPlus { get => _imgDict["RateSSSPlus"]; }

    public static SKBitmap DXStar1 { get => _imgDict["DxStar1"]; }
    public static SKBitmap DXStar2 { get => _imgDict["DxStar2"]; }
    public static SKBitmap DXStar3 { get => _imgDict["DxStar3"]; }
    public static SKBitmap DXStar4 { get => _imgDict["DxStar4"]; }
    public static SKBitmap DXStar5 { get => _imgDict["DxStar5"]; }

    public static SKBitmap DX { get => _imgDict["DX"]; }

    public static SKBitmap AP { get => _imgDict["AP"]; }
    public static SKBitmap APPlus { get => _imgDict["APPlus"]; }
    public static SKBitmap FC { get => _imgDict["FC"]; }
    public static SKBitmap FCPlus { get => _imgDict["FCPlus"]; }
    public static SKBitmap FDX { get => _imgDict["FDX"]; }
    public static SKBitmap FDXPlus { get => _imgDict["FDXPlus"]; }
    public static SKBitmap FS { get => _imgDict["FS"]; }
    public static SKBitmap FSPlus { get => _imgDict["FSPlus"]; }
    public static SKBitmap NoneSpecialRate { get => _imgDict["NoneSpecialRate"]; }
    public static SKBitmap RecommandDS { get => _imgDict["RecommandDs"]; }

    public static SKBitmap RAT15000 { get => _imgDict["RAT15000"]; }
    public static SKBitmap RAT14500 { get => _imgDict["RAT14500"]; }
    public static SKBitmap RAT14000 { get => _imgDict["RAT14000"]; }
    public static SKBitmap RAT13000 { get => _imgDict["RAT13000"]; }

    public static SKBitmap Title15000 { get => _imgDict["Title15000"]; }
    public static SKBitmap Title14000 { get => _imgDict["Title14000"]; }
    public static SKBitmap Title13000 { get => _imgDict["Title13000"]; }

    public static SKBitmap DefaultNameFramework { get => _imgDict["DefaultNameFramework"]; }

    public static SKBitmap DefaultBackBoard { get => _imgDict["DefaultBackBoard"]; }
    public static SKBitmap DefaultAvatar { get => _imgDict["DefaultAvatar"]; }

    public static SKTypeface JangCheng400W { get => _fontDict["400W"]; }

    public static SKTypeface JangCheng500W { get => _fontDict["500W"]; }
    public static SKTypeface JangCheng600W { get => _fontDict["600W"]; }
    public static SKTypeface JangCheng700W { get => _fontDict["700W"]; }
    public static SKTypeface RoGSanSrfStd_Bd { get => _fontDict["RoGSanSrfStd_Bd"]; }

    static SongAssets()
    {
        Console.WriteLine(RootPath);
        var imgDict = new Dictionary<string, SKBitmap>();
        var fontDict = new Dictionary<string, SKTypeface>();
        imgDict.Add("DefaultSongCover", SKBitmap.Decode($"{RootPath}/images/00000.png"));
        imgDict.Add("BackgroundImg", SKBitmap.Decode($"{RootPath}/images/背景.png"));

        imgDict.Add("DX", SKBitmap.Decode($"{RootPath}/images/版本/DX版本.png"));

        imgDict.Add("LevelLabelNone", SKBitmap.Decode($"{RootPath}/images/框/框-无.png"));
        imgDict.Add("LevelLabel0", SKBitmap.Decode($"{RootPath}/images/框/框-绿.png"));
        imgDict.Add("LevelLabel1", SKBitmap.Decode($"{RootPath}/images/框/框-黄.png"));
        imgDict.Add("LevelLabel2", SKBitmap.Decode($"{RootPath}/images/框/框-红.png"));
        imgDict.Add("LevelLabel3", SKBitmap.Decode($"{RootPath}/images/框/框-紫.png"));
        imgDict.Add("LevelLabel4", SKBitmap.Decode($"{RootPath}/images/框/框-白.png"));

        imgDict.Add("RateD", SKBitmap.Decode($"{RootPath}/images/评级/D.png"));
        imgDict.Add("RateC", SKBitmap.Decode($"{RootPath}/images/评级/C.png"));
        imgDict.Add("RateB", SKBitmap.Decode($"{RootPath}/images/评级/B.png"));
        //imgDict.Add("RateBB", SKBitmap.Decode($"{RootPath}/images/评级/BB.png"));
        //imgDict.Add("RateBBB", SKBitmap.Decode($"{RootPath}/images/评级/BBB.png"));
        imgDict.Add("RateA", SKBitmap.Decode($"{RootPath}/images/评级/A.png"));
        imgDict.Add("RateAA", SKBitmap.Decode($"{RootPath}/images/评级/AA.png"));
        imgDict.Add("RateAAA", SKBitmap.Decode($"{RootPath}/images/评级/AAA.png"));
        imgDict.Add("RateS", SKBitmap.Decode($"{RootPath}/images/评级/S.png"));
        imgDict.Add("RateSPlus", SKBitmap.Decode($"{RootPath}/images/评级/S+.png"));
        imgDict.Add("RateSS", SKBitmap.Decode($"{RootPath}/images/评级/SS.png"));
        imgDict.Add("RateSSPlus", SKBitmap.Decode($"{RootPath}/images/评级/SS+.png"));
        imgDict.Add("RateSSS", SKBitmap.Decode($"{RootPath}/images/评级/SSS.png"));
        imgDict.Add("RateSSSPlus", SKBitmap.Decode($"{RootPath}/images/评级/SSS+.png"));

        imgDict.Add("DxStar1", SKBitmap.Decode($"{RootPath}/images/星级/1.png"));
        imgDict.Add("DxStar2", SKBitmap.Decode($"{RootPath}/images/星级/2.png"));
        imgDict.Add("DxStar3", SKBitmap.Decode($"{RootPath}/images/星级/3.png"));
        imgDict.Add("DxStar4", SKBitmap.Decode($"{RootPath}/images/星级/4.png"));
        imgDict.Add("DxStar5", SKBitmap.Decode($"{RootPath}/images/星级/5.png"));

        imgDict.Add("AP", SKBitmap.Decode($"{RootPath}/images/特殊标识/AP.png"));
        imgDict.Add("APPlus", SKBitmap.Decode($"{RootPath}/images/特殊标识/AP+.png"));
        imgDict.Add("FC", SKBitmap.Decode($"{RootPath}/images/特殊标识/FC.png"));
        imgDict.Add("FCPlus", SKBitmap.Decode($"{RootPath}/images/特殊标识/FC+.png"));
        imgDict.Add("FDX", SKBitmap.Decode($"{RootPath}/images/特殊标识/FDX.png"));
        imgDict.Add("FDXPlus", SKBitmap.Decode($"{RootPath}/images/特殊标识/FDX+.png"));
        imgDict.Add("FS", SKBitmap.Decode($"{RootPath}/images/特殊标识/FS.png"));
        imgDict.Add("FSPlus", SKBitmap.Decode($"{RootPath}/images/特殊标识/FS+.png"));
        imgDict.Add("NoneSpecialRate", SKBitmap.Decode($"{RootPath}/images/特殊标识/无.png"));

        imgDict.Add("DefaultBackBoard", SKBitmap.Decode($"{RootPath}/images/背景板/300201.png"));

        imgDict.Add("RecommandDs", SKBitmap.Decode($"{RootPath}/images/推荐定数.png"));

        imgDict.Add("RAT15000", SKBitmap.Decode($"{RootPath}/images/RAT框/15000.png"));
        imgDict.Add("RAT14500", SKBitmap.Decode($"{RootPath}/images/RAT框/14500.png"));
        imgDict.Add("RAT14000", SKBitmap.Decode($"{RootPath}/images/RAT框/14000.png"));
        imgDict.Add("RAT13000", SKBitmap.Decode($"{RootPath}/images/RAT框/13000.png"));

        imgDict.Add("DefaultNameFramework", SKBitmap.Decode($"{RootPath}/images/wuwuwu.png"));
        imgDict.Add("DefaultAvatar", SKBitmap.Decode($"{RootPath}/images/avatar.png"));

        imgDict.Add("Title15000", SKBitmap.Decode($"{RootPath}/images/称号/15000.png"));
        imgDict.Add("Title14000", SKBitmap.Decode($"{RootPath}/images/称号/14000.png"));
        imgDict.Add("Title13000", SKBitmap.Decode($"{RootPath}/images/称号/13000.png"));


        fontDict.Add("400W", SKTypeface.FromFile($"{RootPath}/font/江城圆体 400W.ttf"));
        fontDict.Add("500W", SKTypeface.FromFile($"{RootPath}/font/江城圆体 500W.ttf"));
        fontDict.Add("600W", SKTypeface.FromFile($"{RootPath}/font/江城圆体 600W.ttf"));
        fontDict.Add("700W", SKTypeface.FromFile($"{RootPath}/font//江城圆体 700W.ttf"));
        fontDict.Add("RoGSanSrfStd_Bd", SKTypeface.FromFile($"{RootPath}/font/RoGSanSrfStd-Bd.otf"));

        _imgDict = imgDict.ToFrozenDictionary();
        _fontDict = fontDict.ToFrozenDictionary();
    }

}
