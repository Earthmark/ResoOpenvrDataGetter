using Elements.Core;
using ProtoFlux.Core;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.DevicePropertyArrayFloat4")]
public class DevicePropertyArrayFloat4 : DevicePropertyArray<float4, Float4ArrayDeviceProperty>
{
}

