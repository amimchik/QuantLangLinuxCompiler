using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression;

public class NumberLiteralExpressionNode(long intv, double floatv) : ExpressionNode
{
    public long IntegerValue { get; set; } = intv;
    public double FloatingValue { get; set; } = floatv;

    public override NodeType Type => NodeType.NumberLiteral;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => [];

    public override bool IsLeftHandSide() => false;
}
