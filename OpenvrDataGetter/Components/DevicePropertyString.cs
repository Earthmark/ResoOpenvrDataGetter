using FrooxEngine;
using FrooxEngine.ProtoFlux;
using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using System;

namespace OpenvrDataGetter.Components;

[Grouping("Ad-Ons.DevicePropertyString")]
[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons/DevicePropertyString" })]
public class DevicePropertyString : FrooxEngine.ProtoFlux.Runtimes.Execution.ObjectFunctionNode<ExecutionContext, string>
{
    public SyncRef<INodeValueOutput<uint>> Index;
    public SyncRef<INodeValueOutput<StringDeviceProperty>> Prop;

    public override int NodeInputCount => base.NodeInputCount + 2;

    public override Type NodeType => typeof(Nodes.DevicePropertyString);

    public Nodes.DevicePropertyString TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.DevicePropertyString()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.DevicePropertyString writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.DevicePropertyString));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }

    protected override void InitializeSyncMembers()
    {
        base.InitializeSyncMembers();
        Index = new SyncRef<INodeValueOutput<uint>>();
        Prop = new SyncRef<INodeValueOutput<StringDeviceProperty>>();
    }

    public override ISyncMember GetSyncMember(int index)
    {
        return index switch
        {
            0 => persistent,
            1 => updateOrder,
            2 => EnabledField,
            3 => Index,
            4 => Prop,
            _ => throw new ArgumentOutOfRangeException(),
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
                return Index;
            case 1:
                return Prop;
            default:
                index -= 2;
                return null;
        }
    }

    public static DevicePropertyString __New()
    {
        return new DevicePropertyString();
    }
}
