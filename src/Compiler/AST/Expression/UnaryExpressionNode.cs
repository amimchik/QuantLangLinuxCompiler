namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression;

public abstract class UnaryExpressionNode(ExpressionNode operand) : ExpressionNode
{
    public ExpressionNode Operand { get; set; } = operand;
    public override List<ASTNode> GetChildNodes() => MakeList(Operand);
}
