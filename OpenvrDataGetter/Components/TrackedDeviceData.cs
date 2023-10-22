using FrooxEngine;
using FrooxEngine.ProtoFlux;
using ProtoFlux.Runtimes.Execution;
using System;

namespace OpenvrDataGetter.Components;

public abstract class TrackedDeviceData<T> : FrooxEngine.ProtoFlux.Runtimes.Execution.ValueFunctionNode<ExecutionContext, T> where T : unmanaged
{
    public SyncRef<INodeValueOutput<uint>> Index;

    public override int NodeInputCount => base.NodeInputCount + 1;

    protected override void InitializeSyncMembers()
    {
        base.InitializeSyncMembers();
        Index = new SyncRef<INodeValueOutput<uint>>();
    }

    protected override ISyncRef GetInputInternal(ref int index)
    {
        ISyncRef inputInternal = base.GetInputInternal(ref index);
        if (inputInternal != null)
        {
            return inputInternal;
        }

        switch (index)
        {
            case 0:
                return Index;
            default:
                index -= 1;
                return null;
        }
    }

    public override ISyncMember GetSyncMember(int index)
    {
        return index switch
        {
            0 => persistent,
            1 => updateOrder,
            2 => EnabledField,
            3 => Index,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }
}
