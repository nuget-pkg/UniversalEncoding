using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using static System.Net.Mime.MediaTypeNames;

namespace Global;

public static class UniversalTransformer {
    public static int Add2(int a, int b) {
        return a + b;
    }
    public static string UnicodeEacape(string text) {
        var sb = new StringBuilder();
        sb.Length = 0;
        if (sb.Capacity < text.Length + (text.Length / 10)) {
            sb.Capacity = text.Length + (text.Length / 10);
        }
        foreach (char c in text) {
            if (c > 127) {
                ushort val = c;
                sb.Append("\\u").Append(val.ToString("X4"));
            } else {
                sb.Append(c);
            }
        }
        string result = sb.ToString();
        sb.Length = 0;
        return result;
    }
    public static string UnicodeUnescape(string text) {
        return Regex.Unescape(text);
    }
    public static List<int> FindCharacterOccurrences(string input, char targetChar) {
        List<int> occurrences = input
            .Select((character, index) => new { character, index })
            .Where(item => item.character == targetChar)
            .Select(item => item.index)
            .ToList();
        return occurrences;
    }
    public static string ReplaceSurrogatePair(string str, string replaceSurrogate = "✅") {
        if (replaceSurrogate == "") {
            return str;
        }
        str = Regex.Replace(str, @"[\uD800-\uDFFF]", "{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}");
        str = str.Replace("{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}", replaceSurrogate);
        str = str.Replace("{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}", replaceSurrogate);
        return str;
    }
    public static string JustOneLineFeed(string code) {
        code = code.Replace("\r\n", "\n");
        code = code.Replace("\r", "\n");
        return code;
    }
    public static string JustOneSpace(string str) {
        str = Regex.Replace(str, @"\s+", " ");
#if true
        str = Regex.Replace(str, @"[ ]*↩[ ]*", "↩");
#else
        str = str.Replace(" ↩", "↩").Replace("↩ ", "↩");
#endif
        return str;
    }
    public static string SafeBaseName(string baseName) {
        // [⭕ファイル名に使えない文字 - Google](https://bit.ly/invalid-filename-chars)
        // \ / : * ? " < > |
        baseName = baseName
            .Replace("\\", "￥")
            .Replace("/", "／")
            .Replace(":", "：")
            .Replace("*", "＊")
            .Replace("?", "❓")
            .Replace("\"", "“")
            .Replace("<", "≪")
            .Replace(">", "≫")
            .Replace("|", "￤")
            .Replace("!", "❗") // Not necessary; but NO ONE USE this charcter for filename!
            ;
        return baseName;
    }
    public static string SafeSourceCode(
        string text,
        bool unicodeEacape = true,
        bool oneLine = true,
        bool dontReplacePeriod = false,
        bool dontReplaceComma = false
        ) {
        text = JustOneLineFeed(text);
        if (unicodeEacape) {
            text = UnicodeEacape(text);
        }
        text = text
            .Replace("!", "❗")
            .Replace("?", "❓")

            .Replace("\"", "“")
            .Replace("'", "‘")
            .Replace("`", "｀")

            .Replace("#", "＃")
            .Replace("%", "％")
            .Replace("&", "＆")

            .Replace("^", "＾")
            .Replace("~", "～")

            .Replace("\\", "＼")
            .Replace("|", "￤")

            .Replace(";", "；")
            .Replace(":", "：")

            .Replace("(", "﴾")
            .Replace(")", "﴿")

            .Replace("[", "⁅")
            .Replace("]", "⁆")

            .Replace("{", "꒰")
            .Replace("}", "꒱")

            .Replace("<", "≪")
            .Replace(">", "≫")

            .Replace("+", "＋")
            .Replace("-", "ー")
            .Replace("*", "＊")
            .Replace("/", "／")
            .Replace("=", "＝")
            ;
        if (!dontReplacePeriod) {
            text = text.Replace(".", "．");
        }
        if (!dontReplaceComma) {
            text = text.Replace(",", "，");
        }
        if (oneLine) {
            text = text.Replace("\n", "↩");
        }
        return text;
    }
    public static string RestoreSourceCode(string safeCode) {
        string restored = safeCode;
        restored =
            restored
            .Replace("↩", "\n")
            .Replace("❗", "!")
            .Replace("❓", "?")

            .Replace("“", "\"")
            .Replace("‘", "'")
            .Replace("｀", "`")

            .Replace("＃", "#")
            .Replace("％", "%")
            .Replace("＆", "&")

            .Replace("＾", "^")
            .Replace("～", "~")

            .Replace("＼", "\\")
            .Replace("￤", "|")

            .Replace("；", ";")
            .Replace("：", ":")

            .Replace("﴾", "(")
            .Replace("﴿", ")")

            .Replace("⁅", "[")
            .Replace("⁆", "]")

            .Replace("꒰", "{")
            .Replace("꒱", "}")

            .Replace("≪", "<")
            .Replace("≫", ">")

            .Replace("＋", "+")
            .Replace("ー", "-")
            .Replace("＊", "*")
            .Replace("／", "/")
            .Replace("＝", "=")
            ;
        restored = UnicodeUnescape(restored);
        return restored;
    }
    private static string _PrettyQuotesPairs(string fileName) {
        var occurrences = FindCharacterOccurrences(fileName, '“');
        char[] array = fileName.ToCharArray();
        int pairCount = occurrences.Count / 2;
        for (int i = 0; i < pairCount; i++) {
            int pairA = occurrences[i * 2 + 0];
            int pairB = occurrences[i * 2 + 1];
            array[pairA] = '❝';
            array[pairB] = '❞';
        }
        fileName = new string(array);
        return fileName;
    }
    public static string SafeFileName(
        string fileName,
        string replaceSurrogate = "✅",
        bool prettyQuotesPairs = false
        ) {
        fileName = SafeSourceCode(
            fileName,
            unicodeEacape: false,
            dontReplacePeriod: true,
            dontReplaceComma: false
            );
        fileName = fileName.Replace("\n", "↩");
        fileName = JustOneSpace(fileName);
        fileName = fileName
            .Replace("　", " ")
            ;
        fileName = ReplaceSurrogatePair(fileName, replaceSurrogate);
        if (prettyQuotesPairs) {
            fileName = _PrettyQuotesPairs(fileName);
        }
        return fileName;
    }
    public static string SafeMetaData(
        string metadata,
        string replaceSurrogate = "✅",
        bool prettyQuotesPairs = false
        ) {
        metadata = metadata
            .Replace("\"", "“")
            .Replace("'", "‘")
            .Replace("\\", "＼")
            ;
        metadata = ReplaceSurrogatePair(metadata, replaceSurrogate);
        if (prettyQuotesPairs) {
            metadata = _PrettyQuotesPairs(metadata);
        }
        return metadata;
    }
}
