namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.TAC;

public class TACOperand(TACType type, string value)
{
    public TACOperandType OperandType;
    public string Name { get; set; } = value;
    public TACType Type { get; set; } = type;
    public override string ToString() => $"{Name}: {TACTypeConverter.TypeToString(Type)}";
}