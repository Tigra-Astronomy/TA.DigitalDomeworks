using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.Specifications.Contexts;

namespace TA.DigitalDomeworks.Specifications.DeviceInterface
    {
    [Subject(typeof(DeviceController), "connection status")]
    class when_the_channel_is_open : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithOpenChannel()
            .WithFakeResponse("Yoohoo2U2")
            .Build();
        It should_be_online = () => Controller.IsOnline.ShouldBeTrue();
        }

    [Subject(typeof(DeviceController), "connection status")]
    class when_the_channel_is_closed : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedChannel()
            .Build();
        It should_be_offline = () => Controller.IsOnline.ShouldBeFalse();
        }
    }