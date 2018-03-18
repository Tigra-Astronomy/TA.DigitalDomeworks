// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ControllerStateMachine.cs  Last modified: 2018-03-17@01:03 by Tim Long

using System;
using System.Threading;
using System.Threading.Tasks;
using NLog.Fluent;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
{
    internal class ShutterMoving : IControllerState
    {
        private ControllerStateMachine machine;
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

    public void EncoderTickReceived(int encoderPosition)
        {
        Log.Error()
            .Message($"Invalid trigger: {nameof(EncoderTickReceived)}")
            .Property(nameof(encoderPosition), encoderPosition)
            .Write();
        }

    public void ShutterCurrentReadingReceived(int motorCurrent)
        {
        machine.ShutterMotorCurrent = motorCurrent;
        ResetTimeout();
        }

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

    public void StatusUpdateReceived(IHardwareStatus status)
        {
        //ToDo: there is a potential race condition here if the timeout happens just as this method is called.
        timeoutCancellation?.Cancel();   
        machine.UpdateStatus(status);
        machine.TransitionToState(new Ready(machine));
        }

    public string Name => nameof(ShutterMoving);
    }
}