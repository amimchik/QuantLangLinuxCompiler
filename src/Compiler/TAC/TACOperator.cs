namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.TAC;

public enum TACOperator
{
    ToI32,
    ToI64,
    ToI16,
    ToChar,
    ToFloat,
    ToDouble,
    ToPtr,

    IPlus,
    IMinus,
    IMul,
    IDiv,
    IModullo,

    FPlus,
    FMinus,
    FMul,
    FDiv,
    FModullo,

    DPlus,
    DMinus,
    DMul,
    DDiv,
    DModullo,

    Deref,
    Ampersand,

    ASMCode,
}