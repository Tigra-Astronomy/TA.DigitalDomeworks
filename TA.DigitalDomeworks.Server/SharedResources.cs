// This file is part of the GTD.Integra.FocusingRotator project
// 
// Copyright © 2016-2017 Tigra Astronomy., all rights reserved.
// 
// File: SharedResources.cs  Last modified: 2017-02-27@19:16 by Tim Long

using System;
using System.Windows.Forms;
using NLog;
using TA.DigitalDomeworks.Server.Properties;

namespace TA.DigitalDomeworks.Server
    {
    /// <summary>
    ///     The resources shared by all drivers and devices. This is how ASCOM drivers will obtain
    ///     an instance of the <see cref="IntegraController" />.
    /// </summary>
    public static class SharedResources
        {
        public const string DomeDriverId = "ASCOM.DigitalDomeworks2018.Dome";
        public const string SwitchDriverId = "ASCOM.DigitalDomeworks2018.Switch";
        public const string DomeDriverName = "Digital Domeworks 2018 Reboot";
        public const string SwitchDriverName = "Digital Domeworks 2018 Reboot";

        private static readonly ILogger Log;

        static SharedResources()
            {
            Log = LogManager.GetCurrentClassLogger();
            ConnectionManager = CreateConnectionManager();
            }

        /// <summary>
        ///     Gets the connection manager.
        /// </summary>
        /// <value>The connection manager.</value>
        public static ClientConnectionManager ConnectionManager { get; }


        public static void DoSetupDialog(Guid clientId)
            {
            var oldConnectionString = Settings.Default.ConnectionString;
            Log.Info($"SetupDialog requested by client {clientId}");
            using (var F = new SetupDialogForm())
                {
                var result = F.ShowDialog();
                switch (result)
                    {
                        case DialogResult.OK:
                            Log.Info($"SetupDialog successful, saving settings");
                            Settings.Default.Save();
                            var newConnectionString = Settings.Default.ConnectionString;
                            if (oldConnectionString != newConnectionString)
                                {
                                Log.Warn(
                                    $"Connection string has changed from {oldConnectionString} to {newConnectionString} - replacing the TansactionProcessorFactory");
                                }
                            break;
                        default:
                            Log.Warn("SetupDialog cancelled or failed - reverting to previous settings");
                            Settings.Default.Reload();
                            break;
                    }
                }
            }

        static ClientConnectionManager CreateConnectionManager()
            {
            Log.Info("Creating ClientConnectionManager");
            return new ClientConnectionManager();
            }
        }
    }