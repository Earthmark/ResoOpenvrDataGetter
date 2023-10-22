/*
using Elements.Core;
using FrooxEngine;
using FrooxEngine.ProtoFlux;
using FrooxEngine.ProtoFlux.Runtimes.Execution;
using ProtoFlux.Core;
using System;
using System.Threading;
using Valve.VR;

namespace OpenvrDataGetter.Components
{
    [Category(new string[] { "LogiX/Add-Ons/OpenvrDataGetter" })]
    public class ImuReader : VoidNode<FrooxEngineContext>, IDisposable
    {
        public ObjectArgument<string> DevicePath;
        public readonly SyncRef<ISyncNodeOperation> OnOpened;
        public readonly SyncRef<ISyncNodeOperation> OnClosed;
        public readonly ValueOutput<bool> isOpened;
        public readonly SyncRef<ISyncNodeOperation> OnFail;
        public readonly ValueOutput<ErrorCode> FailReason;
        public readonly SyncRef<ISyncNodeOperation> OnData;
        public readonly ValueOutput<double> fSampleTime;
        public readonly ValueOutput<double3> vAccel;
        public readonly ValueOutput<double3> vGyro;
        public readonly ValueOutput<Imu_OffScaleFlags> unOffScaleFlags;

        ulong pulBuffer = 0;
        Thread thread = null;

        public ImuReader()
        {
        }

        protected override void OnAwake()
        {
            isOpened.Value = false;
        }

        [ImpulseTarget]
        public void Open()
        {
            string path = DevicePath.Evaluate();
            if (string.IsNullOrEmpty(path))
            {
                Fail(ErrorCode.PathIsNullOrEmpty);
                return;
            }
            if (OpenVR.IOBuffer == null)
            {
                Fail(ErrorCode.OpenVrNotFound);
                return;
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
                        Fail((ErrorCode)errorcode);
                        return;
                    }
                    thread = new Thread(readLoop);
                    thread.Start();
                }
                catch (Exception e)
                {
                    UniLog.Log(e);
                    Fail(ErrorCode.UnknownException);
                    return;
                }
                isOpened.Value = true;
                OnOpened.Trigger();
            }
            else
            {
                Fail(ErrorCode.AlreadyOpened);
            }
        }

        [ImpulseTarget]
        public void Close()
        {
            if (pulBuffer == 0)
            {
                Fail(ErrorCode.AlreadyClosed);
                return;
            }
            var error = OpenVR.IOBuffer.Close(pulBuffer);
            if (error != EIOBufferError.IOBuffer_Success)
            {
                Fail((ErrorCode)error);
                return;
            }
            try
            {
                thread.Abort();
            }
            catch (Exception e)
            {
                UniLog.Log(e);
                Fail(ErrorCode.UnknownException);
                return;
            }
            thread = null;
            pulBuffer = 0;
            isOpened.Value = false;
            OnClosed.Trigger();
        }

        void readLoop()
        {
            EIOBufferError failReason = EIOBufferError.IOBuffer_Success;
            const uint arraySize = 10;
            ImuSample_t[] samples = new ImuSample_t[arraySize];
            try
            {
                while (true)
                {
                    uint punRead = new();
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
                        var sample = samples[i];
                        World.RunSynchronously(() =>
                        {
                            fSampleTime.Value = sample.fSampleTime;
                            vAccel.Value = Converter.HmdVector3ToDobble3(sample.vAccel);
                            vGyro.Value = Converter.HmdVector3ToDobble3(sample.vGyro);
                            unOffScaleFlags.Value = (Imu_OffScaleFlags)sample.unOffScaleFlags;

                            OnData.Trigger();

                            fSampleTime.Value = 0;
                            vAccel.Value = double3.Zero;
                            vGyro.Value = double3.Zero;
                            unOffScaleFlags.Value = 0;
                        });
                    }
                    if (unreadSize == 0) Thread.Sleep(10);
                }
            }
            catch (Exception e)
            {
                UniLog.Log(e);

                thread = null;
                World.RunSynchronously(() =>
                {
                    isOpened.Value = false;
                    Fail(failReason == EIOBufferError.IOBuffer_Success ? ErrorCode.UnknownException : (ErrorCode)failReason);
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

        void Fail(ErrorCode error)
        {
            FailReason.Value = error;
            OnFail.Trigger();
            FailReason.Value = ErrorCode.None;
        }

        public enum ErrorCode
        {
            None = 0, //IOBuffer_Success = 0,
            AlreadyOpened,
            AlreadyClosed,
            UnknownException,
            PathIsNullOrEmpty,
            OpenVrNotFound,
            IOBuffer_OperationFailed = 100,
            IOBuffer_InvalidHandle = 101,
            IOBuffer_InvalidArgument = 102,
            IOBuffer_PathExists = 103,
            IOBuffer_PathDoesNotExist = 104,
            IOBuffer_Permission = 105
        }
    }
}
*/