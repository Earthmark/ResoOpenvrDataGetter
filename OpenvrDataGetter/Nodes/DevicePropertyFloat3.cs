using Elements.Core;
using ProtoFlux.Runtimes.Execution;
using System;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public class DevicePropertyFloat3 : DeviceProperty<float3, Float3DeviceProperty>
{
    protected override float3 Compute(ExecutionContext context)
    {
        uint index = Index.Evaluate(context);
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success; //its not clear the proper way to read vec3 props. this spits out reasonable data
        var Float3 = new float3[1];
        unsafe
        {
            fixed (float3* pFloat3 = Float3)
            {
                OpenVR.System.GetArrayTrackedDeviceProperty(index, (ETrackedDeviceProperty)Prop.Evaluate(context), 0, (IntPtr)pFloat3, (uint)sizeof(float3), ref error);
            }
        }
        return Float3[0];
        throw new NotImplementedException();
    }
}
