using ProtoFlux.Core;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.DevicePropertyArrayFloat")]
public class DevicePropertyArrayFloat : DevicePropertyArray<float, FloatArrayDeviceProperty>
{
}

