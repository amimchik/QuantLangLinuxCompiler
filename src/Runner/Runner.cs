using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.ASTBuilding;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.Parsing.Lexing;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.Preprocessing;
using org.amimchik.QuantLangLinuxCompiler.src.Compiler.TAC;

namespace org.amimchik.QuantLangLinuxCompiler.src.Runner;

public class Runner
{
    public static void Main(string[] args)
    {
        List<TACInstruction> tacs = [];
        tacs.Add(new TACInstruction
        (
            new TACOperand(TACType.I32, "sigma"),
            new TACOperand(TACType.I32, "23"),
            TACOperator.IPlus,
            new TACOperand(TACType.I32, "55")
        ));

        foreach (var tac in tacs)
        {
            Console.WriteLine(tac);
        }
    }
}