using System;
using System.IO;
using System.Text;
using Global;
using static Global.EasyObject;
using static Global.HyperOperatingSystem;
#pragma warning disable CS0162 // 到達できないコードが検出されました
try {
    SetupConsoleEncoding(Encoding.UTF8);
    UseAnsiConsole = true;
    //ShowLineNumbers = false;
    DebugOutput = true;
    string fname =
        """[1080p]✅👀🫧💻🌐`within backticks`<xml>aaa</xml>;{Title}!?x=(11+22-33)*11/2;,(🔥引火帝国🔥):"name1"'name2'?.txt""";
    Log(UniversalTransformer.SafeFileName(fname, prettyQuotesPairs: true),
        "⁅markup⁆[blue]adjusted file name[/]");
    Log(UniversalTransformer.SafeFileName(fname, prettyQuotesPairs: true, replaceSurrogate: ""),
        "⁅markup⁆[green]adjusted file name (keeping surrogate pairs)[/]");
    Log(UniversalTransformer.SafeFileName(fname, prettyQuotesPairs: true, replaceSurrogate: "@"),
        "⁅markup⁆[purple]adjusted file name (specifying surrogate pairs' replacement)[/]");
    string code =
        """
        namespace HelloWorldApp
        {
            class Program
            {
                static void Main(string[] args)
                {
                    Console.WriteLine("Hello, World!?");
                    Console.WriteLine("ハロー©,World!?⁅EMOJI⁆◉▶▸⸝↪️↩️➠✅🈂️❓❗𝑪𝒉𝒆𝒄𝒌");
                    Console.WriteLine("Hello, World❗❓");
                    Console.WriteLine("\u2757\u2753");
                    Console.WriteLine("“"); // this line causes a problem when encoded with SafeSourceCode(code, unicodeEscape: false)
                    Console.WriteLine(Add2(11, 22));
                    EvaluateJavaScript(" console.log(`answer=${11+22}`); ");
                }
                private static int Add2(int x, int y)
                {
                    return x + y;
                }
            }
        }
        """;
    string safeCode1 = UniversalTransformer.SafeSourceCode(code); // [DEFAULT] unicodeEacape: true
    Break(safeCode1, "safeCode1"); // 
    // string
    //     safeCode2 = UniversalTransformer.SafeSourceCode(code,
    //         unicodeEacape: false); // DON'T USE `unicodeEacape: false` if you have PLAN OF RESTORING!
    // //Log(safeCode2, "safeCode2");
    // string safeCode3 = UniversalTransformer.SafeSourceCode(code, asSingleLine: true);
    // //Log(safeCode3, "safeCode3");
    string restoredCode1 = UniversalTransformer.RestoreSourceCode(safeCode1);
    //string restoredCode2 = UniversalTransformer.RestoreSourceCode(safeCode2);
    //string restoredCode3 = UniversalTransformer.RestoreSourceCode(safeCode3);
    Break(restoredCode1, "⁅markup⁆[green]restoredCode1[/]");
    //Log(restoredCode2, "⁅markup⁆[green]restoredCode2[/]");
    //Log(restoredCode3, "⁅markup⁆[green]restoredCode3[/]");
    if (false) {
        string sample = File.ReadAllText(GitProjectFile(GetCwd(), "UniversalEncoding.Demo", "assets", "sample.txt")!);
        string escaped = UniversalTransformer.UnicodeEscape(sample);
        Log(escaped, "escaped");
    }
}
catch (Exception ex) {
    Abort(ex);
}