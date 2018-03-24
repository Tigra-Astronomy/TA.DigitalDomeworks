﻿// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: RxControllerActions.cs  Last modified: 2018-03-24@22:27 by Tim Long

using TA.Ascom.ReactiveCommunications;
using TI.DigitalDomeWorks;

namespace TA.DigitalDomeworks.DeviceInterface.StateMachine
    {
    public class RxControllerActions : IControllerActions
        {
        private readonly ICommunicationChannel channel;

        public RxControllerActions(ICommunicationChannel channel)
            {
            this.channel = channel;
            }

        public void RequestHardwareStatus()
            {
            channel.Send(Constants.CmdGetInfo);
            }

        public void PerformEmergencyStop()
            {
            channel.Send(Constants.CmdEmergencyStop);
            }

        public void RotateToAzimuth(int degreesClockwiseFromNorth)
            {
            var cmd = string.Format(Constants.CmdGotoAz, degreesClockwiseFromNorth);
            channel.Send(cmd);
            }

        public void OpenShutter()
            {
            channel.Send(Constants.CmdOpen);
            }

        public void CloseShutter()
            {
            channel.Send(Constants.CmdClose);
            }

        public void RotateToHomePosition()
            {
            channel.Send(Constants.CmdGotoHome);
            }
        }
    }