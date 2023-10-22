using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using System;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
public abstract class DeviceProperty<T, P> : TrackedDeviceData<T> where T : unmanaged where P : unmanaged, Enum
{
    public ValueArgument<P> Prop;

    protected P PropCompute(ExecutionContext context)
    {
        return 1.ReadValue<P>(context);
    }
}
