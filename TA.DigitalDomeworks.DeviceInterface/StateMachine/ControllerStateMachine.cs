// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ControllerStateMachine.cs  Last modified: 2018-03-17@01:03 by Tim Long

using System;
using System.Diagnostics.Contracts;
using JetBrains.Annotations;
using NLog.Fluent;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    internal class ControllerStateMachine
        {
        public ControllerStateMachine()
            {
            CurrentState = new Uninitialized();
            }
        internal IControllerState CurrentState { get; private set; }

        public int AzimuthEncoderPosition { get; internal set; }

        /// <summary>
        /// Initializes the state machine and optionally sets the starting state.
        /// </summary>
        /// <param name="startState"></param>
        public void Initialize(IControllerState startState = null)
            {
            TransitionToState(startState ?? new Ready(this));
            }

        public void AzimuthEncoderTickReceived(int encoderPosition) =>
            CurrentState.EncoderTickReceived(encoderPosition);

        public void TransitionToState([NotNull] IControllerState targetState)
            {
            Contract.Requires(targetState != null);
            Contract.Ensures(CurrentState != null, "Make sure that Initialize() has been called");
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
        }
    }