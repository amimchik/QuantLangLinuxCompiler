using System;
using System.Runtime.InteropServices.Marshalling;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.LHS;

public class LHSMemberNode(LHSNode parent, string name) : LHSNode
{
    public LHSNode Parent { get; set; } = parent;
    public string MemberName { get; set; } = name;
    public override List<ASTNode> GetChildNodes() => MakeList(Parent);
    public override NodeType Type => NodeType.LHSMember;
    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);
}
