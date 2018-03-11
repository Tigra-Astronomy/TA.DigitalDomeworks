// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: RotationDirection.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:55 by Tim

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    /// <summary>
    ///     Rotation direction
    /// </summary>
    public enum RotationDirection
        {
        /// <summary>
        ///     Counterclockwise of left.
        /// </summary>
        CounterClockwise = -1,

        /// <summary>
        ///     Clockwise or right.
        /// </summary>
        Clockwise = 1,

        /// <summary>
        ///     Not rotating
        /// </summary>
        None = 0
        }
    }