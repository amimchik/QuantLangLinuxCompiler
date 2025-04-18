using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression;

public class StringLiteralExpressionNode(sbyte[] str) : ExpressionNode
{
    public sbyte[] Value { get; set; } = str;

    public override NodeType Type => NodeType.StringLiteral;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => [];

    public override bool IsLeftHandSide() => false;

    public override string ToString() => $"StrLiteral:[{string.Join(",", Value)}]";
}
