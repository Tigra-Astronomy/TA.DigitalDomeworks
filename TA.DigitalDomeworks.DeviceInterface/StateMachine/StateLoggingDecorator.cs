// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: StateLoggingDecorator.cs  Last modified: 2018-03-16@14:10 by Tim Long

using NLog.Fluent;
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
            Log.Info()
                .Message($"Entering state {decoratedState.Name}")
                .Write();
            decoratedState.OnEnter();
            }

        public void OnExit()
            {
            Log.Info()
                .Message($"Exiting state {decoratedState.Name}")
                .Write();
            decoratedState.OnExit();
            }

        public void EncoderTickReceived(int encoderPosition)
            {
            Log.Info()
                .Message($"[{decoratedState.Name}] Encoder tick value={encoderPosition}")
                .Write();
            decoratedState.EncoderTickReceived(encoderPosition);
            }

        public void ShutterCurrentReadingReceived(int motorCurrent)
            {
            Log.Info()
                .Message($"[{decoratedState.Name}] Shutter current value={motorCurrent}")
                .Write();
            decoratedState.ShutterCurrentReadingReceived(motorCurrent);
            }

        public void StatusUpdateReceived(IHardwareStatus status)
            {
            Log.Info()
                .Message($"[{decoratedState.Name}] Status update")
                .Write();
            decoratedState.StatusUpdateReceived(status);
            }
        }
    }