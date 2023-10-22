using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.DevicePropertyFloat")]
public class DevicePropertyFloat : DeviceProperty<float, FloatDeviceProperty>
{
    protected override float Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        FloatDeviceProperty prop = PropCompute(context);
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;
        return OpenVR.System.GetFloatTrackedDeviceProperty(index, (ETrackedDeviceProperty)prop, ref error);
    }
}
