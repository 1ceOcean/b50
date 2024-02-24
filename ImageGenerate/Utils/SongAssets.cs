using SkiaSharp;
using System.Collections.Frozen;
using System.Reflection;

namespace ImageGenerate.Utils;

internal static class SongAssets
{
    private static FrozenDictionary<string, SKBitmap> _imgDict;
    private static FrozenDictionary<string, SKTypeface> _fontDict;

    public static SKBitmap DefaultSongCover { get => _imgDict["00000"]; }
    public static SKBitmap BackgroundImg { get => _imgDict["背景"]; }

    public static SKBitmap LevelLabelNone { get => _imgDict["框-无"]; }
    public static SKBitmap LevelLabelBasic { get => _imgDict["框-绿"]; }
    public static SKBitmap LevelLabelAdvanced { get => _imgDict["框-黄"]; }
    public static SKBitmap LevelLabelExpert { get => _imgDict["框-红"]; }
    public static SKBitmap LevelLabelMaster { get => _imgDict["框-紫"]; }
    public static SKBitmap LevelLabelReMaster { get => _imgDict["框-白"]; }

    public static SKBitmap RateD { get => _imgDict["D"]; }
    public static SKBitmap RateC { get => _imgDict["C"]; }
    public static SKBitmap RateB { get => _imgDict["B"]; }
    
    // sadly we not have bb and bbb rate.
    //public static SKBitmap RateBB { get => _imgDict["RateBB"]; }
    //public static SKBitmap RateBBB { get => _imgDict["RateBBB"]; }

    public static SKBitmap RateBB { get => RateB; }
    public static SKBitmap RateBBB { get => RateB; }
    public static SKBitmap RateA { get => _imgDict["A"]; }
    public static SKBitmap RateAA { get => _imgDict["AA"]; }
    public static SKBitmap RateAAA { get => _imgDict["AAA"]; }
    public static SKBitmap RateS { get => _imgDict["S"]; }
    public static SKBitmap RateSPlus { get => _imgDict["S+"]; }
    public static SKBitmap RateSS { get => _imgDict["SS"]; }
    public static SKBitmap RateSSPlus { get => _imgDict["SS+"]; }
    public static SKBitmap RateSSS { get => _imgDict["SSS"]; }
    public static SKBitmap RateSSSPlus { get => _imgDict["SSS+"]; }

    public static SKBitmap DXStar1 { get => _imgDict["1"]; }
    public static SKBitmap DXStar2 { get => _imgDict["2"]; }
    public static SKBitmap DXStar3 { get => _imgDict["3"]; }
    public static SKBitmap DXStar4 { get => _imgDict["4"]; }
    public static SKBitmap DXStar5 { get => _imgDict["5"]; }

    public static SKBitmap DX { get => _imgDict["DX版本"]; }

    public static SKBitmap AP { get => _imgDict["AP"]; }
    public static SKBitmap APPlus { get => _imgDict["AP+"]; }
    public static SKBitmap FC { get => _imgDict["FC"]; }
    public static SKBitmap FCPlus { get => _imgDict["FC+"]; }
    public static SKBitmap FDX { get => _imgDict["FDX"]; }
    public static SKBitmap FDXPlus { get => _imgDict["FDX+"]; }
    public static SKBitmap FS { get => _imgDict["FS"]; }
    public static SKBitmap FSPlus { get => _imgDict["FS+"]; }
    public static SKBitmap NoneSpecialRate { get => _imgDict["无"]; }
    public static SKBitmap RecommandDS { get => _imgDict["推荐定数"]; }

    public static SKBitmap RAT15000 { get => _imgDict["15000"]; }
    public static SKBitmap RAT14500 { get => _imgDict["14500"]; }
    public static SKBitmap RAT14000 { get => _imgDict["14000"]; }
    public static SKBitmap RAT13000 { get => _imgDict["13000"]; }

    public static SKBitmap Title15000 { get => _imgDict["title_15000"]; }
    public static SKBitmap Title14000 { get => _imgDict["title_14000"]; }
    public static SKBitmap Title13000 { get => _imgDict["title_13000"]; }

