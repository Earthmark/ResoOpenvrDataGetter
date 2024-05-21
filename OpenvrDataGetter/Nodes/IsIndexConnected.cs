using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class IsIndexConnected : TrackedDeviceData<bool>
{
    protected override bool Compute(ExecutionContext context)
    {
        uint index = Index.Evaluate(context);
        return OpenVR.System?.IsTrackedDeviceConnected(index) ?? false;
    }
}
