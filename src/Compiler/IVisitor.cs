using org.amimchik.QuantLangLinuxCompiler.src.Compiler.AST.Expression.BinaryExpression;

namespace org.amimchik.QuantLangLinuxCompiler.src.Compiler;

public interface IVisitor<T>
{
    T Visit(AddExpressionNode node);
}