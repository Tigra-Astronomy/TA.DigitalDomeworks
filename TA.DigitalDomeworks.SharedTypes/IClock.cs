// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: IClock.cs  Last modified: 2018-03-28@17:43 by Tim Long

using System;

namespace TA.DigitalDomeworks.SharedTypes
    {
    public interface IClock
        {
        DateTime GetCurrentTime();
        }

    public class SystemDateTimeUtcClock : IClock
        {
        public DateTime GetCurrentTime()
            {
            return DateTime.UtcNow;
            }
        }
    }