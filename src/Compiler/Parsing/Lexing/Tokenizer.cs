using System;
using System.Numerics;
using System.Text;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.Lexing;

public class Tokenizer(string input)
{
    private readonly TextIterator it = new(input);
    private readonly List<Token> tokens = [];
    private readonly Dictionary<string, Token> keywords = new()
    {
        { "let", new Token(TokenType.Let, string.Empty) },
        { "fn", new Token(TokenType.Fn, string.Empty) },
        { "struct", new Token(TokenType.Struct, string.Empty) },
        { "prot", new Token(TokenType.Prot, string.Empty) },
        { "if", new Token(TokenType.If, string.Empty) },
        { "else", new Token(TokenType.Else, string.Empty) },
        { "return", new Token(TokenType.Return, string.Empty) },
        { "while", new Token(TokenType.While, string.Empty) },
        { "sizeof", new Token(TokenType.Sizeof, string.Empty) },
        { "i32", new Token(TokenType.I32, string.Empty) },
        { "i64", new Token(TokenType.I64, string.Empty) },
        { "i16", new Token(TokenType.I16, string.Empty) },
        { "char", new Token(TokenType.Char, string.Empty) },
        { "float", new Token(TokenType.Float, string.Empty) },
        { "double", new Token(TokenType.Double, string.Empty) }
    };
    private readonly Dictionary<string, Token> specTokens = new()
    {
        { "+", new Token(TokenType.Plus, string.Empty) },
        { "-", new Token(TokenType.Minus, string.Empty) },
        { "*", new Token(TokenType.Star, string.Empty) },
        { "/", new Token(TokenType.Slash, string.Empty) },
        { "%", new Token(TokenType.Modulo, string.Empty) },
        { ">", new Token(TokenType.GT, string.Empty) },
        { "<", new Token(TokenType.LT, string.Empty) },
        { ">=", new Token(TokenType.GTEQ, string.Empty) },
        { "<=", new Token(TokenType.LTEQ, string.Empty) },
        { "->", new Token(TokenType.Arrow, string.Empty) },
        { "==", new Token(TokenType.EQ, string.Empty) },
        { "!=", new Token(TokenType.NTEQ, string.Empty) },
        { "!", new Token(TokenType.NOT, string.Empty) },
        { "&&", new Token(TokenType.AND, string.Empty) },
        { "||", new Token(TokenType.OR, string.Empty) },
        { "&", new Token(TokenType.Ampersand, string.Empty) },
        { "|", new Token(TokenType.BOR, string.Empty) },
        { "^", new Token(TokenType.XOR, string.Empty) },
        { ",", new Token(TokenType.Comma, string.Empty) },
        { ".", new Token(TokenType.Dot, string.Empty) },
        { ":", new Token(TokenType.Colon, string.Empty) },
        { ";", new Token(TokenType.Semicolon, string.Empty) },
        { "{", new Token(TokenType.LBrace, string.Empty) },
        { "(", new Token(TokenType.LParen, string.Empty) },
        { "[", new Token(TokenType.LBracket, string.Empty) },
        { "}", new Token(TokenType.RBrace, string.Empty) },
        { ")", new Token(TokenType.RParen, string.Empty) },
        { "]", new Token(TokenType.RBracket, string.Empty) },
        { "=", new Token(TokenType.Assign, string.Empty) },
    };
    private HashSet<char> specChars = [.. "+-*/%<>=!&|^,.;:{}()[]"];
    public List<Token> Tokenize()
    {
        while (Current != '\0')
        {
            char current = Current;

            if (current == '/' && "/*".Contains(it.Peek(1)))
            {
                if (it.Peek(1) == '/')
                {
                    TokenizeComment();
                }
                else
                {
                    TokenizeMultilineComment();
                }
            }
            else if (char.IsDigit(current))
            {
                TokenizeNumber();
            }
            else if (specChars.Contains(current))
            {
                TokenizeSpec();
            }
            else if (char.IsLetter(current))
            {
                TokenizeWord();
            }
            else if (current == '\"')
            {
                TokenizeStringLiteral();
            }
            else if (current == '\'')
            {
                TokenizeCharLiteral();
            }
            else
            {
                Next();
            }
        }
        AddToken(Token.EOF);

        return tokens;
    }
    private void TokenizeCharLiteral()
    {
        StringBuilder buffer = new();
        Next();
        while (true)
        {
            if (Current == '\'' && !IsEscaped())
            {
                Next();
                break;
            }
            buffer.Append(Current);
            Next();
        }

        sbyte[] sbytes = ConvertEscapedStringToSBytes(buffer.ToString());

        if (sbytes.Length != 1)
            throw new Exception("char literal must contain only one character");

        byte bytec = unchecked((byte)sbytes[0]);

        AddToken(TokenType.CharLiteral, bytec.ToString());
    }
    private void TokenizeStringLiteral()
    {
        StringBuilder buffer = new();
        Next();
        while (true)
        {
            if (Current == '\"' && !IsEscaped())
            {
                Next();
                break;
            }
            buffer.Append(Current);
            Next();
        }

        sbyte[] sbytes = ConvertEscapedStringToSBytes(buffer.ToString());

        byte[] bytes = sbytes.Select(sb => unchecked((byte)sb)).ToArray();

        AddToken(TokenType.StringLiteral, Encoding.ASCII.GetString(bytes));
    }
    private bool IsEscaped()
    {
        int backslashes = 0;
        for (int i = -1; Peek(i) == '\\'; i--)
            backslashes++;

        return backslashes % 2 == 1;
    }
    public static sbyte[] ConvertEscapedStringToSBytes(string input)
    {
        List<sbyte> result = new();

        for (int i = 0; i < input.Length;)
        {
            if (input[i] == '\\' && i + 1 < input.Length)
            {
                char next = input[i + 1];
                switch (next)
                {
                    case 'n': result.Add((sbyte)'\n'); i += 2; break;
                    case 't': result.Add((sbyte)'\t'); i += 2; break;
                    case 'r': result.Add((sbyte)'\r'); i += 2; break;
                    case 'b': result.Add((sbyte)'\b'); i += 2; break;
                    case 'f': result.Add((sbyte)'\f'); i += 2; break;
                    case 'v': result.Add((sbyte)'\v'); i += 2; break;
                    case 'a': result.Add((sbyte)'\a'); i += 2; break;
                    case '\\': result.Add((sbyte)'\\'); i += 2; break;
                    case '\'': result.Add((sbyte)'\''); i += 2; break;
                    case '"': result.Add((sbyte)'"'); i += 2; break;

                    // \ddd
                    case >= '0' and <= '9':
                        {
                            int j = i + 1;
                            string number = "";
                            while (j < input.Length && char.IsDigit(input[j]) && number.Length < 3)
                            {
                                number += input[j];
                                j++;
                            }

                            if (int.TryParse(number, out int decVal) && decVal <= 255)
                            {
                                result.Add((sbyte)decVal);
                                i = j;
                            }
                            else
                            {
                                throw new FormatException($"Invalid escape sequence at position {i}: \\{number}");
                            }
                            break;
                        }

                    // \xXX — hex escape
                    case 'x':
                        {
                            if (i + 3 < input.Length)
                            {
                                string hex = input.Substring(i + 2, 2);
                                if (byte.TryParse(hex, System.Globalization.NumberStyles.HexNumber, null, out byte hexVal))
                                {
                                    result.Add((sbyte)hexVal);
                                    i += 4;
                                }
                                else
                                {
                                    throw new FormatException($"Invalid hex escape sequence at position {i}: \\x{hex}");
                                }
                            }
                            else
                            {
                                throw new FormatException($"Incomplete hex escape at end of input");
                            }
                            break;
                        }

                    default:
                        throw new FormatException($"Unknown escape sequence: \\{next}");
                }
            }
            else
            {
                result.Add((sbyte)input[i]);
                i++;
            }
        }

        return result.ToArray();
    }
    private void TokenizeWord()
    {
        StringBuilder buffer = new();

        while (char.IsLetterOrDigit(Current))
        {
            buffer.Append(Current);
            Next();
        }

        if (keywords.ContainsKey(buffer.ToString()))
        {
            AddToken(keywords[buffer.ToString()]);
        }
        else
        {
            AddToken(TokenType.Identifier, buffer.ToString());
        }
    }
    private void TokenizeSpec()
    {
        StringBuilder buffer = new();

        while (specTokens.ContainsKey(buffer.ToString() + Current.ToString()))
        {
            buffer.Append(Current);

            Next();
        }

        AddToken(specTokens[buffer.ToString()]);
    }
    private void TokenizeNumber()
    {
        if (Peek(0) == '0' && char.ToLower(Peek(1)) == 'x')
        {
            Next();
            Next();
            TokenizeHex();
        }
        else
        {
            TokenizeDec();
        }
    }
    private void TokenizeHex()
    {
        StringBuilder buffer = new();

        const string hexChars = "0123456789abcdef";

        while (hexChars.Contains(Peek(0)))
        {
            buffer.Append(Peek(0));
            it.Next();
        }

        AddToken(TokenType.NumberLiteral, HexToInt(buffer.ToString()).ToString());
    }
    private static long HexToInt(string hex) => long.Parse(hex, System.Globalization.NumberStyles.HexNumber);
    private void TokenizeDec()
    {
        StringBuilder buffer = new();

        bool isFloat = false;

        while (true)
        {
            if (Current == '.')
            {
                if (isFloat)
                {
                    break;
                }
                buffer.Append(',');
            }
            if (char.IsDigit(Current))
            {
                buffer.Append(Current);
            }
            else
            {
                break;
            }
            Next();
        }

        AddToken(TokenType.NumberLiteral, buffer.ToString());
    }
    private void AddToken(Token t)
    {
        tokens.Add(t);
    }
    private void AddToken(TokenType type, string l = "")
    {
        tokens.Add(new Token(type, l));
    }
    private char Peek(int offset) => it.Peek(offset);
    private char Next() => it.Next();
    private char Current => it.Peek(0);
    private void TokenizeComment()
    {
        it.Next();
        it.Next();
        while (it.Peek(0) != '\n')
        {
            it.Next();
        }
        it.Next();
    }
    private void TokenizeMultilineComment()
    {
        it.Next();
        it.Next();
        while (true)
        {
            if (!(it.Peek(0) == '*' && it.Peek(1) == '/'))
            {
                break;
            }
            it.Next();
        }
        it.Next();
        it.Next();
    }
    private class TextIterator(string input)
    {
        private readonly string input = input;
        private int pos = 0;
        public char Peek(int offset)
        {
            return SafeGet(pos + offset);
        }
        public char Next()
        {
            if (pos < input.Length)
            {
                pos++;
            }
            return SafeGet(pos);
        }
        private char SafeGet(int index)
        {
            return InRange(index) ? input[index] : '\0';
        }
        private bool InRange(int index)
        {
            return index >= 0 && index < input.Length;
        }
    }
}
