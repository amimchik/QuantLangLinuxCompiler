namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression;

public abstract class BinaryExpressionNode(ExpressionNode left, ExpressionNode right) : ExpressionNode
{
    public ExpressionNode Left { get; set; } = left;
    public ExpressionNode Right { get; set; } = right;

    public override List<ASTNode> GetChildNodes() => MakeList(Left, Right);
}
