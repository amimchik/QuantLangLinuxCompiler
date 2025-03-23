namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.TAC;

public enum TACType
{
    I32,
    I64,
    I16,
    Char,
    Float,
    Double,
    PTR
}

public static class TACTypeConverter
{
    public static string TypeToString(TACType type)
    {
        switch (type)
        {
            case TACType.I32:
                return "i32";
            case TACType.I64:
                return "i64";
            case TACType.I16:
                return "i16";
            case TACType.Char:
                return "char";
            case TACType.Float:
                return "float";
            case TACType.Double:
                return "double";
            case TACType.PTR:
                return "ptr";
            default:
                return "";
        }
    }
}