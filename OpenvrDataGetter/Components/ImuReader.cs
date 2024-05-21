using Elements.Core;
using FrooxEngine;
using FrooxEngine.ProtoFlux;
using FrooxEngine.ProtoFlux.Runtimes.Execution;
using ProtoFlux.Core;
using System;
using Valve.VR;

namespace OpenvrDataGetter.Components;

[Category("ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons")]
public class ImuReader : VoidNode<FrooxEngineContext>
{
    public readonly SyncNodeOperation Open;
    public readonly SyncNodeOperation Close;

    public readonly SyncRef<INodeObjectOutput<string>> DevicePath;

    public readonly SyncRef<INodeOperation> OnOpened;
    public readonly SyncRef<INodeOperation> OnClosed;
    public readonly SyncRef<INodeOperation> OnFail;
    public readonly SyncRef<INodeOperation> OnData;

    public readonly NodeValueOutput<bool> isOpened;
    public readonly NodeValueOutput<ImuErrorCode> FailReason;

    public readonly NodeValueOutput<double> fSampleTime;
    public readonly NodeValueOutput<double3> vAccel;
    public readonly NodeValueOutput<double3> vGyro;
    public readonly NodeValueOutput<Imu_OffScaleFlags> unOffScaleFlags;

    public override int NodeImpulseCount => base.NodeImpulseCount + 4;

    public override int NodeOperationCount => base.NodeOperationCount + 2;

    public override int NodeInputCount => base.NodeInputCount + 1;

    public override int NodeOutputCount => base.NodeOutputCount + 6;

    protected override ISyncRef GetImpulseInternal(ref int index)
    {
        ISyncRef inter = base.GetImpulseInternal(ref index);
        if (inter != null) return inter;

        switch (index)
        {
            case 0: return OnOpened;
            case 1: return OnClosed;
            case 2: return OnFail;
            case 3: return OnData;
        }

        index -= 4;
        return null;
    }


    protected override INodeOperation GetOperationInternal(ref int index)
    {
        INodeOperation inter = base.GetOperationInternal(ref index);
        if (inter != null) return inter;

        switch (index)
        {
            case 0: return Open;
            case 1: return Close;
        }

        index -= 2;
        return null;
    }

    protected override ISyncRef GetInputInternal(ref int index)
    {
        ISyncRef inter = base.GetInputInternal(ref index);
        if (inter != null) return inter;

        switch (index)
        {
            case 0: return DevicePath;
        }

        index -= 1;
        return null;
    }

    protected override INodeOutput GetOutputInternal(ref int index)
    {
        INodeOutput inter = base.GetOutputInternal(ref index);
        if (inter != null) return inter;

        switch (index)
        {
            case 0: return isOpened;
            case 1: return FailReason;
            case 2: return fSampleTime;
            case 3: return vAccel;
            case 4: return vGyro;
            case 5: return unOffScaleFlags;
        }
        index -= 6;
        return null;
    }

    public override Type NodeType => typeof(Nodes.ImuReader);

    public Nodes.ImuReader TypedNodeInstance { get; private set; }

    public override INode NodeInstance => TypedNodeInstance;

    public override N Instantiate<N>()
    {
        if (TypedNodeInstance != null)
        {
            throw new InvalidOperationException("Node has already been instantiated");
        }

        return (TypedNodeInstance = new Nodes.ImuReader()) as N;
    }

    protected override void AssociateInstanceInternal(INode node)
    {
        TypedNodeInstance = (Nodes.ImuReader)node;
    }

    public override void ClearInstance()
    {
        TypedNodeInstance = null;
    }
}
