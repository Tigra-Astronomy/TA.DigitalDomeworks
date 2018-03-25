// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ConnectionSpecs.cs  Last modified: 2018-03-19@17:43 by Tim Long

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.SharedTypes;
using TA.DigitalDomeworks.Specifications.Contexts;
using TA.DigitalDomeworks.Specifications.DeviceInterface.Behaviours;
using TA.DigitalDomeworks.Specifications.Helpers;
using TI.DigitalDomeWorks;

#pragma warning disable 0169

namespace TA.DigitalDomeworks.Specifications.DeviceInterface
    {
    /*
     * Given a new DeviceController
     * When Open() is called
     * It should:
     * -    Send a GINF command
     * -    Receive a status response
     * -    Parse the response and update internal state
     * -    Not return until the above is completed.
     */

    [Subject(typeof(DeviceController), "tasks on connect")]
    internal class when_opening_the_controller : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedConnection("Simulator:Fast")
            .Build();

        Because of = () => exception = Catch.Exception(() => Controller.Open());
        It should_send_a_status_request = () => SimulatorChannel.SendLog.First().ShouldEqual("GINF");
        It should_perform_shutter_recovery = () => SimulatorChannel.SendLog.Skip(1).First().ShouldEqual(Constants.CmdClose);
        It should_connect_successfully = () => exception.ShouldBeNull();
        static Exception exception;
        Behaves_like<a_stopped_dome> stopped_dome;
        static SimulatorCommunicationsChannel SimulatorChannel => (SimulatorCommunicationsChannel) Channel;
        }
    }