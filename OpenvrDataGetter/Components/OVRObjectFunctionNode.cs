using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using System;

namespace OpenvrDataGetter.Components;

public abstract class OVRObjectFunctionNode<T, TNode> : FrooxEngine.ProtoFlux.Runtimes.Execution.ObjectFunctionNode<ExecutionContext, T> where TNode : class, INode, new()
{
    public override Type NodeType => typeof(TNode);

    public TNode TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new TNode()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        TypedNodeInstance = (TNode)node;
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
