using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression;

public class IndexExpressionNode(ExpressionNode node, ExpressionNode index) : ExpressionNode
{
    public ExpressionNode Expression { get; set; } = node;
    public ExpressionNode Index { get; set; } = index;

    public override NodeType Type => NodeType.IndexExpression;
    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => MakeList(Expression, Index);
    public override bool IsLeftHandSide() => true;
}
