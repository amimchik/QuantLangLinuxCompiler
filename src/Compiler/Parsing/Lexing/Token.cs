namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.Lexing;

public class Token(TokenType type, string lexeme)
{
    public string Lexeme { get; } = lexeme;
    public TokenType Type { get; } = type;
    public override string ToString() => $"{Type}:'{Lexeme}'";
    public static Token EOF { get => new(TokenType.EOF, string.Empty); }
}