    public static SKBitmap DefaultNameFramework { get => _imgDict["wuwuwu"]; }

    public static SKBitmap DefaultBackBoard { get => _imgDict["300201"]; }
    public static SKBitmap DefaultAvatar { get => _imgDict["avatar"]; }

    public static SKTypeface JangCheng400W { get => _fontDict["江城圆体 400W"]; }

    public static SKTypeface JangCheng500W { get => _fontDict["江城圆体 500W"]; }
    public static SKTypeface JangCheng600W { get => _fontDict["江城圆体 600W"]; }
    public static SKTypeface JangCheng700W { get => _fontDict["江城圆体 700W"]; }
    public static SKTypeface RoGSanSrfStd_Bd { get => _fontDict["RoGSanSrfStd-Bd"]; }

    static SongAssets()
    {   
        var imgDict = new Dictionary<string, SKBitmap>();
        var fontDict = new Dictionary<string, SKTypeface>();

        var assembly = Assembly.GetExecutingAssembly();
        var strs = assembly.GetManifestResourceNames();

        foreach (var name in strs)
        {
            using var stream = assembly.GetManifestResourceStream(name);
            var splited = name.Split('.');
            if(splited[^1] == "png")
                imgDict.TryAdd(splited[^2], SKBitmap.Decode(stream));
            else if(splited[^1] is "otf" or "ttf")
                fontDict.TryAdd(splited[^2], SKTypeface.FromStream(stream));
        }

        // imgDict.Add("DefaultSongCover", SKBitmap.Decode($"{RootPath}/images/00000.png"));
        // imgDict.Add("BackgroundImg", SKBitmap.Decode($"{RootPath}/images/背景.png"));

        // imgDict.Add("DX", SKBitmap.Decode($"{RootPath}/images/版本/DX版本.png"));

        // imgDict.Add("LevelLabelNone", SKBitmap.Decode($"{RootPath}/images/框/框-无.png"));
        // imgDict.Add("LevelLabel0", SKBitmap.Decode($"{RootPath}/images/框/框-绿.png"));
        // imgDict.Add("LevelLabel1", SKBitmap.Decode($"{RootPath}/images/框/框-黄.png"));
        // imgDict.Add("LevelLabel2", SKBitmap.Decode($"{RootPath}/images/框/框-红.png"));
        // imgDict.Add("LevelLabel3", SKBitmap.Decode($"{RootPath}/images/框/框-紫.png"));
        // imgDict.Add("LevelLabel4", SKBitmap.Decode($"{RootPath}/images/框/框-白.png"));

        // imgDict.Add("RateD", SKBitmap.Decode($"{RootPath}/images/评级/D.png"));
        // imgDict.Add("RateC", SKBitmap.Decode($"{RootPath}/images/评级/C.png"));
        // imgDict.Add("RateB", SKBitmap.Decode($"{RootPath}/images/评级/B.png"));
        // //imgDict.Add("RateBB", SKBitmap.Decode($"{RootPath}/images/评级/BB.png"));
        // //imgDict.Add("RateBBB", SKBitmap.Decode($"{RootPath}/images/评级/BBB.png"));
        // imgDict.Add("RateA", SKBitmap.Decode($"{RootPath}/images/评级/A.png"));
        // imgDict.Add("RateAA", SKBitmap.Decode($"{RootPath}/images/评级/AA.png"));
        // imgDict.Add("RateAAA", SKBitmap.Decode($"{RootPath}/images/评级/AAA.png"));
        // imgDict.Add("RateS", SKBitmap.Decode($"{RootPath}/images/评级/S.png"));
        // imgDict.Add("RateSPlus", SKBitmap.Decode($"{RootPath}/images/评级/S+.png"));
        // imgDict.Add("RateSS", SKBitmap.Decode($"{RootPath}/images/评级/SS.png"));
        // imgDict.Add("RateSSPlus", SKBitmap.Decode($"{RootPath}/images/评级/SS+.png"));
        // imgDict.Add("RateSSS", SKBitmap.Decode($"{RootPath}/images/评级/SSS.png"));
        // imgDict.Add("RateSSSPlus", SKBitmap.Decode($"{RootPath}/images/评级/SSS+.png"));

        // imgDict.Add("DxStar1", SKBitmap.Decode($"{RootPath}/images/星级/1.png"));
        // imgDict.Add("DxStar2", SKBitmap.Decode($"{RootPath}/images/星级/2.png"));
        // imgDict.Add("DxStar3", SKBitmap.Decode($"{RootPath}/images/星级/3.png"));
        // imgDict.Add("DxStar4", SKBitmap.Decode($"{RootPath}/images/星级/4.png"));
        // imgDict.Add("DxStar5", SKBitmap.Decode($"{RootPath}/images/星级/5.png"));

        // imgDict.Add("AP", SKBitmap.Decode($"{RootPath}/images/特殊标识/AP.png"));
        // imgDict.Add("APPlus", SKBitmap.Decode($"{RootPath}/images/特殊标识/AP+.png"));
        // imgDict.Add("FC", SKBitmap.Decode($"{RootPath}/images/特殊标识/FC.png"));
        // imgDict.Add("FCPlus", SKBitmap.Decode($"{RootPath}/images/特殊标识/FC+.png"));
        // imgDict.Add("FDX", SKBitmap.Decode($"{RootPath}/images/特殊标识/FDX.png"));
        // imgDict.Add("FDXPlus", SKBitmap.Decode($"{RootPath}/images/特殊标识/FDX+.png"));
        // imgDict.Add("FS", SKBitmap.Decode($"{RootPath}/images/特殊标识/FS.png"));
        // imgDict.Add("FSPlus", SKBitmap.Decode($"{RootPath}/images/特殊标识/FS+.png"));
        // imgDict.Add("NoneSpecialRate", SKBitmap.Decode($"{RootPath}/images/特殊标识/无.png"));

        // imgDict.Add("DefaultBackBoard", SKBitmap.Decode($"{RootPath}/images/背景板/300201.png"));

        // imgDict.Add("RecommandDs", SKBitmap.Decode($"{RootPath}/images/推荐定数.png"));

        // imgDict.Add("RAT15000", SKBitmap.Decode($"{RootPath}/images/RAT框/15000.png"));
        // imgDict.Add("RAT14500", SKBitmap.Decode($"{RootPath}/images/RAT框/14500.png"));
        // imgDict.Add("RAT14000", SKBitmap.Decode($"{RootPath}/images/RAT框/14000.png"));
        // imgDict.Add("RAT13000", SKBitmap.Decode($"{RootPath}/images/RAT框/13000.png"));

        // imgDict.Add("DefaultNameFramework", SKBitmap.Decode($"{RootPath}/images/wuwuwu.png"));
        // imgDict.Add("DefaultAvatar", SKBitmap.Decode($"{RootPath}/images/avatar.png"));

        // imgDict.Add("Title15000", SKBitmap.Decode($"{RootPath}/images/称号/15000.png"));
        // imgDict.Add("Title14000", SKBitmap.Decode($"{RootPath}/images/称号/14000.png"));
        // imgDict.Add("Title13000", SKBitmap.Decode($"{RootPath}/images/称号/13000.png"));


        // fontDict.Add("400W", SKTypeface.FromFile($"{RootPath}/font/江城圆体 400W.ttf"));
        // fontDict.Add("500W", SKTypeface.FromFile($"{RootPath}/font/江城圆体 500W.ttf"));
        // fontDict.Add("600W", SKTypeface.FromFile($"{RootPath}/font/江城圆体 600W.ttf"));
        // fontDict.Add("700W", SKTypeface.FromFile($"{RootPath}/font//江城圆体 700W.ttf"));
        // fontDict.Add("RoGSanSrfStd_Bd", SKTypeface.FromFile($"{RootPath}/font/RoGSanSrfStd-Bd.otf"));

        _imgDict = imgDict.ToFrozenDictionary();
        _fontDict = fontDict.ToFrozenDictionary();
    }

}
