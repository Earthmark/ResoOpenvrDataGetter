using FrooxEngine.ProtoFlux;
using FrooxEngine;
using System;

namespace OpenvrDataGetter.Components;

public abstract class DevicePropertyArrayBase<T, P, R> : DeviceProperty<R, P> where R : unmanaged where T : unmanaged where P : unmanaged, Enum
{
    public SyncRef<INodeValueOutput<uint>> ArrIndex;

    public override int NodeInputCount => base.NodeInputCount + 1;

    protected override void InitializeSyncMembers()
    {
        base.InitializeSyncMembers();
        ArrIndex = new SyncRef<INodeValueOutput<uint>>();
    }

    public override ISyncMember GetSyncMember(int index)
    {
        return index switch
        {
            5 => ArrIndex,
            _ => base.GetSyncMember(index),
        };
    }
}
