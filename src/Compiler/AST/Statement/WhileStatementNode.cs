using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

public class WhileStatementNode(ExpressionNode condition, StatementNode block) : StatementNode
{
    public ExpressionNode Condition { get; set; } = condition;
    public StatementNode Block { get; set; } = block;

    public override NodeType Type => NodeType.WhileStatement;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => MakeList(Condition, Block);
}
