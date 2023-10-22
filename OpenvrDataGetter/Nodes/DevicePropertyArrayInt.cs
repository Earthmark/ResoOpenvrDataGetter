using ProtoFlux.Core;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.DevicePropertyArrayInt")]
public class DevicePropertyArrayInt : DevicePropertyArray<int, IntArrayDeviceProperty>
{
}
