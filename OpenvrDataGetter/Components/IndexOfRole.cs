using FrooxEngine.ProtoFlux;
using FrooxEngine;
using ProtoFlux.Core;
using Valve.VR;
using ProtoFlux.Runtimes.Execution;
using System;

namespace OpenvrDataGetter.Components;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
public class IndexOfRole : FrooxEngine.ProtoFlux.Runtimes.Execution.ValueFunctionNode<ExecutionContext, uint>
{
    public static IndexOfRole __New()
    {
        return new IndexOfRole();
    }

    public override Type NodeType => typeof(Nodes.IndexOfRole);

    public SyncRef<INodeValueOutput<ETrackedControllerRole>> Role;

    public override int NodeInputCount => base.NodeInputCount + 1;

    protected override void InitializeSyncMembers()
    {
        base.InitializeSyncMembers();
        Role = new SyncRef<INodeValueOutput<ETrackedControllerRole>>();
    }

    public override ISyncMember GetSyncMember(int index)
    {
        return index switch
        {
            0 => persistent,
            1 => updateOrder,
            2 => EnabledField,
            3 => Role,
            _ => throw new ArgumentOutOfRangeException(),
        };
    }

    public Nodes.IndexOfRole TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.IndexOfRole()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        if (node is Nodes.IndexOfRole writeValueToGlobal)
        {
            TypedNodeInstance = writeValueToGlobal;
            return;
        }

        throw new ArgumentException("Node instance is not of type " + typeof(Nodes.IndexOfRole));
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
