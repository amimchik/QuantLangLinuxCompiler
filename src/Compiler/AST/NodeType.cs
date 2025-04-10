namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;

public enum NodeType
{
    NumberLiteral,
    StringLiteral,

    FunctionCallExpression,
    VariableCallExpression,
    StructMemberExpression,

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
    IndexExpression,

    ExpressionStatement,

    AssignStatement,
    VariableDeclarationStatement,
    FunctionDeclarationStatement,
    BlockStatement,

    StructDeclarationStatement,
    ReturnStatement,

    AsmStatement,

    IfElseStatement,
    WhileStatement,
}