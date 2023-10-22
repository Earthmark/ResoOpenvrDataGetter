using Elements.Core;
using ProtoFlux.Core;
using System;
using Valve.VR;

namespace OpenvrDataGetter.Components;

public class DevicePropertyArrayMatrix3x4 : DevicePropertyArrayBase<HmdMatrix34_t, Matrix3x4ArrayDeviceProperty, float4x4>
{
    public static DevicePropertyArrayMatrix3x4 __New()
    {
        return new DevicePropertyArrayMatrix3x4();
    }

    public override Type NodeType => typeof(Nodes.DevicePropertyArrayMatrix3x4);

    public Nodes.DevicePropertyArrayMatrix3x4 TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.DevicePropertyArrayMatrix3x4()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyArrayMatrix3x4 writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyArrayMatrix3x4));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
