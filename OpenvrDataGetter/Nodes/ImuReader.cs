using Elements.Core;
using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using System;
using System.Threading;
using Valve.VR;
using ExecContext = FrooxEngine.ProtoFlux.FrooxEngineContext;

namespace OpenvrDataGetter.Nodes;

public class ImuReader : VoidNode<ExecContext>, IDisposable
{
    public readonly ObjectInput<string> DevicePath;

    [PossibleContinuations("OnOpened", "OnFail")]
    public readonly Operation Open;
    [PossibleContinuations("OnClosed", "OnFail")]
    public readonly Operation Close;

    public Continuation OnOpened;
    public Continuation OnClosed;
    [ContinuouslyChanging]
    public readonly ValueOutput<bool> isOpened;

    public Call OnFail;
    [ContinuouslyChanging]
    public readonly ValueOutput<ImuErrorCode> FailReason;

    public Call OnData;
    public readonly ValueOutput<double> fSampleTime;
    public readonly ValueOutput<double3> vAccel;
    public readonly ValueOutput<double3> vGyro;
    public readonly ValueOutput<Imu_OffScaleFlags> unOffScaleFlags;

    public ImuReader()
    {
        Open = new Operation(this, 0);
        Close = new Operation(this, 1);
        isOpened = new ValueOutput<bool>(this);
        FailReason = new ValueOutput<ImuErrorCode>(this);
        fSampleTime = new ValueOutput<double>(this);
        vAccel = new ValueOutput<double3>(this);
        vGyro = new ValueOutput<double3>(this);
        unOffScaleFlags = new ValueOutput<Imu_OffScaleFlags>(this);
    }

    ulong pulBuffer = 0;
    Thread thread = null;

    public IOperation DoOpen(ExecContext c)
    {
        string path = DevicePath.Evaluate(c);
        if (string.IsNullOrEmpty(path))
        {
            return Fail(ImuErrorCode.PathIsNullOrEmpty, c);
        }
        if (OpenVR.IOBuffer == null)
        {
            return Fail(ImuErrorCode.OpenVrNotFound, c);
        }
        if (pulBuffer == 0)
        {
            try
            {
                EIOBufferError errorcode;
                unsafe
                {
                    errorcode = OpenVR.IOBuffer.Open(path, EIOBufferMode.Read, (uint)sizeof(ImuSample_t), 0, ref pulBuffer);
                }
                if (errorcode != EIOBufferError.IOBuffer_Success)
                {
                    return Fail((ImuErrorCode)errorcode, c);
                }
                NodeContextPath nodePath = c.CaptureContextPath();
                c.GetEventDispatcher(out var dispatcher);
                thread = new Thread(() => readLoop(nodePath, dispatcher));
                thread.Start();
            }
            catch (Exception e)
            {
                UniLog.Log(e);
                return Fail(ImuErrorCode.UnknownException, c);
            }
            isOpened.Write(true, c);
            return OnOpened.Target;
        }
        else
        {
            return Fail(ImuErrorCode.AlreadyOpened, c);
        }
    }

    public IOperation DoClose(ExecContext c)
    {
        if (pulBuffer == 0)
        {
            return Fail(ImuErrorCode.AlreadyClosed, c);
        }
        var error = OpenVR.IOBuffer.Close(pulBuffer);
        if (error != EIOBufferError.IOBuffer_Success)
        {
            return Fail((ImuErrorCode)error, c);
        }
        try
        {
            thread.Abort();
        }
        catch (Exception e)
        {
            UniLog.Log(e);
            return Fail(ImuErrorCode.UnknownException, c);
        }
        thread = null;
        pulBuffer = 0;
        isOpened.Write(false, c);
        return OnClosed.Target;
    }

    private void HandleImuData(ExecContext c, object obj)
    {
        ImuSample_t sample = (ImuSample_t)obj;

        fSampleTime.Write(sample.fSampleTime, c);
        vAccel.Write(Converter.HmdVector3ToDobble3(sample.vAccel), c);
        vGyro.Write(Converter.HmdVector3ToDobble3(sample.vGyro), c);
        unOffScaleFlags.Write((Imu_OffScaleFlags)sample.unOffScaleFlags, c);

        OnData.Execute(c);
    }

    void readLoop(NodeContextPath path, ExecutionEventDispatcher<ExecContext> c)
    {
        EIOBufferError failReason = EIOBufferError.IOBuffer_Success;
        const uint arraySize = 10;
        ImuSample_t[] samples = new ImuSample_t[arraySize];
        try
        {
            while (true)
            {
                uint punRead = 0;
                unsafe
                {
                    fixed (ImuSample_t* pSamples = samples)
                    {
                        failReason = OpenVR.IOBuffer.Read(pulBuffer, (IntPtr)pSamples, (uint)sizeof(ImuSample_t) * arraySize, ref punRead);
                    }
                }
                if (failReason != EIOBufferError.IOBuffer_Success)
                {
                    throw new Exception("read retuned: " + failReason.ToString());
                }
                int unreadSize = new();
                unsafe
                {
                    unreadSize = (int)punRead / sizeof(ImuSample_t);
                }
                for (int i = 0; i < unreadSize; i++)
                {
                    c.ScheduleEvent(path, HandleImuData, samples[i]);
                }
                if (unreadSize == 0)
                {
                    Thread.Sleep(10);
                }
            }
        }
        catch (Exception e)
        {
            UniLog.Log(e);

            thread = null;
            c.ScheduleEvent(path, c =>
            {
                isOpened.Write(false, c);
                Fail(failReason == EIOBufferError.IOBuffer_Success ? ImuErrorCode.UnknownException : (ImuErrorCode)failReason, c);
                OnFail.Execute(c);
            });
            OpenVR.IOBuffer.Close(pulBuffer);
            pulBuffer = 0;
        }
    }

    void IDisposable.Dispose()
    {
        if (thread != null)
        {
            thread.Abort();
            thread = null;
        }
        if (pulBuffer != 0)
        {
            OpenVR.IOBuffer.Close(pulBuffer);
        }
    }

    private IOperation Fail(ImuErrorCode error, ExecContext c)
    {
        FailReason.Write(error, c);
        return OnFail.Target;
    }
}
