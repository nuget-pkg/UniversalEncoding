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
                        }
                    }
                }
                """;
            Log(SafeSourceCode(code));
            string fname = """[1080p] ✅ 👀 🫧 💻 🌐 🎵 <xml>aaa</xml> ; {Title}!? x=11+22-33; ,(🔥引火帝国🔥):"name1" 'name2'?.txt""";
            Log(SafeFileName(fname, prettyQuotesPairs: true), "⁅markup⁆[blue]adjusted file name[/]");
            Log(SafeFileName(fname, prettyQuotesPairs: true, replaceSurrogate: ""), "⁅markup⁆[green]adjusted file name (keeping surrogate pairs)[/]");
            Log(SafeFileName(fname, prettyQuotesPairs: true, replaceSurrogate: "@"), "⁅markup⁆[purple]adjusted file name (spicifying surrogate pairs' replacement)[/]");
            Log(SafeFileName(code));
        } catch (Exception ex) {
            Sys.Crash(ex);
        }
    }
}
