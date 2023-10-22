using FrooxEngine;
using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

[Grouping("Ad-Ons.DevicePropertyUlong")]
[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons/DevicePropertyUlong" })]
public class DevicePropertyUlong : DeviceProperty<ulong, UlongDeviceProperty>
{
    public static DevicePropertyUlong __New()
    {
        return new DevicePropertyUlong();
    }

    public override Type NodeType => typeof(Nodes.DevicePropertyUlong);

    public Nodes.DevicePropertyUlong TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.DevicePropertyUlong()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyUlong writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyUlong));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
