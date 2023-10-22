using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

public class DevicePropertyFloat : DeviceProperty<float, FloatDeviceProperty>
{
    public static DevicePropertyFloat __New()
    {
        return new DevicePropertyFloat();
    }

    public override Type NodeType => typeof(Nodes.DevicePropertyFloat);

    public Nodes.DevicePropertyFloat TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.DevicePropertyFloat()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyFloat writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyFloat));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
