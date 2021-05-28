﻿using Meadow.Devices;
using Meadow.Hardware;
using System;
using System.Linq;
using System.Text;
using System.Threading;

namespace Meadow
{

    public class MeadowOnLinux<TPinout> : IMeadowDevice
        where TPinout : IPinDefinitions, new()
    {
        public IPinDefinitions Pins { get; }
        public DeviceCapabilities Capabilities { get; }

        public MeadowOnLinux()
        {
            Pins = new TPinout();
            Capabilities = new DeviceCapabilities(
                new AnalogCapabilities(false, null),
                new NetworkCapabilities(false, true)
                );
        }

        public void Initialize()
        {
//            IoController.Initialize();
        }

        public II2cBus CreateI2cBus()
        {
            return CreateI2cBus(Pins["PIN05"], Pins["PIN03"]);
        }

        public II2cBus CreateI2cBus(int frequencyHz)
        {
            // TODO: how do we affect frequency on these platforms?

            return CreateI2cBus(Pins["PIN05"], Pins["PIN03"]);
        }

        public II2cBus CreateI2cBus(IPin[] pins, int frequencyHz = 100000)
        {
            // TODO: how do we affect frequency on these platforms?

            return CreateI2cBus(Pins["PIN05"], Pins["PIN03"]);
        }

        public II2cBus CreateI2cBus(IPin clock, IPin data, int frequencyHz = 100000)
        {
            // TODO: how do we affect frequency on these platforms?

            return new I2CBus(clock, data, frequencyHz);
        }

        // ----- BELOW HERE ARE NOT YET IMPLEMENTED -----

        public IAnalogInputPort CreateAnalogInputPort(IPin pin, float voltageReference = 3.3F)
        {
            throw new NotImplementedException();
        }

        public IBiDirectionalPort CreateBiDirectionalPort(IPin pin, bool initialState = false, InterruptMode interruptMode = InterruptMode.None, ResistorMode resistorMode = ResistorMode.Disabled, PortDirectionType initialDirection = PortDirectionType.Input, double debounceDuration = 0, double glitchDuration = 0, OutputType output = OutputType.PushPull)
        {
            throw new NotImplementedException();
        }

        public IDigitalInputPort CreateDigitalInputPort(IPin pin, InterruptMode interruptMode = InterruptMode.None, ResistorMode resistorMode = ResistorMode.Disabled, double debounceDuration = 0, double glitchDuration = 0)
        {
            throw new NotImplementedException();
        }

        public IDigitalOutputPort CreateDigitalOutputPort(IPin pin, bool initialState = false, OutputType initialOutputType = OutputType.PushPull)
        {
            throw new NotImplementedException();
        }

        public IPwmPort CreatePwmPort(IPin pin, float frequency = 100, float dutyCycle = 0.5F, bool invert = false)
        {
            throw new NotImplementedException();
        }

        public ISerialMessagePort CreateSerialMessagePort(SerialPortName portName, byte[] suffixDelimiter, bool preserveDelimiter, int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int readBufferSize = 512)
        {
            throw new NotImplementedException();
        }

        public ISerialMessagePort CreateSerialMessagePort(SerialPortName portName, byte[] prefixDelimiter, bool preserveDelimiter, int messageLength, int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int readBufferSize = 512)
        {
            throw new NotImplementedException();
        }

        public ISerialPort CreateSerialPort(SerialPortName portName, int baudRate = 9600, int dataBits = 8, Parity parity = Parity.None, StopBits stopBits = StopBits.One, int readBufferSize = 1024)
        {
            throw new NotImplementedException();
        }

        public ISpiBus CreateSpiBus(IPin clock, IPin mosi, IPin miso, SpiClockConfiguration config)
        {
            throw new NotImplementedException();
        }

        public ISpiBus CreateSpiBus(IPin clock, IPin mosi, IPin miso, long speedkHz = 375)
        {
            throw new NotImplementedException();
        }

        public IPin GetPin(string pinName)
        {
            throw new NotImplementedException();
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public void SetClock(DateTime dateTime)
        {
            throw new NotImplementedException();
        }

        public void SetSynchronizationContext(SynchronizationContext context)
        {
            throw new NotImplementedException();
        }

        public void WatchdogEnable(TimeSpan timeout)
        {
            throw new NotImplementedException();
        }

        public void WatchdogReset()
        {
            throw new NotImplementedException();
        }
    }
}
