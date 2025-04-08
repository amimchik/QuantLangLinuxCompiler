using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

public class VariableDeclarationStatementNode(string name, QLType type, ExpressionNode? initialValue) : StatementNode
{
    public QLType VarType { get; set; } = type;
    public string Name { get; set; } = name;
    public ExpressionNode? InitialValue { get; set; } = initialValue;

    public override NodeType Type => NodeType.VariableDeclarationStatement;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => InitialValue is null ? [] : MakeList(InitialValue);
}
