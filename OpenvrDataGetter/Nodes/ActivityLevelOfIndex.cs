using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.ActivityLevelOfIndexNode")]
public class ActivityLevelOfIndexNode : TrackedDeviceData<EDeviceActivityLevel>
{
    protected override EDeviceActivityLevel Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        return OpenVR.System.GetTrackedDeviceActivityLevel(index);
    }
}