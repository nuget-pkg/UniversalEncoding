using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Global;

public class UniversalTransformer {
    public static int Add2(int a, int b) {
        return a + b;
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
    public static string SafeSourceCode(
        string codeString,
        bool dontReplacePeriod = false,
        bool dontReplaceComma = false
        ) {
        codeString = JustOneLineFeed(codeString);
        codeString = codeString
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
            codeString = codeString.Replace(".", "．");
        }
        if (!dontReplaceComma) {
            codeString = codeString.Replace(",", "，");
        }
        return codeString;
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
