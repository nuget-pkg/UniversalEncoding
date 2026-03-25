using System;
using System.Text;
using Global;
using static Global.EasyObject;
using static Global.UniversalTransformer;

namespace Demo;

static class Program {
    static void Main(string[] args) {
        try {
            SetupConsoleEncoding(Encoding.UTF8);
            UseAnsiConsole = true;
            DebugOutput = true;
            string code =
                """
                namespace HelloWorldApp
                {
                    class Program
                    {
                        static void Main(string[] args)
                        {
                            Console.WriteLine("Hello, World!?");
                            Console.WriteLine("Hello, World❗❓");
                            Console.WriteLine("\u2757\u2753");
                            Console.WriteLine("“"); // this line causes a problem when encoded with SafeSourceCode(code, unicodeEacape: false)
                        }
                    }
                }
                """;
            string fname = """[1080p] ✅ 👀 🫧 💻 🌐 🎵 <xml>aaa</xml> ; {Title}!? x=(11+22-33)*11/2; ,(🔥引火帝国🔥):"name1" 'name2'?.txt""";
            Log(SafeFileName(fname, prettyQuotesPairs: true), "⁅markup⁆[blue]adjusted file name[/]");
            Log(SafeFileName(fname, prettyQuotesPairs: true, replaceSurrogate: ""), "⁅markup⁆[green]adjusted file name (keeping surrogate pairs)[/]");
            Log(SafeFileName(fname, prettyQuotesPairs: true, replaceSurrogate: "@"), "⁅markup⁆[purple]adjusted file name (spicifying surrogate pairs' replacement)[/]");
            Log(SafeFileName(code));

            string safeCode1 = SafeSourceCode(code);
            Log(safeCode1, "safeCode1");
            string safeCode2 = SafeSourceCode(code, unicodeEacape: false); // DON'T USE `unicodeEacape: false` if you have PLAN OF RESTORING!
            Log(safeCode2, "safeCode2");
            string safeCode3 = SafeSourceCode(code, asSingleLine: true);
            Log(safeCode3, "safeCode3");

            string restoredCode1 = RestoreSourceCode(safeCode1);
            string restoredCode2 = RestoreSourceCode(safeCode2);
            string restoredCode3 = RestoreSourceCode(safeCode3);

            Log(restoredCode1, "⁅markup⁆[green]restoredCode1[/]");
            Log(restoredCode2, "⁅markup⁆[green]restoredCode2[/]");
            Log(restoredCode3, "⁅markup⁆[green]restoredCode3[/]");

            Console.WriteLine("\u2757\u2753"); // "❗❓"
        } catch (Exception ex) {
            Sys.Crash(ex);
        }
    }
}
