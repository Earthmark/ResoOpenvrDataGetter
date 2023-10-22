using FrooxEngine.ProtoFlux;
using FrooxEngine;
using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using System;

namespace OpenvrDataGetter.Components
{
    [NodeCategory("Add-Ons.OpenvrDataGetter")]
    public abstract class TrackedDeviceData<T> : FrooxEngine.ProtoFlux.Runtimes.Execution.ValueFunctionNode<ExecutionContext, T> where T : unmanaged
    {
        public SyncRef<INodeValueOutput<uint>> Index;

        public override int NodeInputCount => base.NodeInputCount + 1;

        protected override void InitializeSyncMembers()
        {
            base.InitializeSyncMembers();
            Index = new SyncRef<INodeValueOutput<uint>>();
        }

        public override ISyncMember GetSyncMember(int index)
        {
            return index switch
            {
                0 => persistent,
                1 => updateOrder,
                2 => EnabledField,
                3 => Index,
                _ => throw new ArgumentOutOfRangeException(),
            };
        }
    }
}
