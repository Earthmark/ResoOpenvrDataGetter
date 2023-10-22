using ProtoFlux.Core;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.DevicePropertyArrayBool")]
public class DevicePropertyArrayBool : DevicePropertyArrayBase<byte, BoolArrayDeviceProperty, bool>
{
    protected override bool Reader(byte[] apiVal, uint arrindex)
    {
        return (apiVal[arrindex / 8] & (byte)(1 << (int)(arrindex % 8))) != 0;
    }
    static DevicePropertyArrayBool()
    {
        trueIndexFactor = 8;
    }
}
