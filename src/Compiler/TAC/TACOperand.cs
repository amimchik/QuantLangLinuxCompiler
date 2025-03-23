namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.TAC;

public class TACOperand(TACType type, string value)
{
    public string Value { get; set; } = value;
    public TACType Type { get; set; } = type;
    public override string ToString() => $"{Value}: {TACTypeConverter.TypeToString(Type)}";
}