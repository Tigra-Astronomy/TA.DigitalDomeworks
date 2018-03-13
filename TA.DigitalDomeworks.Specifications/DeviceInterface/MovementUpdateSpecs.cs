
using System;
using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface;
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
        }
    }