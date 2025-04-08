using org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.Lexing;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.Preprocessing;

namespace org.amimchik.QuantLangLinuxCompiler.src.Runner;

public class Runner
{
    public static void Main(string[] args)
    {
        Tokenizer tokenizer = new("""
        fn main(argc: i32, argv: char**) -> i32 {
            printf("Hello, world!")
            return 0
        }
        """);
        foreach (var t in tokenizer.Tokenize())
        {
            Console.WriteLine(t);
        }
    }
}