using Valve.VR;

namespace OpenvrDataGetter.Nodes;

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
