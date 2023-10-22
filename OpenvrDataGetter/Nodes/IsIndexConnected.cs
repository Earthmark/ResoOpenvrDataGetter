using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.IsIndexConnected")]
public class IsIndexConnected : TrackedDeviceData<bool>
{
    protected override bool Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        return OpenVR.System.IsTrackedDeviceConnected(index);
    }
}
