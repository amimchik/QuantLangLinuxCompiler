using System.Numerics;
using System.Reflection.Metadata;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.Lexing;

public class Token(TokenType type, string lexeme)
{
    public string Lexeme { get; } = lexeme;
    public TokenType Type { get; } = type;
    public override string ToString() => $"{Type}:'{Lexeme}'";
    public static bool operator ==(Token left, Token right)
    {
        if (left.Type == right.Type)
        {
            if (ContainsLexeme(left.Type))
            {
                return left.Lexeme == right.Lexeme;
            }
            return true;
        }
        return false;
    }

    public static bool operator !=(Token left, Token right) => !(left == right);
    public static bool ContainsLexeme(TokenType type)
    {
        return new TokenType[] { TokenType.Identifier, TokenType.StringLiteral,
            TokenType.CharLiteral, TokenType.NumberLiteral }
            .Contains(type);
    }
    public static Token EOF { get => new(TokenType.EOF, string.Empty); }

    public override bool Equals(object? obj) =>
        obj is not null && obj is Token other && this == other;
}