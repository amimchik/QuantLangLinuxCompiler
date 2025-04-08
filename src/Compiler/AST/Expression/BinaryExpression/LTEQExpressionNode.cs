using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.BinaryExpression;

public class LTEQExpressionNode(ExpressionNode left, ExpressionNode right) : BinaryExpressionNode(left, right)
{
    public override NodeType Type => NodeType.LTEQExpression;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
}
