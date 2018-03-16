// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: Rotating.cs  Last modified: 2018-03-16@18:22 by Tim Long

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
            }

        public void OnExit()
            {
            }

        public void EncoderTickReceived(int encoderPosition)
            {
            }

        public void ShutterCurrentReadingReceived(int motorCurrent)
            {
            }

        public void StatusUpdateReceived(IHardwareStatus status)
            {
            }

        public string Name => nameof(Rotating);
        }
    }