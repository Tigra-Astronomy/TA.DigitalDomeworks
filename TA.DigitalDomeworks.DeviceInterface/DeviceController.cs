using System.Runtime.CompilerServices;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface
    {
    internal class DeviceController
        {
        private readonly ICommunicationChannel channel;
        private readonly ControllerStatusFactory statusFactory;
        private SerialCommunicationChannel test;

        public DeviceController(ICommunicationChannel channel, ControllerStatusFactory factory)
            {
            this.channel = channel;
            this.statusFactory = factory;
            }

        public bool IsOnline => channel.IsOpen;
        }
    }