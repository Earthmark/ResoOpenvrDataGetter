using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.ClassOfIndex")]
public class ClassOfIndex : TrackedDeviceData<ETrackedDeviceClass>
{
    protected override ETrackedDeviceClass Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        return OpenVR.System.GetTrackedDeviceClass(index);
    }
}
