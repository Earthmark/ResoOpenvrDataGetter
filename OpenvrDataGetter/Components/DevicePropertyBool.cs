using FrooxEngine;
using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

[Grouping("Ad-Ons.DevicePropertyBool")]
[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons/DevicePropertyBool" })]
public class DevicePropertyBool : DeviceProperty<bool, BoolDeviceProperty>
{
    public static DevicePropertyBool __New()
    {
        return new DevicePropertyBool();
    }

    public override Type NodeType => typeof(Nodes.DevicePropertyBool);

    public Nodes.DevicePropertyBool TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.DevicePropertyBool()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyBool writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyBool));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
