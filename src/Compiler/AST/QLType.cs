using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;

public class QLType(QLTypePrimitive type, QLType? ptrType = null)
{
    public QLTypePrimitive PrimitiveType { get; set; } = type;
    public QLType? PointerType { get; set; } = ptrType;
}
