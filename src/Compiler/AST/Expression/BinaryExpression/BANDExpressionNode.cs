using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.BinaryExpression;

public class BANDExpressionNode(ExpressionNode left, ExpressionNode right) : BinaryExpressionNode(left, right)
{
    public override NodeType Type => NodeType.BANDExpression;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
}
