using FrooxEngine;
using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

[Grouping("Ad-Ons.DevicePropertyArrayInt")]
[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons/DevicePropertyArrayInt" })]
public class DevicePropertyArrayInt : DevicePropertyArray<int, IntArrayDeviceProperty>
{
    public static DevicePropertyArrayInt __New()
    {
        return new DevicePropertyArrayInt();
    }

    public override Type NodeType => typeof(Nodes.DevicePropertyArrayInt);

    public Nodes.DevicePropertyArrayInt TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.DevicePropertyArrayInt()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyArrayInt writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyArrayInt));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}

