using ProtoFlux.Core;
using System;

namespace OpenvrDataGetter.Components;

public abstract class DevicePropertyArray<T, P, TNode> : DevicePropertyArrayBase<T, P, T, TNode> where T : unmanaged where P : unmanaged, Enum where TNode : class, INode, new()
{
}
