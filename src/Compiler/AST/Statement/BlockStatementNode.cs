using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

public class BlockStatementNode(List<StatementNode> nodes) : StatementNode
{
    public List<StatementNode> Nodes { get; set; } = nodes;
    public override NodeType Type => NodeType.BlockStatement;
    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
    public override List<ASTNode> GetChildNodes() => MakeList([.. Nodes]);
}
