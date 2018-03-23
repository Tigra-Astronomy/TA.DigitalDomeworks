// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ControllerStateMachine.cs  Last modified: 2018-03-17@01:03 by Tim Long

using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using System.Threading;
using JetBrains.Annotations;
using NLog.Fluent;
using PostSharp.Patterns.Model;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.SharedTypes;
using PostSharp.Patterns.Threading;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    [NotifyPropertyChanged]
    public class ControllerStateMachine : INotifyHardwareStateChanged
        {
        internal IControllerActions ControllerActions { get; }

        internal readonly ManualResetEvent InReadyState = new ManualResetEvent(false);

        public ControllerStateMachine(IControllerActions controllerActions)
            {
            this.ControllerActions = controllerActions;
            CurrentState = new Uninitialized();
            }
        internal IControllerState CurrentState { get; private set; }

        public int AzimuthEncoderPosition { get; internal set; }

        public int ShutterMotorCurrent { get; internal set; }

        public RotationDirection AzimuthDirection { get; internal set; }

        public ShutterDirection ShutterMovementDirection { get; internal set; }

        public bool AzimuthMotorActive { get; internal set; }

        public bool ShutterMotorActive { get; internal set; }

        /// <summary>
        /// Initializes the state machine and optionally sets the starting state.
        /// </summary>
        /// <param name="startState"></param>
        public void Initialize(IControllerState startState)
            {
            TransitionToState(startState);
            }

        #region State triggers 
        public void AzimuthEncoderTickReceived(int encoderPosition)
            {
            AzimuthEncoderPosition = encoderPosition;
            CurrentState.RotationDetected();
            }

        public void HardwareStatusReceived(IHardwareStatus status) =>
            CurrentState.StatusUpdateReceived(status);

        #endregion State triggers

        public void TransitionToState([NotNull] IControllerState targetState)
            {
            if (targetState == null) throw new ArgumentNullException(nameof(targetState));
            try
                {
                CurrentState.OnExit();
                }
            catch (Exception ex)
                {
                Log.Error()
                    .Exception(ex)
                    .Message($"Unexpected exception leaving state {CurrentState.Name}")
                    .Write();
                }

            CurrentState = new StateLoggingDecorator(targetState);
            try
                {
                CurrentState.OnEnter();
                }
            catch (Exception ex)
                {
                Log.Error()
                    .Exception(ex)
                    .Message($"Unexpected exception entering state {targetState.Name}")
                    .Write();
                }
            }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected void OnPropertyChanged([CallerMemberName]string propertyName = null)
            {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        /// <summary>
        /// Update state machine properties from a <see cref="IHardwareStatus"/> record.
        /// By definition, when a status report is received, all movement has ceased.
        /// </summary>
        /// <param name="status">The status report received from the hardware.</param>
        internal void UpdateStatus(IHardwareStatus status)
            {
            AzimuthEncoderPosition = status.CurrentAzimuth;
            AzimuthMotorActive = false;
            AzimuthDirection = RotationDirection.None;
            ShutterMotorActive = false;
            ShutterMovementDirection = ShutterDirection.None;
            ShutterMotorCurrent = 0;
            }

        internal void RequestHardwareStatus()
            {
            ControllerActions.RequestHardwareStatus();
            }

        public void ShutterMotorCurrentReceived(int current)
            {
            ShutterMotorCurrent = current;
            CurrentState.ShutterMovementDetected();
            }

        public void ShutterDirectionReceived(ShutterDirection direction)
            {
            ShutterMovementDirection = direction;
            CurrentState.ShutterMovementDetected();
            }

        public void AllStop()
            {
            Log.Warn().Message("Emergency Stop requested").Write();
            ControllerActions.PerformEmergencyStop();
            }

        public void RotationDirectionReceived(RotationDirection direction)
            {
            AzimuthDirection = direction;
            CurrentState.RotationDetected();
            }

        public void WaitForReady(TimeSpan timeout)
            {
            var signalled = InReadyState.WaitOne(timeout);
            if (!signalled)
                {
                var message = $"State machine did not enter the ready state within the allotted time of {timeout}";
                Log.Error().Message(message).Write();
                throw new TimeoutException(message);
                }
            }

        public void RotateToAzimuthDegrees(double azimuth)
            {
            CurrentState.RotateToAzimuthDegrees(azimuth);
            }
        }
    }