// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: MotorConfigurationEventArgs.cs  Created: 2014-10-29@20:45
// Last modified: 2014-11-12@05:55 by Tim

using System;

namespace TI.DigitalDomeWorks.Simulator
    {
    /// <summary>
    ///     Class MotorConfigurationEventArgs. Communicates motor state and direction.
    /// </summary>
    public class MotorConfigurationEventArgs : EventArgs
        {
        internal static readonly MotorConfigurationEventArgs AllStopped = new MotorConfigurationEventArgs
            {
            AzimuthMotor = MotorConfiguration.Stopped,
            ShutterMotor = MotorConfiguration.Stopped
            };

        /// <summary>
        ///     Gets or sets a value indicating whether the simulated azimuth motor is running.
        /// </summary>
        /// <value><c>true</c> if the azimuth motor is energized; otherwise, <c>false</c>.</value>
        public MotorConfiguration AzimuthMotor { get; internal set; }

        /// <summary>
        ///     Gets or sets a value indicating whether the simulated shutter motor is energized.
        /// </summary>
        /// <value><c>true</c> if the shutter motor is energized; otherwise, <c>false</c>.</value>
        public MotorConfiguration ShutterMotor { get; internal set; }
        }
    }