using FrooxEngine;
using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

[Grouping("Ad-Ons.DevicePropertyArrayBool")]
[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons/DevicePropertyArrayBool" })]
public class DevicePropertyArrayBool : DevicePropertyArrayBase<byte, BoolArrayDeviceProperty, bool>
{
    public static DevicePropertyArrayBool __New()
    {
        return new DevicePropertyArrayBool();
    }

    public override Type NodeType => typeof(Nodes.DevicePropertyArrayBool);

    public Nodes.DevicePropertyArrayBool TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.DevicePropertyArrayBool()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyArrayBool writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyArrayBool));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
