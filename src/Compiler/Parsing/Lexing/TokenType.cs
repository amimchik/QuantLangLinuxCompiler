namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.Lexing;

public enum TokenType
{
    // Keywords:
    Let,
    Fn,
    Struct,
    Prot,
    Return,
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

    // Literals:
    NumberLiteral,
    CharLiteral,
    StringLiteral,

    // Operators:
    Plus,
    Minus,
    Star,
    Slash,
    Modulo,

    GT,         // >
    LT,         // <
    GTEQ,       // >=
    LTEQ,       // <=
    EQ,         // ==
    NTEQ,       // !=
    NOT,        // !
    AND,        // &&
    OR,         // ||
    BOR,        // |
    XOR,        // ^
    Ampersand,  // &

    Assign,     // =

    // Separators:
    Comma,      // ,
    Dot,        // .
    Colon,      // :
    Semicolon,  // ;
    Arrow,      // ->

    LParen,
    RParen,
    LBracket,
    RBracket,
    LBrace,
    RBrace,


    EOF,
}