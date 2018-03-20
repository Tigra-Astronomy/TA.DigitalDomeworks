using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.DeviceInterface.StateMachine;
using TA.DigitalDomeworks.Specifications.Fakes;

namespace TA.DigitalDomeworks.Specifications.Contexts {
    class DeviceControllerContext
        {
        public DeviceController Controller { get; set; }

        public ICommunicationChannel Channel { get; set; }

        public ControllerStateMachine StateMachine { get; set; }

        public RxControllerActions Actions { get; set; }
        }
    }