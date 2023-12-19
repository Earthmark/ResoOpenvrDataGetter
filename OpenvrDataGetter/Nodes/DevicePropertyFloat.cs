using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class DevicePropertyFloat : DeviceProperty<float, FloatDeviceProperty>
{
    protected override float Compute(ExecutionContext context)
    {
        uint index = Index.Evaluate(context);
        FloatDeviceProperty prop = Prop.Evaluate(context);
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        return OpenVR.System.GetFloatTrackedDeviceProperty(index, (ETrackedDeviceProperty)prop, ref error);
    }
}
