using FrooxEngine;
using FrooxEngine.ProtoFlux;
using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using System;
using Valve.VR;

namespace OpenvrDataGetter.Components;

[Grouping("Ad-Ons.IndexOfRole")]
[Category(new string[] { "ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons/IndexOfRole" })]
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
