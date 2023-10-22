using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using System;
using System.Runtime.InteropServices;
using Valve.VR;

namespace OpenvrDataGetter.Nodes;

public abstract class DevicePropertyArrayBase<T, P, R> : DeviceProperty<R, P> where R : unmanaged where T : unmanaged where P : unmanaged, Enum
{
    public ValueArgument<uint> ArrIndex;

    protected static readonly P DefaultValue = (P)Enum.GetValues(typeof(P)).GetValue(0);
    protected static readonly int structSize = Marshal.SizeOf<T>();
    protected static uint trueIndexFactor = 1;

    protected virtual R Reader(T[] apiVal, uint arrindex) => (R)(object)apiVal[arrindex];

    protected override R Compute(ExecutionContext context)
    {
        var arrindex = 2.ReadValue<uint>(context);
        if (arrindex == uint.MaxValue) return default;
        var length = (arrindex / trueIndexFactor) + 1;
        var memsize = length * structSize;
        if (memsize >= uint.MaxValue) return default;
        var devindex = IndexCompute(context);
        var prop = (ETrackedDeviceProperty)(object)PropCompute(context);
        ETrackedPropertyError error = ETrackedPropertyError.TrackedProp_Success;


        var arr = new T[length];
        unsafe
        {
            fixed (T* ptr = arr)
            {
                OpenVR.System.GetArrayTrackedDeviceProperty(devindex, prop, 0, (IntPtr)ptr, (uint)memsize, ref error);
            }
        }
        return Reader(arr, arrindex);

    }
}
