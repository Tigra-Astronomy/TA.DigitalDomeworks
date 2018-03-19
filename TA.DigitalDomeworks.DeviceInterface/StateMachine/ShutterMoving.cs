// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ShutterMoving.cs  Last modified: 2018-03-18@17:02 by Tim Long

using System;
using System.Threading;
using System.Threading.Tasks;
using NLog.Fluent;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    internal class ShutterMoving : IControllerState
        {
        private readonly ControllerStateMachine machine;
        private CancellationTokenSource timeoutCancellation;

        public ShutterMoving(ControllerStateMachine machine)
            {
            this.machine = machine;
            }

        public void OnEnter()
            {
            ResetTimeout();
            machine.ShutterMotorActive = true;
            }

        public void OnExit()
            {
            timeoutCancellation?.Cancel();
            machine.ShutterMotorCurrent = 0;
            machine.ShutterMotorActive = false;
            machine.ShutterMovementDirection = ShutterDirection.None;
            }

        public void RotationDetected()
            {
            Log.Error()
                .Message($"Invalid trigger: {nameof(RotationDetected)}")
                .Write();
            }

        public void ShutterMovementDetected()
            {
            ResetTimeout();
            }

        public void StatusUpdateReceived(IHardwareStatus status)
            {
            //ToDo: there is a potential race condition here if the timeout happens just as this method is called.
            timeoutCancellation?.Cancel();
            machine.UpdateStatus(status);
            machine.TransitionToState(new Ready(machine));
            }

        public string Name => nameof(ShutterMoving);

        private async Task ResetTimeout()
            {
            timeoutCancellation?.Cancel();
            timeoutCancellation = new CancellationTokenSource();
            await HandleShutterTimeoutAsync(timeoutCancellation.Token);
            }

        private async Task HandleShutterTimeoutAsync(CancellationToken cancel)
            {
            await Task.Delay(TimeSpan.FromSeconds(5), cancel);
            if (cancel.IsCancellationRequested)
                return;
            Log.Warn().Message("Shutter movement timed out").Write();
            machine.TransitionToState(new RequestStatus(machine));
            }
        }
    }