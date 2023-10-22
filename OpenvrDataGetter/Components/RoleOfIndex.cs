using ProtoFlux.Core;
using System;
using Valve.VR;

namespace OpenvrDataGetter.Components;

public class RoleOfIndex : TrackedDeviceData<ETrackedControllerRole>
{
    public static RoleOfIndex __New()
    {
        return new RoleOfIndex();
    }

    public override Type NodeType => typeof(Nodes.RoleOfIndex);

    public Nodes.RoleOfIndex TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.RoleOfIndex()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.RoleOfIndex writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.RoleOfIndex));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}