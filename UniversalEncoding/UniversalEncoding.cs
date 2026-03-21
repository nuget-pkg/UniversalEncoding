using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

// ReSharper disable once CheckNamespace
namespace Global;

// ReSharper disable once InconsistentNaming
public class UniversalEncoding {
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
    public static string AdjustFileName(string fileName, string replaceSurrogate = "★") {
        fileName = fileName
            .Replace("!", "❢")
            .Replace("！", "❢")
            .Replace("\"", "“")
            .Replace("'", "‘")
            .Replace("#", "＃")
            .Replace("%", "％")
            .Replace("&", "＆")
            .Replace("(", "｟")
            .Replace(")", "｠")
            .Replace("（", "｟")
            .Replace("）", "｠")
            .Replace("^", "＾")
            .Replace("~", "～")
            .Replace("\\", "＼")
            .Replace("|", "￤")
            .Replace("｜", "￤")
            .Replace("`", "｀")
            .Replace(";", "；")
            .Replace(":", "：")
            .Replace("*", "＊")
            .Replace("[", "⁅")
            .Replace("]", "⁆")
            .Replace("［", "⁅")
            .Replace("］", "⁆")
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
            .Replace("　", " ")
            ;
        fileName = ReplaceSurrogatePair(fileName, replaceSurrogate);
        return fileName;
    }
    public static string AdjustMetaData(string metadata, string replaceSurrogate = "★") {
        metadata = metadata
            .Replace("\"", "“")
            .Replace("'", "‘")
            .Replace("\\", "＼")
            ;
        metadata = ReplaceSurrogatePair(metadata, replaceSurrogate);
        return metadata;
    }
}
