using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.RoleOfIndex")]
public class RoleOfIndex : TrackedDeviceData<ETrackedControllerRole>
{
    protected override ETrackedControllerRole Compute(ExecutionContext context)
    {
        uint index = IndexCompute(context);
        return OpenVR.System.GetControllerRoleForTrackedDeviceIndex(index);
    }
}