// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: Ready.cs  Last modified: 2018-03-16@18:14 by Tim Long

using System;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    internal sealed class Ready : IControllerState
        {
        private readonly ControllerStateMachine machine;

        public Ready(ControllerStateMachine machine)
            {
            this.machine = machine;
            }

        public string Name => nameof(Ready);

        public void OnEnter() { }

        public void OnExit() { }

        public void EncoderTickReceived(int encoderPosition)
            {
            machine.AzimuthEncoderPosition = encoderPosition;
            machine.TransitionToState(new Rotating(machine));
            }

        public void ShutterCurrentReadingReceived(int motorCurrent)
            {
            machine.ShutterMotorCurrent = motorCurrent;
            machine.TransitionToState(new ShutterMoving(machine));
            }

        public void StatusUpdateReceived(IHardwareStatus status)
            {
            throw new NotImplementedException();
            }
        }
    }