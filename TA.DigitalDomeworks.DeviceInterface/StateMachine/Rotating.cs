// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: Rotating.cs  Last modified: 2018-03-20@00:57 by Tim Long

using System;
using NLog.Fluent;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    internal sealed class Rotating : ControllerStateBase
        {
        private static readonly TimeSpan RotationTimeout = TimeSpan.FromSeconds(5);

        public Rotating(ControllerStateMachine machine)
            {
            this.machine = machine;
            }

        public override void OnEnter()
            {
            base.OnEnter();
            machine.AzimuthMotorActive = true;
            }

        public override void OnExit()
            {
            base.OnExit();
            machine.AzimuthMotorActive = false;
            }

        /// <summary>
        ///     Trigger: updates the encoder position
        /// </summary>
        public override void RotationDetected() => ResetTimeout(RotationTimeout);

        /// <summary>
        ///     Trigger: invalid for this state.
        /// </summary>
        public override void ShutterMovementDetected()
            {
            base.ShutterMovementDetected();
            Log.Error()
                .Message("Shutter movement detected while rotating. This is unexpected.")
                .Write();
            }

        /// <summary>
        ///     Trigger: => Ready.
        /// </summary>
        /// <param name="status"></param>
        public override void StatusUpdateReceived(IHardwareStatus status)
            {
            CancelTimeout();
            machine.UpdateStatus(status);
            machine.TransitionToState(new Ready(machine));
            }

        protected override void HandleTimeout()
            {
            base.HandleTimeout();
            machine.TransitionToState(new RequestStatus(machine));
            }
        }
    }