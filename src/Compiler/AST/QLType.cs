using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;

public class QLType(QLTypePrimitive type)
{
    public QLTypePrimitive PrimitiveType { get; set; } = type;
    public QLType? PointerType { get; set; } = null;
    public string? StructName { get; set; } = null;
    public QLType(QLTypePrimitive type, QLType ptrType) : this(type) => PointerType = ptrType;
    public QLType(QLTypePrimitive type, string structName) : this(type) => StructName = structName;
}
