// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: Uninitialized.cs  Last modified: 2018-03-17@01:37 by Tim Long

using System;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    internal class Uninitialized : IControllerState
        {
        private readonly InvalidOperationException uninitialized =
            new InvalidOperationException("Call Initialize() before using the state machine");

        public void OnEnter()
            {
            throw uninitialized;
            }

        public void OnExit() { }

        public void EncoderTickReceived(int encoderPosition)
            {
            throw uninitialized;
            }

        public void ShutterCurrentReadingReceived(int motorCurrent)
            {
            throw uninitialized;
            }

        public void StatusUpdateReceived(IHardwareStatus status)
            {
            throw uninitialized;
            }

        public string Name => nameof(Uninitialized);
        }
    }