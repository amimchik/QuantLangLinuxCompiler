using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.LHS;

public class LHSNode(string name)
{
    public string VariableName { get; set; } = name;
}
