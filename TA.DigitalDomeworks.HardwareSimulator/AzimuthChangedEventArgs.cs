// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: AzimuthChangedEventArgs.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:55 by Tim

using System;

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    /// <summary>
    ///     Defines the event arguments passed to the <see cref="SimulatorStateMachine.AzimuthChanged" /> event handler.
    /// </summary>
    public class AzimuthChangedEventArgs : EventArgs
        {
        /// <summary>
        ///     Gets or sets the new azimuth.
        /// </summary>
        /// <value>The new azimuth.</value>
        public int NewAzimuth { get; internal set; }
        }
    }