using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.BinaryExpression;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.UnaryExpression;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler;

public interface IVisitor<T>
{
    T Visit(NumberLiteralExpressionNode node);
    T Visit(StringLiteralExpressionNode node);

    T Visit(FunctionCallExpressionNode node);
    T Visit(VariableCallExpressionNode node);

    T Visit(AddExpressionNode node);
    T Visit(SubstractExpressionNode node);
    T Visit(MulExpressionNode node);
    T Visit(DivExpressionNode node);
    T Visit(ModExpressionNode node);
    T Visit(ANDExpressionNode node);
    T Visit(BANDExpressionNode node);
    T Visit(ORExpressionNode node);
    T Visit(BORExpressionNode node);
    T Visit(EQExpressionNode node);
    T Visit(NTEQExpressionNode node);
    T Visit(GTEQExpressionNode node);
    T Visit(GTExpressionNode node);
    T Visit(LTEQExpressionNode node);
    T Visit(LTExpressionNode node);
    T Visit(XORExpressionNode node);
    T Visit(AddrExpressionNode node);
    T Visit(DerefExpressionNode node);
    T Visit(NOTExpressionNode node);
    T Visit(StructMemberExpressionNode node);

    T Visit(AssignStatementNode node);
    T Visit(VariableDeclarationStatementNode node);
    T Visit(FunctionDeclarationStatementNode node);
    T Visit(BlockStatementNode node);

    T Visit(StructDeclarationStatementNode node);
    T Visit(ReturnStatementNode node);

    T Visit(AsmStatementNode node);

    T Visit(IfElseStatementNode node);
    T Visit(WhileStatementNode node);
}