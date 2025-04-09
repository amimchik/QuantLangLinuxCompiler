using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

public class AsmStatementNode(string code) : StatementNode
{
    public string AsmCode { get; set; } = code;

    public override NodeType Type => NodeType.AsmStatement;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => [];
}
