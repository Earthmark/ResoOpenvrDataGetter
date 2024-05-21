using Elements.Core;
using FrooxEngine;
using Valve.VR;

namespace OpenvrDataGetter.Components;

[Category("ProtoFlux/Runtimes/Execution/Nodes/Ad-Ons")]
public class DevicePropertyArrayMatrix3x4 : DevicePropertyArrayBase<HmdMatrix34_t, Matrix3x4ArrayDeviceProperty, float4x4, Nodes.DevicePropertyArrayMatrix3x4>
{
}
