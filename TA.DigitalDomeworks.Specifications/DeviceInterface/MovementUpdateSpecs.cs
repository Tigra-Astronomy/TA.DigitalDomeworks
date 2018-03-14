﻿// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: MovementUpdateSpecs.cs  Last modified: 2018-03-14@00:31 by Tim Long

using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.SharedTypes;
using TA.DigitalDomeworks.Specifications.Contexts;
using TA.DigitalDomeworks.Specifications.DeviceInterface.Behaviours;

namespace TA.DigitalDomeworks.Specifications.DeviceInterface
    {
    [Subject(typeof(DeviceController), "Encoder Ticks")]
    internal class when_the_controller_sends_an_encoder_tick : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedConnection("Fake")
            .WithFakeResponse("P99\n")
            .Build();

        Because of = () =>
            {
            Controller.Open(performOnConnectActions: false);
            Channel.Send(string.Empty);
            };
        It should_update_the_position_property = () => Controller.AzimuthEncoderSteps.ShouldEqual(99);
        Behaves_like<a_directionless_rotating_dome> _;
        }

    [Subject(typeof(DeviceController), "Direction")]
    internal class when_the_dome_begins_to_rotate_counterclockwise : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedConnection("Fake")
            .WithFakeResponse("L")
            .Build();
        Because of = () =>
            {
            Controller.Open(performOnConnectActions: false);
            Channel.Send(string.Empty);
            };
        It should_be_rotating_counter_clockwise =
            () => Controller.RotationDirection.ShouldEqual(RotationDirection.CounterClockwise);
        Behaves_like<a_rotating_dome> _;
        }

    [Subject(typeof(DeviceController), "Direction")]
    internal class when_the_dome_begins_to_rotate_clockwise : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedConnection("Fake")
            .WithFakeResponse("R")
            .Build();

        Because of = () =>
            {
            Controller.Open(performOnConnectActions: false);
            Channel.Send(string.Empty);
            };
        It should_be_rotating_clockwise = () => Controller.RotationDirection.ShouldEqual(RotationDirection.Clockwise);
        Behaves_like<a_rotating_dome> _;
        }

    [Subject(typeof(DeviceController), "Shutter Current")]
    internal class when_the_controller_sends_a_shutter_current_reading : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedConnection("Fake")
            .WithFakeResponse("Z15\n")
            .Build();

        Because of = () =>
            {
            Controller.Open(performOnConnectActions: false);
            Channel.Send(string.Empty);
            };
        It should_update_the_shutter_current_property = () => Controller.ShutterCurrent.ShouldEqual(15);
        Behaves_like<a_dome_with_a_moving_shutter> _;
        }

    [Subject(typeof(DeviceController), "Shutter Direction")]
    internal class when_the_shutter_begins_to_close : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedConnection("Fake")
            .WithFakeResponse("C")
            .Build();
        Because of = () =>
            {
            Controller.Open(performOnConnectActions: false);
            Channel.Send(string.Empty);
            };
        It should_be_closing = () => Controller.ShutterDirection.ShouldEqual(ShutterDirection.Closing);
        Behaves_like<a_dome_with_a_moving_shutter> _;
        }

    [Subject(typeof(DeviceController), "Shutter Direction")]
    internal class when_the_shutter_begins_to_open : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedConnection("Fake")
            .WithFakeResponse("O")
            .Build();
        Because of = () =>
            {
            Controller.Open(performOnConnectActions: false);
            Channel.Send(string.Empty);
            };
        It should_be_opening = () => Controller.ShutterDirection.ShouldEqual(ShutterDirection.Opening);
        Behaves_like<a_dome_with_a_moving_shutter> _;
        }
    }