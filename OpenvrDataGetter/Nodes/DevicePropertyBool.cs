using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class DevicePropertyBool : DeviceProperty<bool, BoolDeviceProperty>
{
    protected override bool Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        return OpenVR.System.GetBoolTrackedDeviceProperty(index, (ETrackedDeviceProperty)PropCompute(context), ref error);
    }
}
