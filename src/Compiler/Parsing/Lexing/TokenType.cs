namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.Lexing;

public enum TokenType
{
    // Keywords:
    Let,
    Fn,
    Struct,
    Prot,
    Ret,
    If,
    Else,
    While,
    Sizeof,
    
    // Types:
    I32,
    I64,
    I16,
    Char,
    Float,
    Double,

    // Id:
    Identifier,

    // Operators:
    Plus,
    Minus,
    Star,
    Slash,
    Modullo,

    // Separators:
    Comma,
    Point,
    Colon,
    Semicolon,

    Lparen,
    RParen,
    LBracket,
    RBracket,
    LBrace,
    RBrace,


    EOF,
}