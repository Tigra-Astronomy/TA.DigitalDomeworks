// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: RxControllerActions.cs  Last modified: 2018-03-20@01:16 by Tim Long

using System;
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
            throw new NotImplementedException();
            }

        public void OpenShutter()
            {
            throw new NotImplementedException();
            }

        public void CloseShutter()
            {
            throw new NotImplementedException();
            }

        public void RotateToHomePosition()
            {
            throw new NotImplementedException();
            }
        }
    }