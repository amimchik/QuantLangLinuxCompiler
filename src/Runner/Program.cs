using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.ASTBuilding;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.Lexing;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.Preprocessing;

namespace org.amimchik.QuantLangLinuxCompiler.src.Runner;

public class Runner
{
    public static void Main(string[] args)
    {
        Tokenizer tokenizer = new("""
        fn main() -> i32 {
            printf("Hello, world!\n");
            return 0;
        }
        """);
        var tokens = tokenizer.Tokenize();
        foreach (var t in tokens)
        {
            Console.WriteLine(t);
        }
        Parser parser = new(tokens);

        ASTNode ast = parser.Parse();

        Console.WriteLine(ast);
    }
}