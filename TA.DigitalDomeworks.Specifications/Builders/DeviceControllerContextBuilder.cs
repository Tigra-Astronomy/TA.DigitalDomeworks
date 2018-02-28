using System;
using System.Text;
using NodaTime;
using NodaTime.Testing;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.SharedTypes;
using TA.DigitalDomeworks.Specifications.Contexts;
using TA.DigitalDomeworks.Specifications.Fakes;

namespace TA.DigitalDomeworks.Specifications.Builders
    {
    class DeviceControllerContextBuilder
        {
        bool channelShouldBeOpen;
        readonly StringBuilder fakeResponseBuilder = new StringBuilder();
        IClock timeSource = new FakeClock(Instant.MinValue);

        public DeviceControllerContext Build()
            {
            // Build the communications channel
            var channel = new FakeCommunicationChannel(fakeResponseBuilder.ToString())
                {
                IsOpen = channelShouldBeOpen
                };

            // Build the ControllerStatusFactory
            var statusFactory = new ControllerStatusFactory(timeSource);

            // Build the device controller
            var controller = new DeviceController(channel, statusFactory);

            // Assemble the device controller test context
            var context = new DeviceControllerContext
                {
                Channel = channel,
                Controller = controller
                };

            return context;
            }

        public DeviceControllerContextBuilder WithOpenChannel()
            {
            channelShouldBeOpen = true;
            return this;
            }

        public DeviceControllerContextBuilder WithFakeResponse(string fakeResponse)
            {
            fakeResponseBuilder.Append(fakeResponse);
            return this;
            }

        public DeviceControllerContextBuilder WithClosedChannel()
            {
            channelShouldBeOpen = false;
            return this;
            }
        }
    }