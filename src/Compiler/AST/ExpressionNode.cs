using System;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST;

public abstract class ExpressionNode : ASTNode
{
    public abstract override List<ASTNode> GetChildNodes();
    public abstract override NodeType Type { get; }
    public abstract override T Accept<T>(IVisitor<T> visitor);
    public abstract override bool IsLeftHandSide();
}
