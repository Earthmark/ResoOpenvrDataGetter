using FrooxEngine;
using FrooxEngine.ProtoFlux;

namespace OpenvrDataGetter.Components;

[Category("ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons")]
public class DevicePropertyString : OVRObjectFunctionNode<string, Nodes.DevicePropertyString>
{
    public readonly SyncRef<INodeValueOutput<uint>> Index;
    public readonly SyncRef<INodeValueOutput<StringDeviceProperty>> Prop;

    public override int NodeInputCount => base.NodeInputCount + 2;

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
            case 1:
                return Prop;
            default:
                index -= 2;
                return null;
        }
    }
}
