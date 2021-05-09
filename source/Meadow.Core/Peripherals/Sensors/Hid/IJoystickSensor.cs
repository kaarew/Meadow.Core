﻿using System;

namespace Meadow.Peripherals.Sensors.Hid
{
    public interface IJoystickSensor : ISensor, IObservable<ChangeResult<JoystickPosition>>
    {
        // TODO: why aren't these encapsulated into JoystickPosition?

        JoystickPosition? Position { get; }

        ///// <summary>
        ///// Last horizontal value read from the Joystick.
        ///// </summary>
        //float? HorizontalValue { get; }

        ///// <summary>
        ///// Last vertical value read from the Joystick.
        ///// </summary>
        //float? VerticalValue { get; }

        /// <summary>
        /// Raised when a new reading has been made. Events will only be raised
        /// while the driver is updating. To start, call the `StartUpdating()`
        /// method.
        /// </summary>
        event EventHandler<ChangeResult<JoystickPosition>> Updated;
    }
}