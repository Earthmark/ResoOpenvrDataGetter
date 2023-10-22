using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

public class DevicePropertyInt : DeviceProperty<int, IntDeviceProperty>
{
    public static DevicePropertyInt __New()
    {
        return new DevicePropertyInt();
    }

    public override Type NodeType => typeof(Nodes.DevicePropertyInt);

    public Nodes.DevicePropertyInt TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.DevicePropertyInt()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyInt writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyInt));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
