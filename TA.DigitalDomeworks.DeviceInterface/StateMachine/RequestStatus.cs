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

        /// <summary>
        /// This trigger is not valid in this state, so we basically ignore it.
        /// </summary>
        /// <param name="motorCurrent"></param>
        public void ShutterCurrentReadingReceived(int motorCurrent)
            {
            machine.ShutterMotorCurrent = motorCurrent;
            }

        public void StatusUpdateReceived(IHardwareStatus status)
            {
            machine.UpdateStatus(status);
            machine.TransitionToState(new Ready(machine));
            }

        public string Name => nameof(RequestStatus);
        }
    }