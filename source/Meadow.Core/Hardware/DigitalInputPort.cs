﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Meadow.Hardware
{
    /// <summary>
    /// Represents a port that is capable of reading digital input.
    /// </summary>
    public class DigitalInputPort : DigitalInputPortBase
    {
        private ResistorMode _resistorMode;
        private int _debounceDuration;
        private int _glitchCycleCount;

        protected IIOController IOController { get; set; }

        protected DateTime LastEventTime { get; set; } = DateTime.MinValue;

        protected DigitalInputPort(
            IPin pin,
            IIOController ioController,
            IDigitalChannelInfo channel,
            InterruptMode interruptMode = InterruptMode.None,
            ResistorMode resistorMode = ResistorMode.Disabled,
            int debounceDuration = 0,
            int glitchFilterCycleCount = 0
            ) : base(pin, channel, interruptMode)
        {
            if (interruptMode != InterruptMode.None && (!channel.InterrruptCapable))
            {
                throw new Exception("Unable to create port; channel is not capable of interrupts");
            }

            this.IOController = ioController;
            this.IOController.Interrupt += OnInterrupt;
            this._resistorMode = resistorMode;

            // attempt to reserve
            var success = DeviceChannelManager.ReservePin(pin, ChannelConfigurationType.DigitalInput);
            if (success.Item1)
            {
                // make sure the pin is configured as a digital output with the proper state
                ioController.ConfigureInput(pin, resistorMode, interruptMode, debounceDuration, glitchFilterCycleCount);
                DebounceDuration = debounceDuration;
                GlitchFilterCycleCount = glitchFilterCycleCount;
            }
            else
            {
                throw new PortInUseException();
            }
        }

        public static DigitalInputPort From(
            IPin pin,
            IIOController ioController,
            InterruptMode interruptMode = InterruptMode.None,
            ResistorMode resistorMode = ResistorMode.Disabled,
            int debounceDuration = 0,
            int glitchFilterCycleCount = 0
            )
        {
            var chan = pin.SupportedChannels.OfType<IDigitalChannelInfo>().FirstOrDefault();
            if (chan != null) {
                //TODO: need other checks here.
                if (interruptMode != InterruptMode.None && (!chan.InterrruptCapable)) {
                    throw new Exception("Unable to create input; channel is not capable of interrupts");
                }
                return new DigitalInputPort(pin, ioController, chan, interruptMode, resistorMode, debounceDuration, glitchFilterCycleCount);
            } else {
                throw new Exception("Unable to create an output port on the pin, because it doesn't have a digital channel");
            }
        }

        public override ResistorMode Resistor
        {
            get => _resistorMode;
            set
            {
                IOController.SetResistorMode(this.Pin, value);
                _resistorMode = value;
            }
        }

        void OnInterrupt(IPin pin, bool state)
        {
            if(pin == this.Pin)
            {
                var time = DateTime.Now;

                // debounce timing checks
                if (DebounceDuration > 0) {
                    if ((time - this.LastEventTime).TotalMilliseconds < DebounceDuration) {
                        //Console.WriteLine("Debounced.");
                        return;
                    }
                }

                var capturedLastTime = LastEventTime; // note: doing this for latency reasons. kind of. sort of. bad time good time. all time.
                this.LastEventTime = time;

                RaiseChangedAndNotify(new DigitalInputPortEventArgs(state, time, capturedLastTime));
                
            }
        }

        public override void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // TODO: we should consider moving this logic to the finalizer
            // but the problem with that is that we don't know when it'll be called
            // but if we do it in here, we may need to check the _disposed field
            // elsewhere

            if (!disposed)
            {
                if (disposing)
                {
                    DeviceChannelManager.ReleasePin(Pin);
                }
                disposed = true;
            }
        }

        // Finalizer
        ~DigitalInputPort()
        {
            Dispose(false);
        }

        public override bool State
        {
            get => this.IOController.GetDiscrete(this.Pin);
        }

        public override int DebounceDuration
        {
            get => _debounceDuration;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                _debounceDuration = value;
            }
        }

        public override int GlitchFilterCycleCount
        {
            get => _glitchCycleCount;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException();
                _glitchCycleCount = value;
            }
        }
    }
}