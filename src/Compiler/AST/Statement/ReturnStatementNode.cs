using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

public class ReturnStatementNode(ExpressionNode value) : StatementNode
{
    public ExpressionNode ReturnValue { get; set; } = value;

    public override NodeType Type => NodeType.ReturnStatement;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => MakeList(ReturnValue);
}
