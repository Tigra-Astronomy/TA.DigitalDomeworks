// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ControllerStateMachine.cs  Last modified: 2018-03-17@01:03 by Tim Long

using System;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using NLog.Fluent;
using PostSharp.Patterns.Model;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    [NotifyPropertyChanged]
    internal class ControllerStateMachine : INotifyHardwareStateChanged
        {
        private readonly Action requestHardwareStatus;

        public ControllerStateMachine(Action requestHardwareStatus)
            {
            this.requestHardwareStatus = requestHardwareStatus;
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
        public void AzimuthEncoderTickReceived(int encoderPosition) =>
            CurrentState.EncoderTickReceived(encoderPosition);

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
                targetState.OnEnter();
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
            requestHardwareStatus();
            }

        public void ShutterMotorCurrentUpdated(int current)
            {
            CurrentState.ShutterCurrentReadingReceived(current);
            }
        }
    }