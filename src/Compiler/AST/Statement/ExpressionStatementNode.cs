using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

public class ExpressionStatementNode(ExpressionNode expr) : StatementNode
{
    public ExpressionNode Expression { get; set; } = expr;
    public override NodeType Type => NodeType.ExpressionStatement;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => MakeList(Expression);
}
