using Elements.Core;
using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class DevicePropertyMatrix3x4 : DeviceProperty<float4x4, Matrix3x4DeviceProperty>
{
    protected override float4x4 Compute(ExecutionContext context)
    {
        uint index = Index.Evaluate(context);
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        return Converter.HmdMatrix34ToFloat4x4(OpenVR.System?.GetMatrix34TrackedDeviceProperty(index, (ETrackedDeviceProperty)Prop.Evaluate(context), ref error) ?? new());
    }
}
