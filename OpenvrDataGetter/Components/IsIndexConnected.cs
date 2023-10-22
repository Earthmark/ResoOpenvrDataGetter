using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

public class IsIndexConnected : TrackedDeviceData<bool>
{
    public static IsIndexConnected __New()
    {
        return new IsIndexConnected();
    }

    public override Type NodeType => typeof(Nodes.IsIndexConnected);

    public Nodes.IsIndexConnected TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.IsIndexConnected()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.IsIndexConnected writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.IsIndexConnected));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
