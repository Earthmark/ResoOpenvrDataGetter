using FrooxEngine;
using FrooxEngine.ProtoFlux;
using System;

namespace OpenvrDataGetter.Components;

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
                return Prop;
            default:
                index -= 1;
                return null;
        }
    }
}
