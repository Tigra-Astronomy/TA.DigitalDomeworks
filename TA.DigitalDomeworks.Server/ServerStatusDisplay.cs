// This file is part of the GTD.Integra.FocusingRotator project
// 
// Copyright © 2016-2017 Tigra Astronomy., all rights reserved.
// 
// File: ServerStatusDisplay.cs  Last modified: 2017-03-01@01:17 by Tim Long

using System;
using System.Drawing;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using ASCOM.Controls;
using TA.DigitalDomeworks.Server.Properties;

namespace TA.DigitalDomeworks.Server
    {
    public partial class ServerStatusDisplay : Form
        {
        private IDisposable clientStatusSubscription;

        public ServerStatusDisplay()
            {
            InitializeComponent();
            }

        private void frmMain_Load(object sender, EventArgs e)
            {
            var clientStatusObservable = Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(
                handler => SharedResources.ConnectionManager.ClientStatusChanged += handler,
                handler => SharedResources.ConnectionManager.ClientStatusChanged -= handler);
            clientStatusSubscription = clientStatusObservable
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(ObserveClientStatusChanged);
            ObserveClientStatusChanged(null); // This sets the initial UI state before any notifications arrive
            }


        private void ObserveClientStatusChanged(EventPattern<EventArgs> eventPattern)
            {
            SetUiButtonState();
            SetUiDeviceConnectedState();
            var clientStatus = SharedResources.ConnectionManager.Clients;
            registeredClientCount.Text = clientStatus.Count().ToString();
            OnlineClients.Text = clientStatus.Count(p => p.Online).ToString();
            ConfigureUiPropertyNotifications();
            }

        /// <summary>
        ///     Enables each device activity annunciator if there are any clients connected to that
        ///     device.
        /// </summary>
        private void SetUiDeviceConnectedState()
            {
            var clients = SharedResources.ConnectionManager.Clients;
            }

        /// <summary>
        ///     Begins or terminates UI updates depending on the number of online clients.
        /// </summary>
        private void ConfigureUiPropertyNotifications()
            {
            var clientsOnline = SharedResources.ConnectionManager.OnlineClientCount;
            if (clientsOnline > 0)
                SubscribePropertyChangeNotifications();
            else
                UnsubscribePropertyChangeNotifications();
            }

        /// <summary>
        ///     Stops observing the controller property change notifications.
        /// </summary>
        private void UnsubscribePropertyChangeNotifications()
            {
            }

        private void SetUiButtonState()
            {
            if (!SharedResources.ConnectionManager.MaybeControllerInstance.Any() ||
                SharedResources.ConnectionManager.OnlineClientCount < 1)
                {
                // No controller instance or no clients, so disable everything and return.
                return;
                }
            var controller = SharedResources.ConnectionManager.MaybeControllerInstance.Single();
            }

        /// <summary>
        ///     Creates subscriptions that observe property change notifications and update the UI as changes occur.
        /// </summary>
        private void SubscribePropertyChangeNotifications()
            {
            if (!SharedResources.ConnectionManager.MaybeControllerInstance.Any())
                return;
            var controller = SharedResources.ConnectionManager.MaybeControllerInstance.Single();
            /* ToDo:
             * Add subscriptions to PropertyChanged notifications using this pattern:
             *  movingSubscription = controller
             *      .GetObservableValueFor(m => m.IsMoving)
             *      .ObserveOn(SynchronizationContext.Current)
             *      .Subscribe(SetMotorMovingState);
             */

        }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
            {
            clientStatusSubscription?.Dispose();
            UnsubscribePropertyChangeNotifications();
            var clients = SharedResources.ConnectionManager.Clients;
            foreach (var client in clients)
                {
                SharedResources.ConnectionManager.GoOffline(client.ClientId);
                }
            Application.Exit();
            }

        private void SetupCommand_Click(object sender, EventArgs e)
            {
            SharedResources.DoSetupDialog(default(Guid));
            }

        private void frmMain_LocationChanged(object sender, EventArgs e)
            {
            Settings.Default.Save();
            }
        }
    }