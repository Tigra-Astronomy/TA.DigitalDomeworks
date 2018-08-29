// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: DeviceControllerOptions.cs  Last modified: 2018-06-16@16:50 by Tim Long

using System;

namespace TA.DigitalDomeworks.SharedTypes
    {
    public class DeviceControllerOptions
        {
        public bool PerformShutterRecovery { get; set; } = true;

        public TimeSpan MaximumShutterCloseTime { get; set; }

        public TimeSpan MaximumFullRotationTime { get; set; }

        public TimeSpan KeepAliveTimerInterval { get; set; }

        public int CurrentDrawDetectionThreshold { get; set; }

        public bool IgnoreHardwareShutterSensor { get; set; }

        public TimeSpan ShutterTickTimeout { get; set; }
        }
    }