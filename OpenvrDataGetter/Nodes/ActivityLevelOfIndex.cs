using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class ActivityLevelOfIndexNode : TrackedDeviceData<EDeviceActivityLevel>
{
    protected override EDeviceActivityLevel Compute(ExecutionContext context)
    {
        uint index = Index.Evaluate(context);
        return OpenVR.System.GetTrackedDeviceActivityLevel(index);
    }
}