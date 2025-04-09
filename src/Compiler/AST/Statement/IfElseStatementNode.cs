using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

public class IfElseStatementNode(ExpressionNode condition, StatementNode thenBlock, StatementNode elseBlock) : StatementNode
{
    public ExpressionNode Condition { get; set; } = condition;
    public StatementNode ThenBlock { get; set; } = thenBlock;
    public StatementNode ElseBlock { get; set; } = elseBlock;
    public override NodeType Type => NodeType.IfElseStatement;
    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
    public override List<ASTNode> GetChildNodes() => MakeList(condition, thenBlock, elseBlock);
}
