using FrooxEngine;
using ProtoFlux.Core;
using System;
using Valve.VR;

namespace OpenvrDataGetter.Components;

[Grouping("Ad-Ons.ClassOfIndex")]
[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons/ClassOfIndex" })]
public class ClassOfIndex : TrackedDeviceData<ETrackedDeviceClass>
{
    public static ClassOfIndex __New()
    {
        return new ClassOfIndex();
    }

    public override Type NodeType => typeof(Nodes.ClassOfIndex);

    public Nodes.ClassOfIndex TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.ClassOfIndex()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.ClassOfIndex writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.ClassOfIndex));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
