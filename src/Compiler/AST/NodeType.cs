namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;

public enum NodeType
{
    NumberLiteral,
    StringLiteral,

    AddExpression,
    SubstractExpression,
    MulExpression,
    DivExpression,
    ModExpression,
    GTExpression,
    LTExpression,
    GTEQExpression,
    LTEQExpression,
    EQExpression,
    NTEQExpression,
    ANDExpression,
    ORExpression,
    BANDExpression,
    BORExpression,
    XORExpression,

    NOTExpression,
    DerefExpression,
    AddrExpression,

    LHSDeref,
    LHSAssign,
    LHSMember,

    AssignStatement,
    VariableDeclarationStatement,
}