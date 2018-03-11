using System;
using System.ComponentModel;
using System.Linq;
using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.SharedTypes;
using TA.DigitalDomeworks.Specifications.Contexts;
using TA.DigitalDomeworks.Specifications.Helpers;
using TI.DigitalDomeWorks;

namespace TA.DigitalDomeworks.Specifications.DeviceInterface
    {
    [Subject(typeof(DeviceController), "connection status")]
    class when_the_channel_is_open : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithOpenConnection("Fake")
            .WithFakeResponse("Yoohoo2U2")
            .Build();
        It should_be_online = () => Controller.IsOnline.ShouldBeTrue();
        }

    [Subject(typeof(DeviceController), "connection status")]
    class when_the_channel_is_closed : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedConnection("Fake")
            .Build();
        It should_be_offline = () => Controller.IsOnline.ShouldBeFalse();
        }

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
    internal class when_opening_the_communications_channel : with_device_controller_context
        {
        Establish context = () => Context = DeviceControllerContextBuilder
            .WithClosedConnection("Simulator:Fast")
            .OnPropertyChanged(DetectStatusChanged)
            .Build();

        static void DetectStatusChanged(object sender, PropertyChangedEventArgs e)
            {
            statusChanged = e.PropertyName == nameof(Controller.CurrentStatus);
            }

        Because of = () => Exception = Catch.Exception(() => Controller.Open());
        It should_send_a_status_request = () =>
            (Channel as SimulatorCommunicationsChannel).SendLog.Single().ShouldEqual("GINF");
        It should_connect_successfully = () => Exception.ShouldBeNull();
        It should_update_internal_state_to_reflect_the_received_status_response = () => 
            statusChanged.ShouldBeTrue();
        static IControllerStatus status;
        static Exception Exception;
        static bool statusChanged;
        }
    }