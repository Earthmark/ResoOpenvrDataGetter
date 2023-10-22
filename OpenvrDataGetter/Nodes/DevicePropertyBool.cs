using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.DevicePropertyBool")]
public class DevicePropertyBool : DeviceProperty<bool, BoolDeviceProperty>
{
    protected override bool Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        return OpenVR.System.GetBoolTrackedDeviceProperty(index, (ETrackedDeviceProperty)PropCompute(context), ref error);
    }
}
