using FrooxEngine.ProtoFlux;
using FrooxEngine;
using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using System;

namespace OpenvrDataGetter.Components;

public class DevicePropertyString : FrooxEngine.ProtoFlux.Runtimes.Execution.ObjectFunctionNode<ExecutionContext, string>
{
    public SyncRef<INodeValueOutput<uint>> Index;
    public SyncRef<INodeValueOutput<StringDeviceProperty>> Prop;

    public override int NodeInputCount => base.NodeInputCount + 2;

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

    public static DevicePropertyString __New()
    {
        return new DevicePropertyString();
    }

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
}
