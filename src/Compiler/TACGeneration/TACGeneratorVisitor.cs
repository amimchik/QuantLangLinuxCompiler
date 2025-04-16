using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.BinaryExpression;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.UnaryExpression;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.TAC;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.TACGeneration;

public class TACGeneratorVisitor(ASTNode node) : IVisitor<TACGeneratorReturnValue>
{
    private readonly ASTNode rootNode = node;
    private List<TACInstruction> instructions = [];

    public TACGeneratorReturnValue Visit(NumberLiteralExpressionNode node)
    {
        return new();
    }

    public TACGeneratorReturnValue Visit(StringLiteralExpressionNode node)
    {
        return new();
    }

    public TACGeneratorReturnValue Visit(FunctionCallExpressionNode node)
    {
        return new();
    }

    public TACGeneratorReturnValue Visit(VariableCallExpressionNode node)
    {
        return new();
    }

    public TACGeneratorReturnValue Visit(AddExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(SubstractExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(MulExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(DivExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(ModExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(ANDExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(BANDExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(ORExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(BORExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(EQExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(NTEQExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(GTEQExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(GTExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(LTEQExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(LTExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(XORExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(AddrExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(DerefExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(NOTExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(StructMemberExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(IndexExpressionNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(ExpressionStatementNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(AssignStatementNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(VariableDeclarationStatementNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(FunctionDeclarationStatementNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(BlockStatementNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(StructDeclarationStatementNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(ReturnStatementNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(AsmStatementNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(IfElseStatementNode node)
    {
        throw new NotImplementedException();
    }

    public TACGeneratorReturnValue Visit(WhileStatementNode node)
    {
        throw new NotImplementedException();
    }
}
