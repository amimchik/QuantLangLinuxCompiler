namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.Lexing;

public class Token(TokenType type, string lexeme)
{
    public string Lexeme { get; } = lexeme;
    public TokenType Type { get; } = type;
}