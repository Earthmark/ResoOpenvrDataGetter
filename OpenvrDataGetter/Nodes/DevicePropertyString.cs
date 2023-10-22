using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using System.Text;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

[NodeCategory("Add-Ons.OpenvrDataGetter")]
[NodeOverload("Add-Ons.OpenvrDataGetter.DevicePropertyString")]
public class DevicePropertyString : ObjectFunctionNode<ExecutionContext, string>
{
    public ValueArgument<uint> Index;
    public ValueArgument<StringDeviceProperty> Prop;

    protected override string Compute(ExecutionContext context)
    {
        uint index = 0.ReadValue<uint>(context);
        ETrackedDeviceProperty property = (ETrackedDeviceProperty)1.ReadValue<StringDeviceProperty>(context);
        ETrackedPropertyError pError = ETrackedPropertyError.TrackedProp_Success;
        StringBuilder stringBuilder = new(64);
        uint stringTrackedDeviceProperty = OpenVR.System.GetStringTrackedDeviceProperty(index, property, null, 0u, ref pError);
        if (stringTrackedDeviceProperty > 1)
        {
            stringBuilder = new StringBuilder((int)stringTrackedDeviceProperty);
            OpenVR.System.GetStringTrackedDeviceProperty(index, property, stringBuilder, stringTrackedDeviceProperty, ref pError);
        }
        return stringBuilder.ToString();
    }
}
