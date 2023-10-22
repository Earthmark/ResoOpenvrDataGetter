using Elements.Core;
using FrooxEngine;
using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

[Grouping("Ad-Ons.DevicePropertyArrayFloat4")]
[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons/DevicePropertyArrayFloat4" })]
public class DevicePropertyArrayFloat4 : DevicePropertyArray<float4, Float4ArrayDeviceProperty>
{
    public static DevicePropertyArrayFloat4 __New()
    {
        return new DevicePropertyArrayFloat4();
    }

    public override Type NodeType => typeof(Nodes.DevicePropertyArrayFloat4);

    public Nodes.DevicePropertyArrayFloat4 TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.DevicePropertyArrayFloat4()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyArrayFloat4 writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyArrayFloat4));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
