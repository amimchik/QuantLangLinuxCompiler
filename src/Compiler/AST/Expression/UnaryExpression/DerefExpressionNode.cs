using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.UnaryExpression;

public class DerefExpressionNode(ExpressionNode operand) : UnaryExpressionNode(operand)
{
    public override NodeType Type => NodeType.DerefExpression;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
}
