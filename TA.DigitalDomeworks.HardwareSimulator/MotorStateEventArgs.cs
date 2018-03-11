// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: MotorStateEventArgs.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:55 by Tim

using System;

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    /// <summary>
    ///     Communicates the state of the various motors.
    /// </summary>
    [Obsolete]
    public class MotorStateEventArgs : EventArgs
        {
        /// <summary>
        ///     Gets or sets a value indicating whether the simulated azimuth motor is running.
        /// </summary>
        /// <value><c>true</c> if the azimuth motor is energized; otherwise, <c>false</c>.</value>
        public bool AzimuthMotor { get; internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the simulated shutter motor is energized.
        /// </summary>
        /// <value><c>true</c> if the shutter motor is energized; otherwise, <c>false</c>.</value>
        public bool ShutterMotor { get; internal set; }
        }
    }