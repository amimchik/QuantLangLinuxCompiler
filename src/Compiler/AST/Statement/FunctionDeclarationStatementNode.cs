using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Statement;

public class FunctionDeclarationStatementNode(string name,
    List<VariableDeclarationStatementNode> args, QLType returnType, BlockStatementNode body) : StatementNode
{
    public string Name { get; set; } = name;
    public List<VariableDeclarationStatementNode> Args { get; set; } = args;
    public QLType ReturnType { get; set; } = returnType;
    public BlockStatementNode Body { get; set; } = body;

    public override NodeType Type => throw new NotImplementedException();

    public override T Accept<T>(IVisitor<T> visitor) => visitor.Visit(this);

    public override List<ASTNode> GetChildNodes()
    {
        List<ASTNode> nodes = [];

        nodes.AddRange(Args);
        nodes.Add(Body);

        return nodes;
    }
    public override string ToString() => $"{GetType()}[{string.Join("; ", GetChildNodes())}]";
}
