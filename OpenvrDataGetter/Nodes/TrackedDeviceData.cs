using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;

namespace OpenvrDataGetter.Nodes;

public abstract class TrackedDeviceData<T> : ValueFunctionNode<ExecutionContext, T> where T : unmanaged
{
    public ValueInput<uint> Index;
}
