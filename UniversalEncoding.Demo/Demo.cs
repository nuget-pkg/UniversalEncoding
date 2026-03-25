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
                //!?"'#%&^~\|`;:()[]{}<>, + - * / = ❝　❞←全角スペース
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
            Log(SafeFileName(fname), "⁅markup⁆[blue]adjusted file name[/]");
            Log(SafeFileName(fname, replaceSurrogate: ""), "⁅markup⁆[green]adjusted file name (keeping surrogate pairs)[/]");
            Log(SafeFileName(fname, replaceSurrogate: "@"), "⁅markup⁆[purple]adjusted file name (spicifying surrogate pairs' replacement)[/]");
        } catch (Exception ex) {
            Sys.Crash(ex);
        }
    }
}
