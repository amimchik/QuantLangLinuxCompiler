namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;

public abstract class ASTNode
{
    public abstract List<ASTNode> GetChildNodes();
    public abstract NodeType Type { get; }
    protected static List<ASTNode> MakeList(params ASTNode[] nodes) => [.. nodes];
    public abstract T Accept<T>(IVisitor<T> visitor);
}