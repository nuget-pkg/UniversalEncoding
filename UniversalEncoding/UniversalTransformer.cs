using System.Text.RegularExpressions;

namespace Global;

public class UniversalTransformer {
    public static int Add2(int a, int b) {
        return a + b;
    }
    public static string ReplaceSurrogatePair(string str, string replaceSurrogate = "★") {
        if (replaceSurrogate == "") {
            return str;
        }
        str = Regex.Replace(str, @"[\uD800-\uDFFF]", "{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}");
        str = str.Replace("{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}", replaceSurrogate);
        str = str.Replace("{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}", replaceSurrogate);
        return str;
    }
    public static string SafeFileName(string fileName, string replaceSurrogate = "★") {
        /*
        renamed:    assets/#AI STATION#😀『 六本木純情派／荻野目洋子』1986年作品😀【AIが歌う名曲】#荻野目洋子【ID=KW-Y_BvNbw0】#1920x1080#.mp4 -> assets/《AI STATION》😀『 六本木純情派／荻野目洋子』1986年作品😀【AIが歌う名曲】#荻野目洋子【ID=KW-Y_BvNbw0】〔1920x1080〕.mp4
        renamed:    assets/#AI STATION#😀『 六本木純情派／荻野目洋子』1986年作品😀【AIが歌う名曲】#荻野目洋子【ID=KW-Y_BvNbw0】#854x480#.mp4 -> assets/《AI STATION》😀『 六本木純情派／荻野目洋子』1986年作品😀【AIが歌う名曲】#荻野目洋子【ID=KW-Y_BvNbw0】〔854x480〕.mp4
         */
        //〔〕
        /*
❢
“
‘
＃
％
＆
《
》
＾
～
＼
￤
｀
；
：
＊
〔
〕
〘
〙
≪
≫
／
❔
，
＋
         */
        fileName = fileName
            .Replace("!", "❢")
            .Replace("！", "❢")
            .Replace("\"", "“")
            .Replace("'", "‘")
            .Replace("#", "＃")
            .Replace("%", "％")
            .Replace("&", "＆")
            .Replace("(", "《")
            .Replace(")", "》")
            .Replace("（", "《")
            .Replace("）", "》")
            .Replace("^", "＾")
            .Replace("~", "～")
            .Replace("\\", "＼")
            .Replace("|", "￤")
            .Replace("｜", "￤")
            .Replace("`", "｀")
            .Replace(";", "；")
            .Replace(":", "：")
            .Replace("*", "＊")
            .Replace("[", "〔")
            .Replace("]", "〕")
            .Replace("［", "〔")
            .Replace("］", "〕")
            .Replace("{", "〘")
            .Replace("}", "〙")
            .Replace("｛", "〘")
            .Replace("｝", "〙")
            .Replace("<", "≪")
            .Replace(">", "≫")
            .Replace("＜", "≪")
            .Replace("＞", "≫")
            .Replace("/", "／")
            .Replace("?", "❔")
            .Replace("？", "❔")
            .Replace(",", "，")
            .Replace("+", "＋")
            .Replace("　", " ")
            ;
        fileName = ReplaceSurrogatePair(fileName, replaceSurrogate);
        return fileName;
    }
    public static string SafeMetaData(string metadata, string replaceSurrogate = "★") {
        metadata = metadata
            .Replace("\"", "“")
            .Replace("'", "‘")
            .Replace("\\", "＼")
            ;
        metadata = ReplaceSurrogatePair(metadata, replaceSurrogate);
        return metadata;
    }
}
