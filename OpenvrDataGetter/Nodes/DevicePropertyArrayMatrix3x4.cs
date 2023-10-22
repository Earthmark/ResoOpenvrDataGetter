using Elements.Core;
using ProtoFlux.Core;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.DevicePropertyArrayMatrix3x4")]
public class DevicePropertyArrayMatrix3x4 : DevicePropertyArrayBase<HmdMatrix34_t, Matrix3x4ArrayDeviceProperty, float4x4>
{
    protected override float4x4 Reader(HmdMatrix34_t[] apiVal, uint arrindex) => Converter.HmdMatrix34ToFloat4x4(apiVal[arrindex]);
}
