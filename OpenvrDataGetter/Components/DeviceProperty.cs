using FrooxEngine;
using FrooxEngine.ProtoFlux;
using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

public abstract class DeviceProperty<T, P, TNode> : TrackedDeviceData<T, TNode> where T : unmanaged where P : unmanaged, Enum where TNode : class, INode, new()
{
    public readonly SyncRef<INodeValueOutput<P>> Prop;

    public override int NodeInputCount => base.NodeInputCount + 1;

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
