
using System;
using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.HardwareSimulator;
using TA.DigitalDomeworks.SharedTypes;
using TA.DigitalDomeworks.Specifications.Contexts;

namespace TA.DigitalDomeworks.Specifications
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
        It should_indicate_that_the_azimuth_motor_is_active = () => Controller.DomeRotationInProgress.ShouldBeTrue();
        It should_not_indicate_that_the_shutter_motor_is_active = () => Controller.ShutterMovementInProgress.ShouldBeFalse();
        It should_indicate_that_something_is_moving = () => Controller.IsMoving.ShouldBeTrue();
        It should_not_indicate_direction = () => Controller.Direction.ShouldEqual(RotationDirection.None);
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
        It should_be_rotating_counter_clockwise = () => Controller.Direction.ShouldEqual(RotationDirection.CounterClockwise);
        It should_indicate_that_the_azimuth_motor_is_active = () => Controller.DomeRotationInProgress.ShouldBeTrue();
        It should_not_indicate_that_the_shutter_motor_is_active = () => Controller.ShutterMovementInProgress.ShouldBeFalse();
        It should_indicate_that_something_is_moving = () => Controller.IsMoving.ShouldBeTrue();
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
        It should_be_rotating_clockwise = () => Controller.Direction.ShouldEqual(RotationDirection.Clockwise);
        It should_indicate_that_the_azimuth_motor_is_active = () => Controller.DomeRotationInProgress.ShouldBeTrue();
        It should_not_indicate_that_the_shutter_motor_is_active = () => Controller.ShutterMovementInProgress.ShouldBeFalse();
        It should_indicate_that_something_is_moving = () => Controller.IsMoving.ShouldBeTrue();
        }
    }