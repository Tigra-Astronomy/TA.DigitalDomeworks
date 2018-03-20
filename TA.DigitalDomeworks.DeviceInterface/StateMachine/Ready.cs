// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: Ready.cs  Last modified: 2018-03-20@01:00 by Tim Long

using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    internal sealed class Ready : ControllerStateBase
        {
        public Ready(ControllerStateMachine machine)
            {
            this.machine = machine;
            }

        public override void OnEnter() => machine.InReadyState.Set();

        public override void OnExit() => machine.InReadyState.Reset();

        public override void RotationDetected()
            {
            machine.TransitionToState(new Rotating(machine));
            }

        public override void ShutterMovementDetected()
            {
            machine.TransitionToState(new ShutterMoving(machine));
            }

        public override void StatusUpdateReceived(IHardwareStatus status)
            {
            machine.UpdateStatus(status);
            }
        }
    }