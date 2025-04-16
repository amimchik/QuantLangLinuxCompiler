using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.BinaryExpression;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.UnaryExpression;
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
        if (Match(TokenType.Struct))
        {
            nodes.Add(StructDeclaration());
        }

        return new BlockStatementNode(nodes);
    }
    private StatementNode StructDeclaration()
    {
        if (!Match(out Token name, TokenType.Identifier))
        {
            throw new Exception("Expected identifier after 'struct' keyword");
        }
        Expect(TokenType.LBrace, new Exception("Expected block after struct declaration"));
        List<VariableDeclarationStatementNode> vars = [];
        while (!Match(TokenType.RBrace))
        {
            vars.Add(VariableDeclaration(true));
            Expect(TokenType.Semicolon, new Exception("Expected ';' token after decl in struct"));
        }
        Expect(TokenType.Semicolon, new Exception("Expected ';' token after struct declaration"));
        return new StructDeclarationStatementNode(name.Lexeme, vars);
    }
    private StatementNode FunctionDeclaration()
    {
        //Console.WriteLine($"p = {1}");
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
        if (Match(TokenType.Assign))
        {
            if (explicitNoInitialValue)
            {
                throw new Exception("Cannot set initial value here");
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
        Expect(TokenType.LBrace, new("Expected '{' in block"));

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
        if (Current.Type == TokenType.Let)
        {
            Advance();
            var decl = VariableDeclaration();
            Expect(TokenType.Semicolon, new Exception("';' expected'"));
            return decl;
        }
        if (Current.Type == TokenType.While)
        {
            return WhileStatement();
        }
        if (Current.Type == TokenType.Asm)
        {
            return AsmStatement();
        }
        if (Current.Type == TokenType.Return)
        {
            return Return();
        }
        return Assigment();
    }
    private ReturnStatementNode Return()
    {
        Expect(TokenType.Return, new Exception("return excepted"));

        var ret = new ReturnStatementNode(Expression());

        Expect(TokenType.Semicolon, new Exception("';' expected"));

        return ret;
    }
    private IfElseStatementNode IfElseStatement()
    {
        Expect(TokenType.If, new Exception("if excepted"));

        var condition = Expression();

        var thenBlock = Block();

        Expect(TokenType.Else, new Exception("else excepted"));

        var elseBlock = Block();

        return new IfElseStatementNode(condition, thenBlock, elseBlock);
    }
    private WhileStatementNode WhileStatement()
    {
        Expect(TokenType.While, new Exception("while excepted"));

        var condition = Expression();

        var block = Block();

        return new WhileStatementNode(condition, block);
    }
    private AsmStatementNode AsmStatement()
    {
        Expect(TokenType.Asm, new Exception("asm expected"));

        Expect(TokenType.LParen, new Exception("'(' expected after asm keyword"));

        if (!Match(out Token t, TokenType.StringLiteral))
        {
            throw new Exception("String literal expected inside asm node");
        }

        Expect(TokenType.RParen, new Exception("')' expected after asm keyword"));

        return new AsmStatementNode(t.Lexeme);
    }
    private StatementNode Assigment()
    {
        var left = Expression();
        if (Match(TokenType.Assign))
        {
            var right = Expression();

            if (!left.IsLeftHandSide())
            {
                throw new Exception("left from = statement expr must be left hand side");
            }

            Expect(TokenType.Semicolon, new Exception("Expected ';' token after expression"));

            return new AssignStatementNode(left, right);
        }
        else
        {
            Expect(TokenType.Semicolon, new Exception("Expected ';' token after expression"));
            return new ExpressionStatementNode(left);
        }
    }
    private ExpressionNode Expression()
    {
        return LogicalOr();
    }
    private ExpressionNode LogicalOr()
    {
        ExpressionNode left = LogicalAnd();

        while (Match(TokenType.OR))
        {
            ExpressionNode right = LogicalAnd();

            left = new ORExpressionNode(left, right);
        }

        return left;
    }
    private ExpressionNode LogicalAnd()
    {
        ExpressionNode left = BitwiseOr();

        while (Match(TokenType.AND))
        {
            ExpressionNode right = BitwiseOr();

            left = new ANDExpressionNode(left, right);
        }

        return left;
    }
    private ExpressionNode BitwiseOr()
    {
        ExpressionNode left = XOr();

        while (Match(TokenType.BOR))
        {
            ExpressionNode right = XOr();

            left = new BORExpressionNode(left, right);
        }

        return left;
    }
    private ExpressionNode XOr()
    {
        ExpressionNode left = BitwiseAnd();

        while (Match(TokenType.XOR))
        {
            ExpressionNode right = BitwiseAnd();

            left = new BORExpressionNode(left, right);
        }

        return left;
    }
    private ExpressionNode BitwiseAnd()
    {
        ExpressionNode left = Equals();

        while (Match(TokenType.Ampersand))
        {
            ExpressionNode right = Equals();

            left = new BORExpressionNode(left, right);
        }

        return left;
    }
    private ExpressionNode Equals()
    {
        ExpressionNode left = Compare();

        while (Match(out Token op, TokenType.EQ, TokenType.NTEQ))
        {
            ExpressionNode right = Compare();

            if (op.Type == TokenType.EQ)
            {
                left = new EQExpressionNode(left, right);
            }
            else
            {
                left = new NTEQExpressionNode(left, right);
            }
        }

        return left;
    }
    private ExpressionNode Compare()
    {
        ExpressionNode left = Additive();

        while (Match(out Token op, TokenType.LT, TokenType.GT, TokenType.LTEQ, TokenType.GTEQ))
        {
            ExpressionNode right = Additive();

            if (op.Type == TokenType.LT)
            {
                left = new LTExpressionNode(left, right);
            }
            else if (op.Type == TokenType.GT)
            {
                left = new GTExpressionNode(left, right);
            }
            else if (op.Type == TokenType.GTEQ)
            {
                left = new GTEQExpressionNode(left, right);
            }
            else
            {
                left = new LTEQExpressionNode(left, right);
            }
        }

        return left;
    }
    private ExpressionNode Additive()
    {
        ExpressionNode left = Multiplicative();

        while (Match(out Token op, TokenType.Plus, TokenType.Minus))
        {
            ExpressionNode right = Multiplicative();

            if (op.Type == TokenType.Minus)
            {
                left = new SubstractExpressionNode(left, right);
            }
            else
            {
                left = new AddExpressionNode(left, right);
            }
        }

        return left;
    }
    private ExpressionNode Multiplicative()
    {
        ExpressionNode left = Unary();

        while (Match(out Token op, TokenType.Plus, TokenType.Minus))
        {
            ExpressionNode right = Unary();

            if (op.Type == TokenType.Minus)
            {
                left = new SubstractExpressionNode(left, right);
            }
            else
            {
                left = new AddExpressionNode(left, right);
            }
        }

        return left;
    }
    private ExpressionNode Unary()
    {
        if (Match(out Token op, TokenType.NOT, TokenType.Ampersand, TokenType.Star))
        {
            if (op.Type == TokenType.NOT)
            {
                return new NOTExpressionNode(Unary());
            }
            if (op.Type == TokenType.Ampersand)
            {
                return new AddrExpressionNode(Unary());
            }
            return new DerefExpressionNode(Unary());
        }
        return PrimaryOperator();
    }
    private ExpressionNode PrimaryOperator()
    {
        var expr = PrimaryValue();

        if (Match(TokenType.LBracket))
        {
            var index = Expression();

            Expect(TokenType.RBracket, new Exception("Expected ']'"));

            return new IndexExpressionNode(expr, index);
        }
        if (Match(TokenType.Dot))
        {
            if (!Match(out Token id, TokenType.Identifier))
            {
                throw new Exception("Expected identifier after '<val>.' node");
            }
            return new StructMemberExpressionNode(expr, id.Lexeme);
        }
        if (Match(TokenType.Arrow))
        {
            if (!Match(out Token id, TokenType.Identifier))
            {
                throw new Exception("Expected identifier after '<val>->' node");
            }
            return new StructMemberExpressionNode(new DerefExpressionNode(expr), id.Lexeme);
        }

        return expr;
    }
    private ExpressionNode PrimaryValue()
    {
        if (Match(out Token t, TokenType.StringLiteral))
        {
            return new StringLiteralExpressionNode([..
            System.Text.Encoding.ASCII
            .GetBytes(t.Lexeme)
            .Select(c => (sbyte)c)
            ]);
        }
        if (Match(out t, TokenType.NumberLiteral, TokenType.CharLiteral))
        {
            return new NumberLiteralExpressionNode(
                int.TryParse(t.Lexeme, out int ival) ? ival : default,

                double.TryParse(t.Lexeme, out double fval) ? fval : default
            );
        }
        if (Match(out t, TokenType.Identifier))
        {
            if (Match(TokenType.LParen))
            {
                List<ExpressionNode> args = [];

                while (!Match(TokenType.RParen))
                {
                    args.Add(Expression());
                    if (!Match(TokenType.Comma))
                    {
                        Expect(TokenType.RParen, new Exception("Expected ')' token"));
                        break;
                    }
                }

                return new FunctionCallExpressionNode(t.Lexeme, args);
            }
            return new VariableCallExpressionNode(t.Lexeme);
        }
        if (Match(TokenType.LParen))
        {
            var expr = Expression();

            Expect(TokenType.RParen, new Exception("expected ')' token"));

            return expr;
        }

        throw new Exception("Primary expected");
    }

    private void Expect(TokenType type, Exception e)
    {
        if (!Match(type))
        {
            throw e;
        }
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
            Console.WriteLine($"Match:{Current};{pos}");
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
            Console.WriteLine($"Advance:{Current};{pos}");
            if (SafeGet(pos) != Token.EOF)
            {
                pos++;
                return true;
            }
            return false;
        }
        public bool Rewind()
        {
            Console.WriteLine($"Rewind:{Current};{pos}");
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
