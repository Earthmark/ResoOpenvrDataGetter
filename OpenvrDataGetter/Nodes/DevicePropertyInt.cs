using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class DevicePropertyInt : DeviceProperty<int, IntDeviceProperty>
{
    protected override int Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        return OpenVR.System.GetInt32TrackedDeviceProperty(index, (ETrackedDeviceProperty)PropCompute(context), ref error);
    }
}
