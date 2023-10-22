using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class RoleOfIndex : TrackedDeviceData<ETrackedControllerRole>
{
    protected override ETrackedControllerRole Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        return OpenVR.System.GetControllerRoleForTrackedDeviceIndex(index);
    }
}