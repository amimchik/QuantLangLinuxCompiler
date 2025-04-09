using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression;

public class FunctionCallExpressionNode(string name, List<ExpressionNode> args) : ExpressionNode
{
    public string Name { get; set; } = name;
    public List<ExpressionNode> Args { get; set; } = args;
    public override NodeType Type => NodeType.FunctionCallExpression;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => MakeList([.. Args]);

    public override bool IsLeftHandSide() => false;
}
