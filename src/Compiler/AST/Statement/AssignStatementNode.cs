using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

public class AssignStatementNode(LHSNode left, ExpressionNode right) : StatementNode
{
    public LHSNode Left { get; set; } = left;
    public ExpressionNode Right { get; set; } = right;

    public override NodeType Type => NodeType.AssignStatement;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => MakeList(Left, Right);
}
