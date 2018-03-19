using NLog.Fluent;
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

        public void RotationDetected()
            {
            Log.Warn()
                .Message("Rotation detected while expecting status. Issuing AllStop and re-requesting status.")
                .Write();
            EmergencyStopAndRequestStatus();
            }

        private void EmergencyStopAndRequestStatus()
            {
            machine.AllStop();
            machine.RequestHardwareStatus();
            }

        /// <summary>
        /// This trigger is not valid in this state, so we basically ignore it.
        /// </summary>
        public void ShutterMovementDetected()
            {
            Log.Warn()
                .Message("Shutter movement detected while expecting status. Issuing AllStop and re-requesting status.")
                .Write();
            EmergencyStopAndRequestStatus();
            }

        public void StatusUpdateReceived(IHardwareStatus status)
            {
            machine.UpdateStatus(status);
            machine.TransitionToState(new Ready(machine));
            }

        public string Name => nameof(RequestStatus);
        }
    }