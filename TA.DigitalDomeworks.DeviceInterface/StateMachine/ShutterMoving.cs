// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ShutterMoving.cs  Last modified: 2018-03-20@00:56 by Tim Long

using System;
using NLog.Fluent;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    internal sealed class ShutterMoving : ControllerStateBase
        {
        /// <summary>
        ///     If no shutter movement indications are received for this long,
        ///     the state will time out and attempt to request a status update.
        /// </summary>
        private static readonly TimeSpan shutterTimeout = TimeSpan.FromSeconds(5);

        public ShutterMoving(ControllerStateMachine machine)
            {
            this.machine = machine;
            }

        public override void OnEnter()
            {
            base.OnEnter();
            ResetTimeout(shutterTimeout);
            machine.ShutterMotorActive = true;
            }

        public override void OnExit()
            {
            base.OnExit();
            machine.ShutterMotorCurrent = 0;
            machine.ShutterMotorActive = false;
            machine.ShutterMovementDirection = ShutterDirection.None;
            }

        public override void RotationDetected()
            {
            base.RotationDetected();
            Log.Error()
                .Message($"Invalid trigger: {nameof(RotationDetected)}")
                .Write();
            }

        public override void ShutterMovementDetected()
            {
            base.ShutterMovementDetected();
            ResetTimeout(shutterTimeout);
            }

        public override void StatusUpdateReceived(IHardwareStatus status)
            {
            base.StatusUpdateReceived(status);
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