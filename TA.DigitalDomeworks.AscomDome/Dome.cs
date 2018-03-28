// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: Dome.cs  Last modified: 2018-03-25@04:52 by Tim Long

using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ASCOM;
using ASCOM.DeviceInterface;
using JetBrains.Annotations;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.Server;
using TA.DigitalDomeworks.SharedTypes;
using TA.PostSharp.Aspects;
using NotImplementedException = ASCOM.NotImplementedException;

namespace TA.DigitalDomeworks.AscomDome
    {
    [ProgId(SharedResources.DomeDriverId)]
    [Guid("CCF89F7D-2889-4A9D-891D-E28760A0FFCA")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [DeviceId(SharedResources.DomeDriverId, DeviceName = SharedResources.DomeDriverName)]
    [ServedClassName(SharedResources.DomeDriverName)]
    public class Dome : ReferenceCountedObject, IDomeV2, IAscomDriver
        {
        [NotNull] private readonly Guid clientId;
        [CanBeNull] private DeviceController controller;

        public Dome()
            {
            clientId = SharedResources.ConnectionManager.RegisterClient("ASCOM Dome");
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

        [MustBeConnected]
        public void AbortSlew()
            {
            controller.RequestEmergencyStop();
            }

        [MustBeConnected]
        public void CloseShutter()
            {
            controller.CloseShutter();
            }

        [MustBeConnected]
        public void FindHome()
            {
            throw new NotImplementedException();
            }

        [MustBeConnected]
        public void OpenShutter()
            {
            controller.OpenShutter();
            }

        [MustBeConnected]
        public void Park()
            {
            controller.Park();
            AtPark = controller.AtHome && controller.ShutterPosition == SensorState.Closed;
            }

        public void SetPark()
            {
            throw new NotImplementedException();
            }

        public void SlewToAltitude(double Altitude)
            {
            throw new NotImplementedException();
            }

        [MustBeConnected]
        public void SlewToAzimuth(double Azimuth)
            {
            controller.SlewToAzimuth(Azimuth);
            }

        public void SyncToAzimuth(double Azimuth)
            {
            throw new NotImplementedException();
            }

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

        public double Altitude => throw new PropertyNotImplementedException(nameof(Altitude), accessorSet: false);

        [MustBeConnected]
        public bool AtHome => controller.AtHome;

        [MustBeConnected]
        public bool AtPark { get; private set; }

        [MustBeConnected]
        public double Azimuth => controller.AzimuthDegrees;

        public bool CanFindHome => true;

        public bool CanPark => true;

        public bool CanSetAltitude => false;

        public bool CanSetAzimuth => true;

        public bool CanSetPark => false;

        public bool CanSetShutter => true;

        public bool CanSlave => false;

        public bool CanSyncAzimuth => false;

        [MustBeConnected]
        public ShutterState ShutterStatus
            {
            get
                {
                if (controller.ShutterMovementDirection == ShutterDirection.Closing)
                    return ShutterState.shutterClosing;
                if (controller.ShutterMovementDirection == ShutterDirection.Opening)
                    return ShutterState.shutterOpening;
                if (controller.ShutterPosition == SensorState.Closed)
                    return ShutterState.shutterClosed;
                if (controller.ShutterPosition == SensorState.Open)
                    return ShutterState.shutterOpen;
                return ShutterState.shutterError;
                }
            }

        public bool Slaved
            {
            get => false;
            set => throw new NotImplementedException();
            }

        [MustBeConnected]
        public bool Slewing => controller.IsMoving;
        }
    }