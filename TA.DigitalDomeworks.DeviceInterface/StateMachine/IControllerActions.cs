// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: IControllerActions.cs  Last modified: 2018-03-19@17:03 by Tim Long

using System;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    internal interface IControllerActions
        {
        /// <summary>
        ///     Requests that the controller send a status report on the current state of the hardware.
        /// </summary>
        void RequestHardwareStatus();

        /// <summary>
        ///     Instructs the controller to immediately stop all movement.
        /// </summary>
        void PerformEmergencyStop();

        /// <summary>
        ///     Requests that the controller rotate to the specified azimuth position.
        /// </summary>
        void RotateToAzimuth(int degreesClockwiseFromNorth);

        /// <summary>
        ///     Requests that the controller open the shutter.
        /// </summary>
        void OpenShutter();

        /// <summary>
        ///     Requests that the controller close the shutter.
        /// </summary>
        void CloseShutter();

        /// <summary>
        ///     Requests that the controller rotates to the azimuth that it considers to be the home position.
        /// </summary>
        void RotateToHomePosition();
        }
    }