using Elements.Core;
using ProtoFlux.Core;
using ProtoFlux.Runtimes.Execution;
using ProtoFlux.Runtimes.Execution.Nodes.Actions;
using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Threading;
using Valve.VR;
using ExecContext = FrooxEngine.ProtoFlux.FrooxEngineContext;

namespace OpenvrDataGetter.Nodes;

public class ImuReader : UpdateBase
{
    public readonly Operation Open;
    public readonly Operation Close;

    public readonly ObjectInput<string> DevicePath;

    public Continuation OnOpened;
    public Continuation OnClosed;
    public Call OnFail;
    public Call OnData;

    [ContinuouslyChanging]
    public readonly ValueOutput<bool> isOpened;

    [ContinuouslyChanging]
    public readonly ValueOutput<ImuErrorCode> FailReason;

    public readonly ValueOutput<double> fSampleTime;
    public readonly ValueOutput<double3> vAccel;
    public readonly ValueOutput<double3> vGyro;
    public readonly ValueOutput<Imu_OffScaleFlags> unOffScaleFlags;

    private ObjectStore<ErrorCodeBox> _failReason;
    private ObjectStore<Thread> _thread;
    private ObjectStore<ulong> _pulBuffer;
    private ObjectStore<ConcurrentQueue<ImuSample_t>> _sampleBuffer;
    private ObjectStore<Stopwatch> _lastTime;
    private ObjectStore<int> _lastProcessed;

    private class ErrorCodeBox
    {
        public volatile ImuErrorCode FailReason;
    }

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

    protected override void ComputeOutputs(ExecContext c)
    {
        isOpened.Write(_thread.Read(c)?.IsAlive == true, c);
        FailReason.Write(_failReason.Read(c)?.FailReason ?? ImuErrorCode.AlreadyClosed, c);
    }

    protected override void RunUpdate(ExecContext c)
    {
        var buffer = _sampleBuffer.Read(c);
        if (buffer == null)
        {
            return;
        }
        var processed = 0;
        while (buffer.TryDequeue(out var sample))
        {
            fSampleTime.Write(sample.fSampleTime, c);
            vAccel.Write(Converter.HmdVector3ToDobble3(sample.vAccel), c);
            vGyro.Write(Converter.HmdVector3ToDobble3(sample.vGyro), c);
            unOffScaleFlags.Write((Imu_OffScaleFlags)sample.unOffScaleFlags, c);

            OnData.Execute(c);

            processed++;
        }
        _lastProcessed.Write(_lastProcessed.Read(c) + processed, c);
        
        var watch = _lastTime.Read(c);
        if(watch.ElapsedMilliseconds > 1000) {
            var process = _lastProcessed.Read(c);
            UniLog.Log($"IMU: Processed {process} messages in the last second");
            watch.Restart();
            _lastProcessed.Write(0, c);
        }
    }

    public IOperation DoOpen(ExecContext c)
    {
        string path = DevicePath.Evaluate(c);
        _failReason.Write(null, c);

        UniLog.Log($"Opening IMU connection to '{path}'");
        if (string.IsNullOrEmpty(path))
        {
            return Fail(ImuErrorCode.PathIsNullOrEmpty, c);
        }
        if (OpenVR.IOBuffer == null)
        {
            return Fail(ImuErrorCode.OpenVrNotFound, c);
        }
        if (_pulBuffer.Read(c) == 0)
        {
            try
            {
                ulong pulBuffer = 0;
                EIOBufferError errorcode;
                unsafe
                {
                    errorcode = OpenVR.IOBuffer.Open(path, EIOBufferMode.Read, (uint)sizeof(ImuSample_t), 0, ref pulBuffer);
                }
                if (errorcode != EIOBufferError.IOBuffer_Success)
                {
                    return Fail((ImuErrorCode)errorcode, c);
                }
                UniLog.Log($"IMU connection to '{path}' open, {pulBuffer}");
                NodeContextPath nodePath = c.CaptureContextPath();
                c.GetEventDispatcher(out var dispatcher);

                var buffer = new ConcurrentQueue<ImuSample_t>();
                var errorBox = new ErrorCodeBox();
                var thread = new Thread(() => ReadLoop(pulBuffer, buffer, errorBox));

                var reportTime = new Stopwatch();
                reportTime.Start();
                _lastTime.Write(reportTime, c);
                _failReason.Write(errorBox, c);
                _sampleBuffer.Write(buffer, c);
                _pulBuffer.Write(pulBuffer, c);
                _thread.Write(thread, c);

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
        var pulBuffer = _pulBuffer.Read(c);
        if (pulBuffer == 0)
        {
            return Fail(ImuErrorCode.AlreadyClosed, c);
        }
        var error = OpenVR.IOBuffer.Close(pulBuffer);
        if (error != EIOBufferError.IOBuffer_Success)
        {
            return Fail((ImuErrorCode)error, c);
        }
        _pulBuffer.Write(0, c);
        _thread.Write(null, c);
        return OnClosed.Target;
    }

    static void ReadLoop(ulong pulBuffer, ConcurrentQueue<ImuSample_t> destination, ErrorCodeBox errorCodeBox)
    {
        UniLog.Log($"IMU read thread starting...");
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
                    errorCodeBox.FailReason = (ImuErrorCode)failReason;
                    UniLog.Log($"Got status during reconnection {failReason}");
                    throw new Exception("read retuned: " + failReason.ToString());
                }
                int unreadSize = new();
                unsafe
                {
                    unreadSize = (int)punRead / sizeof(ImuSample_t);
                }
                for (int i = 0; i < unreadSize; i++)
                {
                    destination.Enqueue(samples[i]);
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

            OpenVR.IOBuffer.Close(pulBuffer);
        }
    }

    private IOperation Fail(ImuErrorCode error, ExecContext c)
    {
        UniLog.Log($"IMU read error: {error}");
        _pulBuffer.Write(0, c);
        _thread.Write(null, c);
        _failReason.Write(null, c);
        isOpened.Write(false, c);
        FailReason.Write(error, c);
        OnFail.Execute(c);
        return null;
    }
}
