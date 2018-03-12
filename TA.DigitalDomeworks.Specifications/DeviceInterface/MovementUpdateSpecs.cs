
using System;
using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.Specifications.Contexts;

namespace TA.DigitalDomeworks.Specifications
    {
    [Subject(typeof(DeviceController), "Concern")]
    internal class when_the_controller_sends_an_encoder_tick : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedConnection("Fake")
            .WithFakeResponse("P0099\n")
            .Build();

        Because of = () =>
            {
            Controller.Open(performOnConnectActions: false);
            Channel.Send(string.Empty);
            };
        It should_update_the_position_property = () => Controller.AzimuthEncoderSteps.ShouldEqual(99);
        }
    }