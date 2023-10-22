using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.IndexOfRole")]
public class IndexOfRole : ValueFunctionNode<ExecutionContext, uint>
{
    public ValueArgument<ETrackedControllerRole> Role;

    protected override uint Compute(ExecutionContext context)
    {
        var role = 0.ReadValue<ETrackedControllerRole>(context);
        return OpenVR.System.GetTrackedDeviceIndexForControllerRole(role);
    }
}
