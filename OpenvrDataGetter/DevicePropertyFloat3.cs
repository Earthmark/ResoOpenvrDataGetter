using Valve.VR;

namespace OpenvrDataGetter
{
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
