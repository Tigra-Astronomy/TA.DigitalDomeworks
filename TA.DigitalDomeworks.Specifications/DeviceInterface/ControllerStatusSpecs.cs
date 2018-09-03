﻿// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ControllerStatusSpecs.cs  Last modified: 2018-03-28@18:17 by Tim Long

using System;
using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.SharedTypes;
using TA.DigitalDomeworks.Specifications.Contexts;
using TA.DigitalDomeworks.Specifications.DeviceInterface.Behaviours;
using TA.DigitalDomeworks.Specifications.Fakes;

#pragma warning disable 0169    // Field not used, triggers on Behaves_like<>

namespace TA.DigitalDomeworks.Specifications.DeviceInterface
    {
    [Subject(typeof(HardwareStatus), "creation")]
    internal class when_creating_a_status
        {
        Establish context = () =>
            factory = new ControllerStatusFactory(new FakeClock(DateTime.MinValue.ToUniversalTime()));
        Because of = () => actual = factory.FromStatusPacket(RealWorldStatusPacket);
        It should_be_v4 = () => actual.FirmwareVersion.ShouldEqual("V4");
        It should_have_circumference = () => actual.DomeCircumference.ShouldEqual(704);
        It should_have_home_position = () => actual.HomePosition.ShouldEqual(293);
        It should_have_coast = () => actual.Coast.ShouldEqual(1);
        It should_have_azimuth = () => actual.CurrentAzimuth.ShouldEqual(289);
        It should_not_be_slaved = () => actual.Slaved.ShouldBeFalse();
        It should_have_indeterminate_shutter = () => actual.ShutterSensor.ShouldEqual(SensorState.Indeterminate);
        It should_have_closed_support_ring = () => actual.DsrSensor.ShouldEqual(SensorState.Closed);
        It should_be_at_home = () => actual.AtHome.ShouldBeTrue();
        It should_have_home_ccw = () => actual.HomeCounterClockwise.ShouldEqual(287);
        It should_have_home_cw = () => actual.HomeClockwise.ShouldEqual(299);
        It should_have_user_pins = () => actual.UserPins.ShouldEqual(Octet.Zero);
        It should_have_weather_age = () => actual.WeatherAge.ShouldEqual(0);
        It should_have_wind_direction = () => actual.WindDirection.ShouldEqual(0);
        It should_have_wind_speed = () => actual.WindSpeed.ShouldEqual(0);
        It should_have_temperature = () => actual.Temperature.ShouldEqual(112);
        It should_have_humidity = () => actual.Humidity.ShouldEqual(50);
        It should_be_dry = () => actual.Wetness.ShouldEqual(0);
        It should_not_be_snowing = () => actual.Snow.ShouldEqual(0);
        It should_have_wind_peak = () => actual.WindPeak.ShouldEqual(0);
        It should_have_lx200_azimuth = () => actual.Lx200Azimuth.ShouldEqual(180);
        It should_have_dead_zone = () => actual.DeadZone.ShouldEqual(5);
        It should_have_offset = () => actual.Offset.ShouldEqual(5);
        //ToDo: fill in the other fields
        static IHardwareStatus actual;
        static ControllerStatusFactory factory;
        // This status packet was captured from real hardware.
        const string RealWorldStatusPacket = "V4,704,293,1,289,0,0,1,0,287,299,0,0,0,0,112,50,0,0,0,180,5,5";
        }

    [Subject(typeof(DeviceController), "property updates")]
    internal class when_a_status_packet_is_received : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedConnection("Simulator:Fast")
            .Build();
        Because of = () =>
            {
            Controller.Open(performOnConnectActions: false);
            Context.Actions.RequestHardwareStatus();
            Context.StateMachine.WaitForReady(TimeSpan.FromSeconds(5));
            };
        static IHardwareStatus receivedStatus;
        Behaves_like<a_stopped_dome> stopped_dome;
        const string RealWorldStatusPacket = "V4,704,293,1,289,0,0,1,0,287,299,0,0,0,0,112,50,0,0,0,180,5,5\n";
        }
    }