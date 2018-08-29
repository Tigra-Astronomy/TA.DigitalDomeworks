// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ShutterDirection.cs  Last modified: 2018-03-19@20:47 by Tim Long

namespace TA.DigitalDomeworks.SharedTypes
    {
    /// <summary>
    ///     Shutter movement directions.
    ///     Note: ordinal values are important and are used for parsing
    ///     received data within <c>DeviceController</c>.
    /// </summary>
    public enum ShutterDirection
        {
        /// <summary>
        ///     The shutter is not moving, or the direction is unknown
        /// </summary>
        None = 0,
        /// <summary>
        ///     The shutter is closing.
        /// </summary>
        Closing = 1,
        /// <summary>
        ///     The shutter is opening.
        /// </summary>
        Opening = 2
        }
    }