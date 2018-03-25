﻿// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: Dome.cs  Last modified: 2018-03-25@04:52 by Tim Long

using System;
using System.Collections;
using System.Runtime.InteropServices;
using ASCOM;
using ASCOM.DeviceInterface;
using JetBrains.Annotations;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.Server;
using NotImplementedException = ASCOM.NotImplementedException;

namespace TA.DigitalDomeworks.AscomDome
    {
    [ProgId(SharedResources.DomeDriverId)]
    [Guid("CCF89F7D-2889-4A9D-891D-E28760A0FFCA")]
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    [DeviceId(SharedResources.DomeDriverId, DeviceName = SharedResources.DomeDriverName)]
    [ServedClassName(SharedResources.DomeDriverName)]
    public class Dome : ReferenceCountedObject, IDomeV2
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
            }

        public void AbortSlew()
            {
            controller.RequestEmergencyStop();
            }

        public void CloseShutter()
            {
            controller.CloseShutter();
            }

        public void FindHome()
            {
            throw new NotImplementedException();
            }

        public void OpenShutter()
            {
            controller.OpenShutter();
            }

        public void Park()
            {
            throw new NotImplementedException();
            }

        public void SetPark()
            {
            throw new NotImplementedException();
            }

        public void SlewToAltitude(double Altitude)
            {
            throw new NotImplementedException();
            }

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

        public string Description { get; }

        public string DriverInfo { get; }

        public string DriverVersion { get; }

        public short InterfaceVersion { get; }

        public string Name { get; }

        public ArrayList SupportedActions { get; }

        public double Altitude { get; }

        public bool AtHome { get; }

        public bool AtPark { get; }

        public double Azimuth { get; }

        public bool CanFindHome { get; }

        public bool CanPark { get; }

        public bool CanSetAltitude { get; }

        public bool CanSetAzimuth { get; }

        public bool CanSetPark { get; }

        public bool CanSetShutter { get; }

        public bool CanSlave { get; }

        public bool CanSyncAzimuth { get; }

        public ShutterState ShutterStatus { get; }

        public bool Slaved { get; set; }

        public bool Slewing { get; }
        }
    }