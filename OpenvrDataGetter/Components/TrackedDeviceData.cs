using FrooxEngine;
using FrooxEngine.ProtoFlux;
using ProtoFlux.Core;

namespace OpenvrDataGetter.Components;

public abstract class TrackedDeviceData<T, TNode> : OVRValueFunctionNode<T, TNode> where T : unmanaged where TNode : class, INode, new()
{
    public readonly SyncRef<INodeValueOutput<uint>> Index;

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
                return Index;
            default:
                index -= 1;
                return null;
        }
    }
}
