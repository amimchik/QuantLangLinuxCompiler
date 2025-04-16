using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;

public class QLType(QLTypePrimitive type)
{
    public QLTypePrimitive PrimitiveType { get; set; } = type;
    public QLType? PointerType { get; set; } = null;
    public string? StructName { get; set; } = null;
    public int? StaticArrayLength { get; set; } = null;
    public QLType(QLTypePrimitive type, QLType ptrType) : this(type) => PointerType = ptrType;
    public QLType(QLTypePrimitive type, string structName) : this(type) => StructName = structName;
    public QLType(QLTypePrimitive type, int staticArrayLength) : this(type) => StaticArrayLength = staticArrayLength;
    public override string ToString()
    {
        if (PrimitiveType == QLTypePrimitive.Pointer)
        {
            return $"*{PointerType}";
        }
        if (PrimitiveType == QLTypePrimitive.Struct)
        {
            return $"struct {StructName}";
        }
        return PrimitiveType.ToString().ToLower();
    }
}
