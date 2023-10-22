using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class DevicePropertyUlong : DeviceProperty<ulong, UlongDeviceProperty>
{
    protected override ulong Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        ETrackedDeviceProperty prop = (ETrackedDeviceProperty)PropCompute(context);
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        return OpenVR.System.GetUint64TrackedDeviceProperty(index, prop, ref error);
    }
}
