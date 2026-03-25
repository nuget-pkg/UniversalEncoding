using System;
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
    public static string ReplaceSurrogatePair(string str, string replaceSurrogate = "★") {
        if (replaceSurrogate == "") {
            return str;
        }
        str = Regex.Replace(str, @"[\uD800-\uDFFF]", "{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}");
        str = str.Replace("{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}", replaceSurrogate);
        str = str.Replace("{ddbea68e-d93f-4e85-92b5-83b1ace6d50f}", replaceSurrogate);
        return str;
    }
    public static string SafeSourceCode(
        string codeString,
        bool dontReplacePeriod = false,
        bool dontReplaceComma = false
        ) {
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
            .Replace("＜", "≪")

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
        //
        return codeString;
    }
    public static string SafeFileName(string fileName, string replaceSurrogate = "★") {
        fileName = SafeSourceCode(
            fileName,
            dontReplacePeriod: true,
            dontReplaceComma: false
            );
        fileName = fileName
            .Replace("　", " ")
            ;
        fileName = ReplaceSurrogatePair(fileName, replaceSurrogate);
        var numbers = FindCharacterOccurrences(fileName, '“');
        numbers.ForEach(n => Console.WriteLine(n));
        char[] array = fileName.ToCharArray();
        //array[1] = 'p'; // Modify the character at index 1 to 'p'
        int pairCount = numbers.Count / 2;
        for (int i = 0; i < pairCount; i++) {
            int pairA = numbers[i * 2 + 0];
            int pairB = numbers[i * 2 + 1];
            Console.WriteLine(array[pairA] + " " + array[pairB]);
            array[pairA] = '❝';
            array[pairB] = '❞';
        }
        fileName = new string(array);
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
