using Elements.Core;
using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.DevicePropertyMatrix3x4")]
public class DevicePropertyMatrix3x4 : DeviceProperty<float4x4, Matrix3x4DeviceProperty>
{
    protected override float4x4 Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        return Converter.HmdMatrix34ToFloat4x4(OpenVR.System.GetMatrix34TrackedDeviceProperty(index, (ETrackedDeviceProperty)PropCompute(context), ref error));
    }
}
