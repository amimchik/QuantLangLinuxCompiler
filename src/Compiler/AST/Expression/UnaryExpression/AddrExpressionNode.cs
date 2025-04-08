using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.UnaryExpression;

public class AddrExpressionNode(ExpressionNode operand) : UnaryExpressionNode(operand)
{
    public override NodeType Type => NodeType.AddrExpression;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
}
