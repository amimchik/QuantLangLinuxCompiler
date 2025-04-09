using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.UnaryExpression;

public class NOTExpressionNode(ExpressionNode operand) : UnaryExpressionNode(operand)
{
    public override NodeType Type => NodeType.NOTExpression;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override bool IsLeftHandSide() => false;
}
