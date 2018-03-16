// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: StateLoggingDecorator.cs  Last modified: 2018-03-16@14:10 by Tim Long

using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    internal class StateLoggingDecorator : IControllerState
        {
        private readonly IControllerState decoratedState;

        public StateLoggingDecorator(IControllerState targetState)
        {
            this.decoratedState = targetState;
        }

        public string Name => decoratedState.Name;

        public void OnEnter()
            {
            decoratedState.OnEnter();
            }

        public void OnExit()
            {
            decoratedState.OnExit();
            }

        public void EncoderTickReceived(int encoderPosition)
            {
            decoratedState.EncoderTickReceived(encoderPosition);
            }

        public void ShutterCurrentReadingReceived(int motorCurrent)
            {
            decoratedState.ShutterCurrentReadingReceived(motorCurrent);
            }

        public void StatusUpdateReceived(IHardwareStatus status)
            {
            decoratedState.StatusUpdateReceived(status);
            }
        }
    }