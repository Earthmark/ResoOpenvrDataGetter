using FrooxEngine;
using ProtoFlux.Core;
using System;
using Valve.VR;

namespace OpenvrDataGetter.Components;

[Grouping("Core.Operators.Add")]
[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Operators" })]
public class ActivityLevelOfIndexNode : TrackedDeviceData<EDeviceActivityLevel>
{
    public static ActivityLevelOfIndexNode __New()
    {
        return new ActivityLevelOfIndexNode();
    }

    public override Type NodeType => typeof(Nodes.ActivityLevelOfIndexNode);

    public Nodes.ActivityLevelOfIndexNode TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.ActivityLevelOfIndexNode()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.ActivityLevelOfIndexNode writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.ActivityLevelOfIndexNode));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}