using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;

namespace OpenvrDataGetter.Nodes;

public abstract class TrackedDeviceData<T> : ValueFunctionNode<ExecutionContext, T> where T : unmanaged
{
    public ValueArgument<uint> Index;

    protected uint IndexCompute(ExecutionContext context)
    {
        return 0.ReadValue<uint>(context);
    }
}
