﻿// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: device_controller_behaviour.cs  Last modified: 2018-03-14@00:06 by Tim Long

using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.Specifications.Contexts;

namespace TA.DigitalDomeworks.Specifications.DeviceInterface.Behaviours
    {
    internal class device_controller_behaviour
        {
        protected static DeviceControllerContext Context;

        protected static DeviceController Controller => Context.Controller;
        }
    }