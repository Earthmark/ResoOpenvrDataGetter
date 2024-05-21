using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class IndexOfRole : ValueFunctionNode<ExecutionContext, uint>
{
    public ValueInput<ETrackedControllerRole> Role;

    protected override uint Compute(ExecutionContext context)
    {
        var role = Role.Evaluate(context);
        return OpenVR.System?.GetTrackedDeviceIndexForControllerRole(role) ?? uint.MaxValue;
    }
}
