using ImageGenerate.Utils;
using MaimaiBasic.Classes.B50;
using MaimaiBasic.Classes.Song;
using MaimaiBasic.Enums;
using MaimaiBasic.Extensions;
using SkiaSharp;
using System.Collections.Frozen;

namespace ImageGenerate;

public class ImageGenerator : IImageGenerator, IDisposable
{
    protected static SKImageInfo _cardImgInfo = new SKImageInfo(302, 132);
    protected static SKImageInfo _imgInfo = new SKImageInfo(1640, 2300);
    protected SKSurface _cardSurface = SKSurface.Create(_cardImgInfo);
    protected SKSurface _imgSurface = SKSurface.Create(_imgInfo);
    private SKImageFilter _filter = SKImageFilter.CreateDropShadow(0, 2, 1, 1, new SKColor(0, 0, 0, (byte)(255 * 0.6)));
    private SKPaint _paint = new SKPaint() { IsAntialias = true, };

    public Stream GenerateImage(B50Info info, Dictionary<int, Stream> covers, in FrozenDictionary<int, SongInfo> songInfos)
    {
        DrawDefault(info, covers, songInfos);
        return _imgSurface.Snapshot().Encode(SKEncodedImageFormat.Png, 100).AsStream();
    }

    private void DrawDefault(B50Info info, Dictionary<int, Stream> covers, in FrozenDictionary<int, SongInfo> songInfos)
    {   
        DrawRecommandRate(info);

        DrawNamePlate(info);

        //_paint.ImageFilter = _filter;
        List<Song>? list = info?.Charts?.Dx;
        var cardShadow = SKImageFilter.CreateDropShadow(0, 4, 3, 3, new SKColor(0, 0, 0, (byte)(255 * 0.8)));


        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Song? song = null;
                try
                {
                    song = list?[5 * i + j];
                }
                catch (ArgumentOutOfRangeException) { }

                if (song == null)
                {
                    DrawVoidCard();
                }
                else
                {
                    song.Id = 5 * i + j + 1;
                    DrawCard(song, covers[song.SongId], songInfos[song.SongId]);
                }

                _paint.ImageFilter = cardShadow;
                _cardSurface.Draw(_imgSurface.Canvas, 39 + 1.045f * j * _cardImgInfo.Width, 1818 + i * _cardImgInfo.Height * 1.1f, _paint);
                _paint.ImageFilter = null;
                _cardSurface.Canvas.Clear();
            }
        }

        list = info?.Charts?.Sd;

        for (int i = 0; i < 7; i++)
        {
            for (int j = 0; j < 5; j++)
            {
                Song? song = null;
                try
                {
                    song = list?[5 * i + j];
                }
                catch (ArgumentOutOfRangeException) { }
                if (song == null)
                {
                    DrawVoidCard();
                }
                else 
                {
                    song.Id = 5 * i + j + 1;
                    DrawCard(song, covers[song.SongId], songInfos[song.SongId]);
                }

                _paint.ImageFilter = cardShadow;

                _cardSurface.Draw(_imgSurface.Canvas, 39 + 1.045f * j * _cardImgInfo.Width, 650 + i * _cardImgInfo.Height * 1.1f, _paint);
                _paint.ImageFilter = null;

                _cardSurface.Canvas.Clear();
            }
        }


    }

    private void DrawNameFramework()
    {
        //46 40 150%(1080 174)
        var resizedFrameWork = SongAssets.DefaultNameFramework.Resize(new SKImageInfo(1080, 174), SKFilterQuality.Medium);
        _imgSurface.Canvas.DrawBitmap(resizedFrameWork, 46, 40);
    }

    private void DrawAvatar()
    {
        //56 50 (154,154) roundrect radius 12px
        var resizedAvatar = SongAssets.DefaultAvatar.Resize(new SKImageInfo(154, 154), SKFilterQuality.Medium);

        _imgSurface.Canvas.DrawBitmap(resizedAvatar, 56, 50);

    }

    private void DrawRating(B50Info? info)
    {
        //213 50
        _imgSurface.Canvas.DrawBitmap(SongAssets.RAT15000, 213, 50);

        var ratingStr = info?.Rating.ToString();

        var p = new SKPoint(416, 85);
        var paint = new SKPaint();
        paint.Color = new SKColor(248,232,122);
        paint.Typeface = SongAssets.JangCheng600W;
        paint.TextSize = 28;
        paint.IsAntialias = true;
        for (int i = ratingStr.Length - 1; i >= 0; i--)
        {
            _imgSurface.Canvas.DrawText(ratingStr[i].ToString(), p, paint);
            p.Offset(-20.6f, 0);
        }


        //213 171
        _imgSurface.Canvas.DrawBitmap(SongAssets.Title15000, 212, 171);

        p = new SKPoint(245, 193);

        paint.TextSize = 20.5f;
        paint.Typeface = SongAssets.RoGSanSrfStd_Bd;
        paint.Color = SKColors.White;
        paint.ImageFilter = SKImageFilter.CreateDropShadow(0, 2, 1, 1, SKColors.Black.WithAlpha((byte)(255*0.5)));
        _imgSurface.Canvas.DrawText($"旧版本{info?.Charts?.Sd?.Sum(song => song.Ra)}+新版本{info?.Charts?.Dx?.Sum(song => song.Ra)}", p, paint);

    }

    private void DrawName(B50Info info)
    {
        //213 102 Rect 354 64 radius 6px 
        var rect = new SKRect(0, 0, 354, 64);
        var roundrect = new SKRoundRect(rect, 6);
        roundrect.Offset(214, 102);

        var paint = new SKPaint();
        paint.Style = SKPaintStyle.Fill;
        paint.IsAntialias = true;
        //paint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal,2);
        paint.Color = SKColors.White;
        _imgSurface.Canvas.DrawRoundRect(roundrect, paint);

        // inner 2px shadow color(110)



        //_imgSurface.Canvas.Save();

        paint.Style = SKPaintStyle.Stroke;
        paint.StrokeWidth = 2;
        //paint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, 2);
        paint.Color = new SKColor(110, 110, 100);
        //_imgSurface.Canvas.ClipRoundRect(roundrect);



        _imgSurface.Canvas.DrawRoundRect(roundrect, paint);
        rect = new SKRect(0, 0, 350, 60);
        roundrect = new SKRoundRect(rect, 6);
        roundrect.Offset(216, 104);
        paint.Color = SKColors.White;
        _imgSurface.Canvas.DrawRoundRect(roundrect, paint);

        var p = new SKPoint(225,152);

        paint.Color = SKColors.Black;
        paint.Typeface = SongAssets.JangCheng400W;
        paint.TextSize = 48;
        paint.Style = SKPaintStyle.Fill;
        paint.IsAntialias = true;

        _imgSurface.Canvas.DrawText(info.UserName, p, paint);



    }

    private void DrawRecommandRate(B50Info info)
    {
        int sdMin = 0;
        int dxMin = 0;
        if (info?.Charts?.Sd?.Count != 0)
            sdMin = info!.Charts!.Sd![^1].Ra;
        if (info?.Charts?.Dx?.Count != 0)
            dxMin = info!.Charts!.Dx![^1].Ra;

        var resiedBroad = SongAssets.DefaultBackBoard.Resize(new SKImageInfo(1640, 686), SKFilterQuality.High);

        //_paint.ImageFilter = _filter;
        _imgSurface.Canvas.DrawBitmap(resiedBroad, 0, 0);
        //_paint.ImageFilter = null;
        _imgSurface.Canvas.DrawBitmap(SongAssets.BackgroundImg, 0, 0);

        var shadow = SKImageFilter.CreateDropShadow(0, 3, 3, 3, new SKColor(0, 0, 0, (byte)(255 * 0.4)));

        _paint.ImageFilter = shadow;

        
        _imgSurface.Canvas.DrawBitmap(SongAssets.RecommandDS, 46, 248, _paint);

        _paint.ImageFilter = null;
        //"3" 32px , 

        // rect 155 55

        //up offset 177 161
        //down offset 177 298



        var textColor = new SKColor(203, 41, 98);

        // origin of recommandplate
        var point = new SKPoint(46,248);

        var paint = new SKPaint();
        paint.Typeface = SongAssets.RoGSanSrfStd_Bd;
        paint.TextSize = 38;
        paint.Color = textColor;
        paint.IsAntialias = true;

        //add offset. this first one rect left down corner.
        point.Offset(177, 161);
        #region rating

        #region sdmin
        // to center
        point.Offset(32, -10);
        _imgSurface.Canvas.DrawText((sdMin + 3).ToString(), point, paint);
        //point.Offset(-2,0);


        var newP = new SKPoint(point.X - 8,point.Y);
        for (int i = 0; i < 4; i++)
        {
            newP.Offset(158.5f, 0);
            var rate = RatingTable.GetGreaterThanRatingMinRating(13 - i, sdMin + 3);
            var text = "-";
            if (rate is not null)
                text = rate?.ds.ToString("#0.0");
            _imgSurface.Canvas.DrawText(text, newP, paint);
        }

        //add 3 for textline
        point.Offset(0, 55 + 1);

        _imgSurface.Canvas.DrawText(sdMin.ToString(), point, paint);

        newP = new SKPoint(point.X - 8, point.Y);
        for (int i = 0; i < 4; i++)
        {
            newP.Offset(158.5f, 0);
            var rate = RatingTable.GetGreaterThanRatingMinRating(13 - i, sdMin);
            var text = "-";
            if (rate is not null)
                text = rate?.ds.ToString("#0.0");
            _imgSurface.Canvas.DrawText(text, newP, paint);
        }

        #endregion // dxmin

        #region dxmin

        point.Offset(0, 79);

        _imgSurface.Canvas.DrawText((dxMin + 3).ToString(), point, paint);

        newP = new SKPoint(point.X - 8, point.Y);
        for (int i = 0; i < 4; i++)
        {
            newP.Offset(158.5f, 0);
            var rate = RatingTable.GetGreaterThanRatingMinRating(13 - i, dxMin + 3);
            var text = "-";
            if (rate is not null)
                text = rate?.ds.ToString("#0.0");
            _imgSurface.Canvas.DrawText(text, newP, paint);
        }


        point.Offset(0, 55 + 1);

        _imgSurface.Canvas.DrawText(dxMin.ToString(), point, paint);

        newP = new SKPoint(point.X - 8, point.Y);
        for (int i = 0; i < 4; i++)
        {
            newP.Offset(158.5f, 0);
            var rate = RatingTable.GetGreaterThanRatingMinRating(13 - i, dxMin);
            var text = "-";
            if (rate is not null)
                text = rate?.ds.ToString("#0.0");
            _imgSurface.Canvas.DrawText(text, newP, paint);
        }

        #endregion // dxmin

        #endregion //rating

        #region SSS+

        #endregion // SSS+


    }

    void DrawNamePlate(B50Info? info) 
    {
        DrawNameFramework();
        DrawAvatar();
        DrawRating(info);
        DrawName(info);
    }

    private void DrawCard(Song song, SKBitmap cover, SongInfo info)
    {
        var resizedCover = cover.Resize(new SKImageInfo(80, 80), SKFilterQuality.Medium);

        DrawLevelLabel(song.LevelIndex);
        DrawCover(resizedCover);
        DrawDx(song.Type);
        DrawRate(song.Achievements);
        DrawText(song);
        DrawScoreStar(info, song);
        DrawSpecialRate(song);
    }

    private void DrawVoidCard()
    {
        DrawLevelLabel((int)(LevelLabel.None));
    }

    private void DrawCard(Song song, Stream cover, SongInfo info)
    {   
        cover.Position = 0;
        using var skData = SKData.Create(cover);
        var resizedCover = SKBitmap.Decode(skData).Resize(new SKImageInfo(80, 80), SKFilterQuality.Medium);

        DrawLevelLabel(song.LevelIndex);
        DrawCover(resizedCover);
        DrawDx(song.Type);
        DrawText(song);
        DrawRate(song.Achievements);
        DrawScoreStar(info, song);
        DrawSpecialRate(song);
    }

    private void DrawSpecialRate(Song song)
    {
        var fc = song.FC;
        var fs = song.FS;

        SKBitmap fcWillDraw;
        SKBitmap fsWillDraw;


        if (fc == FC.FC)
            fcWillDraw = SongAssets.FC;
        else if (fc == FC.AP)
            fcWillDraw = SongAssets.FCPlus;
        else if (fc == FC.APPlus)
            fcWillDraw = SongAssets.APPlus;
        else
            fcWillDraw = SongAssets.NoneSpecialRate;


        if (fs == FS.FS)
            fsWillDraw = SongAssets.FS;
        else if (fs == FS.FSPlus)
            fsWillDraw = SongAssets.FSPlus;
        else if (fs == FS.FDX)
            fsWillDraw = SongAssets.FDX;
        else
            fsWillDraw = SongAssets.NoneSpecialRate;


        _cardSurface.Canvas.DrawBitmap(fcWillDraw, 229, 101);
        _cardSurface.Canvas.DrawBitmap(fsWillDraw, 264, 101);

    }
    private void DrawScoreStar(SongInfo info, Song song)
    {
        var maxScore = info.Charts[song.LevelIndex]?.MaxScore ?? int.MaxValue;
        int score = song.DxScore;

        var percentage = (double)score / maxScore;

        SKBitmap? willDraw = null;

        if (percentage > 0.97d)
            willDraw = SongAssets.DXStar5;
        else if (percentage > 0.95d)
            willDraw = SongAssets.DXStar4;
        else if (percentage > 0.93d)
            willDraw = SongAssets.DXStar3;
        else if (percentage > 0.90d)
            willDraw = SongAssets.DXStar2;
        else if (percentage > 0.85d)
            willDraw = SongAssets.DXStar1;

        if (willDraw == null) return;

        _cardSurface.Canvas.DrawBitmap(willDraw, 144, 107);
    }

    private void DrawRate(double achievements)
    {
        SKBitmap willDraw;
        if (achievements < 50.000d)
            willDraw = SongAssets.RateD;
        else if (achievements < 60.0000d)
            willDraw = SongAssets.RateC;
        else if (achievements < 70.0000d)
            willDraw = SongAssets.RateB;
        else if (achievements < 75.0000d)
            willDraw = SongAssets.RateBB;
        else if (achievements < 80.0000d)
            willDraw = SongAssets.RateBBB;
        else if (achievements < 90.0000d)
            willDraw = SongAssets.RateA;
        else if (achievements < 94.0000d)
            willDraw = SongAssets.RateAA;
        else if (achievements < 97.0000d)
            willDraw = SongAssets.RateAAA;
        else if (achievements < 98.0000d)
            willDraw = SongAssets.RateS;
        else if (achievements < 99.0000d)
            willDraw = SongAssets.RateSPlus;
        else if (achievements < 99.5000d)
            willDraw = SongAssets.RateSS;
        else if (achievements < 100.0000d)
            willDraw = SongAssets.RateSSPlus;
        else if (achievements < 100.5000d)
            willDraw = SongAssets.RateSSS;
        else
            willDraw = SongAssets.RateSSSPlus;
        _cardSurface.Canvas.DrawBitmap(willDraw, 99, 33);
    }

    private void DrawCover(SKBitmap cover)
    {
        var coverPoint = new SKPoint(14, 13);

        var rect = new SKRect(coverPoint.X, coverPoint.Y, coverPoint.X + cover.Width, coverPoint.Y + cover.Height);
        var roundRect = new SKRoundRect(rect, 5);

        var Canvas = _cardSurface.Canvas;

        var shadow = SKImageFilter.CreateDropShadow(0, 2, 2, 2, new SKColor(0, 0, 0, (byte)(255 * 0.8)));
        _paint.ImageFilter = shadow;
        _paint.IsAntialias = true;
        Canvas.DrawRoundRect(roundRect, _paint);
        Canvas.Save();
        Canvas.ClipRoundRect(roundRect, antialias: true);
        Canvas.DrawBitmap(cover, coverPoint);
        Canvas.Restore();
        _paint.ImageFilter = null;

    }

    private void DrawLevelLabel(int index)
    {
        var Canvas = _cardSurface.Canvas;
        switch (index)
        {
            case 0:
                {
                    Canvas.DrawBitmap(SongAssets.LevelLabelBasic, 7, 6);
                    break;
                }

            case 1:
                {
                    Canvas.DrawBitmap(SongAssets.LevelLabelAdvanced, 7, 6);
                    break;
                }

            case 2:
                {
                    Canvas.DrawBitmap(SongAssets.LevelLabelExpert, 7, 6);
                    break;
                }

            case 3:
                {
                    Canvas.DrawBitmap(SongAssets.LevelLabelMaster, 7, 6);
                    break;
                }

            case 4:
                {
                    Canvas.DrawBitmap(SongAssets.LevelLabelReMaster, 7, 6);
                    break;
                }
            default:
                {
                    Canvas.DrawBitmap(SongAssets.LevelLabelNone, 7, 6);
                    break;
                }
        }
    }

    private void DrawDx(SongType type)
    {
        _paint.ImageFilter = _filter;
        if (type == SongType.DX)
            _cardSurface.Canvas.DrawBitmap(SongAssets.DX, 0, 0, _paint);
        _paint.ImageFilter = null;
    }

    private void DrawText(Song song)
    {
        var cardShadow = SKImageFilter.CreateDropShadow(0, 2f, 1, 1, new SKColor(0, 0, 0, (byte)(255 * 0.8)));
        var Canvas = _cardSurface.Canvas;
        var fontPaint600W = new SKPaint() { Typeface = SongAssets.JangCheng600W, TextSize = 22, Color = SKColors.White, IsAntialias = true, ImageFilter = cardShadow };
        var fontPaint700W = new SKPaint() { Typeface = SongAssets.JangCheng700W, TextSize = 35, Color = SKColors.White, IsAntialias = true, ImageFilter = cardShadow };
        var fontPaint500W = new SKPaint() { Typeface = SongAssets.JangCheng500W, TextSize = 18, Color = SKColors.White, IsAntialias = true, ImageFilter = cardShadow, };

        DrawTextClip(Canvas, song.Title!, new SKRect(99, 27, 102 + 160, 200), fontPaint500W);
        if(song.Achievements > 100.0000d)
            Canvas.DrawText($"{song.Achievements:0.0000}%", 96, 93, fontPaint700W);
        else
            Canvas.DrawText($"{song.Achievements:0.0000}%", 99, 93, fontPaint700W);

        fontPaint700W.TextSize = 17; fontPaint700W.Color = SKColors.Black; fontPaint700W.ImageFilter = null;
        Canvas.DrawText($"#{song.Id}", 16, 122, fontPaint700W);
        Canvas.DrawText($"{song.DS:0.0}→{song.Ra}", 53, 122, fontPaint700W);

        fontPaint600W.TextAlign = SKTextAlign.Right; fontPaint600W.TextSize = 14;

        Canvas.DrawText($"ID:{song.SongId}", 289, 45, fontPaint600W);
        fontPaint600W.TextSize = 14;
        Canvas.DrawText(song.LevelLabel?.ToUpper(), 290, 61, fontPaint600W);
    }

    void DrawTextClip(SKCanvas canvas, string text, SKRect rect, SKPaint paint)
    {
        float wordX = rect.Left;
        float wordY = rect.Top; //+ paint.TextSize;
        bool needOmit = false;
        foreach (string word in text.Select(c => c.ToString()))
        {
            float wordWidth = paint.MeasureText(word);
            if (wordWidth <= rect.Right - wordX)
            {
                canvas.DrawText(word, wordX, wordY, paint);
                wordX += wordWidth;
            }
            else
            {
                needOmit = true;
                break;
            }
        }

        if (needOmit)
        {
            canvas.DrawText("...", wordX, wordY, paint);
        }
    }

    public void Dispose()
    {
        this._filter.Dispose();
        this._paint.Dispose();
        this._cardSurface.Dispose();
        this._imgSurface.Dispose();
    }
}
