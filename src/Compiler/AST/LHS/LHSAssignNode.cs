using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.LHS;

public class LHSAssignNode(string name) : LHSNode
{
    public string Name { get; set; } = name;
    public override List<ASTNode> GetChildNodes() => [];
    public override NodeType Type => NodeType.LHSAssign;
    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
}
