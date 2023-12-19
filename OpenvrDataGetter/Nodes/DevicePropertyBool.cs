using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class DevicePropertyBool : DeviceProperty<bool, BoolDeviceProperty>
{
    protected override bool Compute(ExecutionContext context)
    {
        uint index = Index.Evaluate(context);
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        return OpenVR.System?.GetBoolTrackedDeviceProperty(index, (ETrackedDeviceProperty)Prop.Evaluate(context), ref error) ?? false;
    }
}
