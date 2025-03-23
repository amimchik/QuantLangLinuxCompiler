namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.TAC;

public class TACInstruction(TACOperand result, TACOperand left, TACOperator op, TACOperand right)
{
    public TACOperand Result { get; set; } = result;
    public TACOperand Left { get; set; } = left;
    public TACOperator Operator { get; set; } = op;
    public TACOperand Right { get; set; } = right;
    public override string ToString() => $"{Result} = {Left} {Operator} {Right}";
}