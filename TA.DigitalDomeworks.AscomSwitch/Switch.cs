// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: Switch.cs  Last modified: 2018-03-28@01:08 by Tim Long

using System;
using System.Collections;
using System.Runtime.InteropServices;
using ASCOM;
using ASCOM.DeviceInterface;
using JetBrains.Annotations;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.Server;
using TA.PostSharp.Aspects;
using NotImplementedException = ASCOM.NotImplementedException;

namespace TA.DigitalDomeworks.AscomSwitch
    {
    [ProgId(SharedResources.SwitchDriverId)]
    [Guid("8f3d72d5-7fb8-4f8a-8f73-3c724a8a375c")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [DeviceId(SharedResources.SwitchDriverId, DeviceName = SharedResources.SwitchDriverName)]
    [ServedClassName(SharedResources.SwitchDriverName)]
    public class Switch : ReferenceCountedObject, ISwitchV2, IAscomDriver
        {
        private readonly Guid clientId;
        [CanBeNull] private DeviceController controller;

        public Switch()
            {
            clientId = SharedResources.ConnectionManager.RegisterClient("ASCOM Switch");
            }

        public void SetupDialog()
            {
            SharedResources.DoSetupDialog(clientId);
            }

        public string Action(string ActionName, string ActionParameters)
            {
            throw new NotImplementedException();
            }

        public void CommandBlind(string Command, bool Raw = false)
            {
            throw new NotImplementedException();
            }

        public bool CommandBool(string Command, bool Raw = false)
            {
            throw new NotImplementedException();
            }

        public string CommandString(string Command, bool Raw = false)
            {
            throw new NotImplementedException();
            }

        public void Dispose()
            {
            SharedResources.ConnectionManager.GoOffline(clientId);
            SharedResources.ConnectionManager.UnregisterClient(clientId);
            controller = null; //[Sentinel]
            }

        public string GetSwitchName(short id) => id.ToString();

        public void SetSwitchName(short id, string name)
            {
            throw new NotImplementedException();
            }

        public string GetSwitchDescription(short id) => $"Switch {id}";

        public bool CanWrite(short id) => true;

        [MustBeConnected]
        public bool GetSwitch(short id) => controller?.UserPins[id] ?? false;

        [MustBeConnected]
        public void SetSwitch(short id, bool state) => controller?.SetUserOutputPin(id, state);

        public double MaxSwitchValue(short id) => 1.0;

        public double MinSwitchValue(short id) => 0.0;

        public double SwitchStep(short id) => 1.0;

        [MustBeConnected]
        public double GetSwitchValue(short id) => controller?.UserPins[id] ?? false ? 1 : 0;

        [MustBeConnected]
        public void SetSwitchValue(short id, double value) => SetSwitch(id, value >= 0.5);

        public bool Connected
            {
            get => controller?.IsConnected ?? false;
            set
                {
                if (value)
                    {
                    controller = SharedResources.ConnectionManager.GoOnline(clientId);
                    }
                else
                    {
                    SharedResources.ConnectionManager.GoOffline(clientId);
                    controller = null; //[Sentinel]
                    }
                }
            }

        public string Description => "ASCOM Dome driver for Digital Domeworks";

        public string DriverInfo => @"ASCOM Dome driver for Digital Domeworks, 2018 reboot
An open source ASCOM driver by Tigra Astronomy
Home page: http://tigra-astronomy.com
Source code: https://bitbucket.org/tigra-astronomy/ta.digitaldomeworks
License: https://tigra.mit-license.org/
Copyright © 2018 Tigra Astronomy";

        public string DriverVersion => "7.0";

        public short InterfaceVersion => 2;

        public string Name => "Digital Domeworks 2018 Reboot";

        public ArrayList SupportedActions => new ArrayList();

        public short MaxSwitch => 4;
        }
    }