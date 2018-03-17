using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine {
    internal class RequestStatus : IControllerState
        {
        private readonly ControllerStateMachine machine;

        public RequestStatus(ControllerStateMachine machine)
            {
            this.machine = machine;
            }
        public void OnEnter()
            {
            machine.RequestHardwareStatus();
            }

        public void OnExit() { }

        public void EncoderTickReceived(int encoderPosition)
            {
            }

        public void ShutterCurrentReadingReceived(int motorCurrent)
            {
            }

        public void StatusUpdateReceived(IHardwareStatus status)
            {
            machine.UpdateStatus(status);
            machine.TransitionToState(new Ready(machine));
            }

        public string Name => nameof(RequestStatus);
        }
    }