using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression;

public class VariableCallExpressionNode(string name) : ExpressionNode
{
    public string Name { get; set; } = name;
    public override NodeType Type => NodeType.VariableCallExpression;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => [];

    public override bool IsLeftHandSide() => true;
}
