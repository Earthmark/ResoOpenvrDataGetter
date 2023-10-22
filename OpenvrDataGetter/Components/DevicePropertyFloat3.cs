using Elements.Core;
using FrooxEngine;
using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

[Grouping("Ad-Ons.DevicePropertyFloat3")]
[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons/DevicePropertyFloat3" })]
public class DevicePropertyFloat3 : DeviceProperty<float3, Float3DeviceProperty>
{
    public static DevicePropertyFloat3 __New()
    {
        return new DevicePropertyFloat3();
    }

    public override Type NodeType => typeof(Nodes.DevicePropertyFloat3);

    public Nodes.DevicePropertyFloat3 TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.DevicePropertyFloat3()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyFloat3 writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyFloat3));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
