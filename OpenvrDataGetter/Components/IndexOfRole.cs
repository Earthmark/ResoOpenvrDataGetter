using FrooxEngine;
using FrooxEngine.ProtoFlux;
using Valve.VR;

namespace OpenvrDataGetter.Components;

[Category("ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons")]
public class IndexOfRole : OVRValueFunctionNode<int, Nodes.IndexOfRole>
{
    public SyncRef<INodeValueOutput<ETrackedControllerRole>> Role;

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
                return Role;
            default:
                index -= 1;
                return null;
        }
    }
}
