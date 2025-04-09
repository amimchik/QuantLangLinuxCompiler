using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

public class AssignStatementNode(ExpressionNode left, ExpressionNode right) : StatementNode
{
    public ExpressionNode Left { get; set; } = left;
    public ExpressionNode Right { get; set; } = right;

    public override NodeType Type => NodeType.AssignStatement;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => MakeList(Left, Right);
}
