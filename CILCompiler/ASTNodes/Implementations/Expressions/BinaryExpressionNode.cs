﻿using CILCompiler.ASTNodes.Interfaces;
using CILCompiler.ASTVisitors.Interfaces;

namespace CILCompiler.ASTNodes.Implementations.Expressions;

public record BinaryExpressionNode(IExpressionNode Left, IExpressionNode Right, string Operator) : IExpressionNode
{
    public string Expression => $"{Left.Expression} {Operator} {Right.Expression}";

    public T Accept<T>(INodeVisitor<T> visitor) =>
        visitor.VisitBinaryExpression(this);

    public void Accept(INodeVisitor visitor, NodeVisitOptions? options = null) =>
        visitor.VisitBinaryExpression(this, options);
}
