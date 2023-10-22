using FrooxEngine;
using FrooxEngine.ProtoFlux.Runtimes.Execution.Nodes.Operators;
using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

[Grouping("Add-Ons.OpenvrDataGetter")]
public class ClassOfIndex : TrackedDeviceData<ETrackedDeviceClass>
{
    protected override ETrackedDeviceClass Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        return OpenVR.System.GetTrackedDeviceClass(index);
    }
}
