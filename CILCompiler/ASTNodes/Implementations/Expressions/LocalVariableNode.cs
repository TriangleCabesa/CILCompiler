﻿using CILCompiler.ASTNodes.Interfaces;
using CILCompiler.ASTVisitors.Interfaces;

namespace CILCompiler.ASTNodes.Implementations;

public record LocalVariableNode(string Name, IValueAccessorNode ValueAccessor, int DeclaredPosition) : ILocalVariableNode
{
    public object Value { get => ValueAccessor.GetValue(); set => ValueAccessor.SetValue((value as IExpressionNode)!); }
    public Type Type { get => Value.GetType(); }
    public string Expression { get => $"{Type.Name} {Name} = {Value};"; }

    public T Accept<T>(INodeVisitor<T> visitor) =>
        visitor.VisitLocalVariable(this);

    public void Accept(INodeVisitor visitor) =>
        visitor.VisitLocalVariable(this);
}

public record LocalVariableNode<T>(string Name, T TypedValue, int DeclaredPosition) : ILocalVariableNode<T> where T : class
{
    public Type Type { get => typeof(T); }
    public object Value { get => Value!; set => throw new NotImplementedException(); }
    public string Expression { get => $"{Type.Name} {Name} = {Value};"; }

    public TResult Accept<TResult>(INodeVisitor<TResult> visitor) =>
        visitor.VisitLocalVariable(this);

    public void Accept(INodeVisitor visitor) =>
        visitor.VisitLocalVariable(this);
}
