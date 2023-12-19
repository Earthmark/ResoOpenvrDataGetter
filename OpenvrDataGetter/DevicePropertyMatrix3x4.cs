using Valve.VR;

namespace OpenvrDataGetter;

public enum Matrix3x4DeviceProperty
{
    Prop_StatusDisplayTransform = ETrackedDeviceProperty.Prop_StatusDisplayTransform_Matrix34,
    Prop_CameraToHeadTransform = ETrackedDeviceProperty.Prop_CameraToHeadTransform_Matrix34,
    Prop_ImuToHeadTransform = ETrackedDeviceProperty.Prop_ImuToHeadTransform_Matrix34
}
