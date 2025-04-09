using System;
using System.Reflection.Metadata;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.Lexing;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.ASTBuilding;

public class Parser(List<Token> tokens)
{
    private TokenIterator it = new(tokens);

    public ASTNode Parse()
    {
        return TopLevel();
    }
    private ASTNode TopLevel()
    {
        List<StatementNode> nodes = [];

        if (Match(TokenType.Fn))
        {
            nodes.Add(FunctionDeclaration());
        }

        return new BlockStatementNode(nodes);
    }
    private StatementNode FunctionDeclaration()
    {
        if (!Match(out Token name, TokenType.Identifier))
        {
            throw new Exception("Expected an identifier after 'fn' keyword");
        }

        if (!Match(TokenType.LParen))
        {
            throw new Exception("Expected an '(' token after fn declaration");
        }

        List<VariableDeclarationStatementNode> args = [];

        if (!Match(TokenType.RParen))
        {
            do
            {
                args.Add(VariableDeclaration(true));
            } while (Match(TokenType.Comma));
            if (!Match(TokenType.RParen))
            {
                throw new Exception("Expected ')' token");
            }
        }

        if (!Match(TokenType.Arrow))
        {
            throw new Exception("Expected '->' token after fn declaration");
        }

        QLType returnType = ParseType();

        var body = Block();

        return new FunctionDeclarationStatementNode(name.Lexeme, args, returnType, body);
    }
    private VariableDeclarationStatementNode VariableDeclaration(bool explicitNoInitialValue = false)
    {
        if (!Match(out Token name, TokenType.Identifier))
        {
            throw new Exception("Expected an identifier after 'let' keyword");
        }
        if (!Match(TokenType.Colon))
        {
            throw new Exception("Expected an ':' token after identifier in variable declaration");
        }
        QLType type = ParseType();
        if (Current.Type == TokenType.Assign && explicitNoInitialValue)
        {
            if (explicitNoInitialValue)
            {
                throw new Exception("Unexpected token '='");
            }
            else
            {
                var initialValue = Expression();
                return new VariableDeclarationStatementNode(name.Lexeme, type, initialValue);
            }
        }
        return new VariableDeclarationStatementNode(name.Lexeme, type, null);
    }
    private BlockStatementNode Block()
    {
        if (!Match(TokenType.LBrace))
        {
            throw new Exception("Expected '{' in block");
        }

        List<StatementNode> stmts = [];

        while (!Match(TokenType.RBrace))
        {
            if (!MayContinue())
            {
                throw new Exception("Excepted '}' after block");
            }
            stmts.Add(Statement());
        }

        return new BlockStatementNode(stmts);
    }
    private StatementNode Statement()
    {
        if (Current.Type == TokenType.If)
        {
            return IfElseStatement();
        }
        throw new Exception();
    }
    private IfElseStatementNode IfElseStatement()
    {
        return null!;
    }
    private WhileStatementNode WhileStatement()
    {
        return null!;
    }
    private AsmStatementNode AsmStatement()
    {
        return null!;
    }
    private ExpressionNode Expression()
    {
        return null!;
    }

    private QLType ParseType()
    {
        QLType type;
        if (Match(TokenType.Struct))
        {
            if (!Match(out Token structName, TokenType.Identifier))
            {
                throw new Exception("Expected type");
            }
            type = new(QLTypePrimitive.Struct, structName.Lexeme);
        }
        else if (Match(TokenType.I32))
        {
            type = new(QLTypePrimitive.I32);
        }
        else if (Match(TokenType.I64))
        {
            type = new(QLTypePrimitive.I64);
        }
        else if (Match(TokenType.I16))
        {
            type = new(QLTypePrimitive.I16);
        }
        else if (Match(TokenType.Char))
        {
            type = new(QLTypePrimitive.Char);
        }
        else if (Match(TokenType.Float))
        {
            type = new(QLTypePrimitive.Float);
        }
        else if (Match(TokenType.Double))
        {
            type = new(QLTypePrimitive.Double);
        }
        else
        {
            throw new Exception("Expected type keyword after ':' after variable declaration");
        }
        while (Match(TokenType.Star))
        {
            type = new QLType(QLTypePrimitive.Pointer, type);
        }
        return type;
    }

    private Token Current { get => it.Current; }
    private bool Match(out Token t, params TokenType[] types) => it.Match(out t, types);
    private bool Advance() => it.Advance();
    private bool Rewind() => it.Rewind();
    private bool MayContinue() => it.MayContinue();
    private bool Match(params TokenType[] types) => Match(out _, types);
    private class TokenIterator(List<Token> tokens)
    {
        private readonly List<Token> tokens = tokens;
        private int pos = 0;
        public Token Current { get => tokens[pos]; }
        public Token Peek(int offset) => SafeGet(pos + offset);
        public bool MayContinue() => Current != Token.EOF;
        public bool Match(out Token t, params TokenType[] types)
        {
            t = Current;
            if (types.Contains(t.Type))
            {
                Advance();
                return true;
            }
            return false;
        }
        public bool Advance()
        {
            if (SafeGet(pos) != Token.EOF)
            {
                pos++;
                return true;
            }
            return false;
        }
        public bool Rewind()
        {
            if (pos > 0)
            {
                pos--;
                return true;
            }
            return false;
        }
        private Token SafeGet(int i) => InRange(i) ? tokens[i] : Token.EOF;
        private bool InRange(int i) => i >= 0 && i < tokens.Count;
    }
}
