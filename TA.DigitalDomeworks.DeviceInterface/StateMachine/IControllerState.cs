﻿// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: IControllerState.cs  Last modified: 2018-03-16@14:17 by Tim Long

using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    public interface IControllerState
        {
        /// <summary>
        ///     Called once when the state it first entered, but after the previous state's
        ///     <see cref="OnExit" /> method has been called.
        /// </summary>
        void OnEnter();

        /// <summary>
        ///     Called once when the state exits but before the next state's
        ///     <see cref="OnEnter" /> method is called.
        /// </summary>
        void OnExit();

        /// <summary>
        ///     Trigger: called to signal that dome rotation is detected.
        ///     This can be triggered by a dome rotation direction notification,
        ///     or by an azimuth encoder tick. States are not interested in the actual
        ///     encoder position, only that movement is detected.
        /// </summary>
        void RotationDetected();

        /// <summary>
        ///     Trigger: called to signal that a shutter motor current measurement
        ///     has been received.
        /// </summary>
        void ShutterMovementDetected();

        /// <summary>
        ///     Trigger: called when a status report is received from the controller.
        /// </summary>
        /// <param name="status">An object containing the current hardware state.</param>
        void StatusUpdateReceived(IHardwareStatus status);

        string Name { get; }
        }
    }