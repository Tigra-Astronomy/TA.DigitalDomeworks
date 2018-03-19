// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: Rotating.cs  Last modified: 2018-03-16@18:22 by Tim Long

using NLog.Fluent;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    internal sealed class Rotating : IControllerState
        {
        private readonly ControllerStateMachine machine;

        public Rotating(ControllerStateMachine machine)
            {
            this.machine = machine;
            }

        public void OnEnter()
            {
            machine.AzimuthMotorActive = true;
            }

        public void OnExit()
            {
            machine.AzimuthMotorActive = false;
            }

        /// <summary>
        /// Trigger: updates the encoder position
        /// </summary>
        public void RotationDetected()
            {
            // ToDo - reset rotation timeout
            }

        /// <summary>
        /// Trigger: invalid for this state.
        /// </summary>
        public void ShutterMovementDetected()
            {
            Log.Error()
                .Message("Shutter movement detected while rotating. This is unexpected.")
                .Write();
            }

        /// <summary>
        /// Trigger: => Ready.
        /// </summary>
        /// <param name="status"></param>
        public void StatusUpdateReceived(IHardwareStatus status)
            {
            machine.UpdateStatus(status);
            machine.TransitionToState(new Ready(machine));
            }

        public string Name => nameof(Rotating);
        }
    }