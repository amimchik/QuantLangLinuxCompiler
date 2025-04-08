using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.LHS;

public class LHSDerefNode(LHSNode node) : LHSNode
{
    public LHSNode Pointer { get; set; } = node;
    public override List<ASTNode> GetChildNodes() => MakeList(Pointer);
    public override NodeType Type => NodeType.LHSMember;
    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
}
