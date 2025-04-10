namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression;

public class StructMemberExpressionNode(ExpressionNode parent, string memberName) : ExpressionNode
{
    public ExpressionNode Parent { get; set; } = parent;
    public string MemberName { get; set; } = memberName;

    public override NodeType Type => NodeType.StructMemberExpression;

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes() => MakeList(Parent);

    public override bool IsLeftHandSide() => true;
}
