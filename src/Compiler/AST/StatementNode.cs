namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;

public abstract class StatementNode : ASTNode
{
    public abstract override List<ASTNode> GetChildNodes();
    public abstract override NodeType Type { get; }
    public abstract override T Accept<T>(IVisitor<T> visitor);
}
