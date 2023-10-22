using Elements.Core;
using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

public class DevicePropertyMatrix3x4 : DeviceProperty<float4x4, Matrix3x4DeviceProperty>
{
    public static DevicePropertyMatrix3x4 __New()
    {
        return new DevicePropertyMatrix3x4();
    }

    public override Type NodeType => typeof(Nodes.DevicePropertyMatrix3x4);

    public Nodes.DevicePropertyMatrix3x4 TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.DevicePropertyMatrix3x4()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyMatrix3x4 writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyMatrix3x4));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
