using Elements.Core;
using ProtoFlux.Runtimes.Execution;
using System;
using Valve.VR;

namespace OpenvrDataGetter.Nodes
{
    public class DevicePropertyFloat3 : DeviceProperty<float3, Float3DeviceProperty>
    {
        protected override float3 Compute(ExecutionContext context)
        {
            uint index = IndexCompute(context);
            ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success; //its not clear the proper way to read vec3 props. this spits out reasonable data
            var Float3 = new float3[1];
            unsafe
            {
                fixed (float3* pFloat3 = Float3)
                {
                    OpenVR.System.GetArrayTrackedDeviceProperty(index, (ETrackedDeviceProperty)PropCompute(context), 0, (IntPtr)pFloat3, (uint)sizeof(float3), ref error);
                }
            }
            return Float3[0];
            throw new NotImplementedException();
        }
    }
    public enum Float3DeviceProperty
    {
        Prop_ImuFactoryGyroBias = ETrackedDeviceProperty.Prop_ImuFactoryGyroBias_Vector3,
        Prop_ImuFactoryGyroScale = ETrackedDeviceProperty.Prop_ImuFactoryGyroScale_Vector3,
        Prop_ImuFactoryAccelerometerBias = ETrackedDeviceProperty.Prop_ImuFactoryAccelerometerBias_Vector3,
        Prop_ImuFactoryAccelerometerScale = ETrackedDeviceProperty.Prop_ImuFactoryAccelerometerScale_Vector3,
        Prop_DisplayColorMultLeft = ETrackedDeviceProperty.Prop_DisplayColorMultLeft_Vector3,
        Prop_DisplayColorMultRight = ETrackedDeviceProperty.Prop_DisplayColorMultRight_Vector3
    }
}
