// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: DeviceControllerOptions.cs  Last modified: 2018-03-25@19:15 by Tim Long

using System;

namespace TA.DigitalDomeworks.SharedTypes
    {
    public class DeviceControllerOptions
        {
        public bool PerformShutterRecovery { get; set; } = true;
        public TimeSpan MaximumShutterCloseTime { get; set; }
        public TimeSpan MaximumFullRotationTime { get; set; }
        }
    }