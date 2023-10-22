using FrooxEngine.ProtoFlux;
using FrooxEngine;
using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
public abstract class DeviceProperty<T, P> : TrackedDeviceData<T> where T : unmanaged where P : unmanaged, Enum
{
    public SyncRef<INodeValueOutput<P>> Prop;

    public override int NodeInputCount => base.NodeInputCount + 1;

    protected override void InitializeSyncMembers()
    {
        base.InitializeSyncMembers();
        Prop = new SyncRef<INodeValueOutput<P>>();
    }

    public override ISyncMember GetSyncMember(int index)
    {
        return index switch
        {
            4 => Prop,
            _ => base.GetSyncMember(index),
        };
    }
}
