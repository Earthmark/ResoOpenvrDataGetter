using FrooxEngine;
using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

[Grouping("Ad-Ons.DevicePropertyArrayFloat")]
[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons/DevicePropertyArrayFloat" })]
public class DevicePropertyArrayFloat : DevicePropertyArray<float, FloatArrayDeviceProperty>
{
    public static DevicePropertyArrayFloat __New()
    {
        return new DevicePropertyArrayFloat();
    }

    public override Type NodeType => typeof(Nodes.DevicePropertyArrayFloat);

    public Nodes.DevicePropertyArrayFloat TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }
        return (TypedNodeInstance = new Nodes.DevicePropertyArrayFloat()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyArrayFloat writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyArrayFloat));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}

