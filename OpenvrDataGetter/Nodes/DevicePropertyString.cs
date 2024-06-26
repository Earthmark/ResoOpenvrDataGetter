﻿using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using System.Text;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class DevicePropertyString : ObjectFunctionNode<ExecutionContext, string>
{
    public ValueInput<uint> Index;
    public ValueInput<StringDeviceProperty> Prop;

    protected override string Compute(ExecutionContext context)
    {
        uint index = Index.Evaluate(context);
        ETrackedDeviceProperty property = (ETrackedDeviceProperty)Prop.Evaluate(context);
        ETrackedPropertyError pError = ETrackedPropertyError.TrackedProp_Success;

        uint stringTrackedDeviceProperty = OpenVR.System?.GetStringTrackedDeviceProperty(index, property, null, 0u, ref pError) ?? 0;
        if (stringTrackedDeviceProperty > 1)
        {
            var stringBuilder = new StringBuilder((int)stringTrackedDeviceProperty);
            OpenVR.System.GetStringTrackedDeviceProperty(index, property, stringBuilder, stringTrackedDeviceProperty, ref pError);
            return stringBuilder.ToString();
        }
        return "";
    }
}
