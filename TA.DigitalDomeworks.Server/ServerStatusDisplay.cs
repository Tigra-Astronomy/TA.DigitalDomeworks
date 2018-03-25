// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ServerStatusDisplay.cs  Last modified: 2018-03-24@23:40 by Tim Long

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading;
using System.Windows.Forms;
using ASCOM.Controls;
using JetBrains.Annotations;
using TA.DigitalDomeworks.Server.Properties;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.Server
    {
    public partial class ServerStatusDisplay : Form
        {
        [NotNull] private readonly List<IDisposable> disposableSubscriptions = new List<IDisposable>();
        private IDisposable clientStatusSubscription;

        public ServerStatusDisplay()
            {
            InitializeComponent();
            }

        private void frmMain_Load(object sender, EventArgs e)
            {
            ConfigureAnnunciators();
            var clientStatusObservable = Observable.FromEventPattern<EventHandler<EventArgs>, EventArgs>(
                handler => SharedResources.ConnectionManager.ClientStatusChanged += handler,
                handler => SharedResources.ConnectionManager.ClientStatusChanged -= handler);
            clientStatusSubscription = clientStatusObservable
                .ObserveOn(SynchronizationContext.Current)
                .Subscribe(ObserveClientStatusChanged);
            ObserveClientStatusChanged(null); // This sets the initial UI state before any notifications arrive
            }

        private void ConfigureAnnunciators()
            {
            var annunciators = new List<Annunciator>();
            annunciators.Add(AzimuthMotorAnnunciator);
            annunciators.Add(ClockwiseAnnunciator);
            annunciators.Add(CounterClockwiseAnnunciator);
            annunciators.Add(ShutterMotorAnnunciator);
            annunciators.Add(ShutterOpeningAnnunciator);
            annunciators.Add(ShutterClosingAnnunciator);
            annunciators.ForEach(p => p.Mute = false);
            annunciators.ForEach(p => p.Cadence = CadencePattern.SteadyOn);
            AzimuthMotorAnnunciator.Cadence = CadencePattern.BlinkAlarm;
            ShutterMotorAnnunciator.Cadence = CadencePattern.BlinkAlarm;
            annunciators.ForEach(p => p.Mute = true);
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
            if (clients.Count == 1)
                ConfigureAnnunciators();
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
            disposableSubscriptions.ForEach(p => p.Dispose());
            disposableSubscriptions.Clear();
            }

        private void SetUiButtonState()
            {
            if (!SharedResources.ConnectionManager.MaybeControllerInstance.Any() ||
                SharedResources.ConnectionManager.OnlineClientCount < 1)
                return;
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
            disposableSubscriptions.Add(
                controller
                    .GetObservableValueFor(p => p.AzimuthMotorActive)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(motorActive => AzimuthMotorAnnunciator.Mute = !motorActive)
            );
            disposableSubscriptions.Add(
                controller
                    .GetObservableValueFor(p => p.AzimuthDirection)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(SetRotationDirection)
            );
            disposableSubscriptions.Add(
                controller
                    .GetObservableValueFor(p => p.AzimuthDegrees)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(SetAzimuthPosition)
            );
            disposableSubscriptions.Add(
                controller.GetObservableValueFor(p => p.ShutterMotorActive)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(motorActive => ShutterMotorAnnunciator.Mute = !motorActive)
            );
            disposableSubscriptions.Add(
                controller
                    .GetObservableValueFor(p => p.ShutterMovementDirection)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(SetShutterDirection)
            );
            disposableSubscriptions.Add(
                controller
                    .GetObservableValueFor(p => p.ShutterMotorCurrent)
                    .ObserveOn(SynchronizationContext.Current)
                    .Subscribe(SetShutterMotorCurrent)
            );
            }

        private void SetAzimuthPosition(float position)
            {
            var format = AzimuthPositionAnnunciator.Tag.ToString();
            var formattedPosition = string.Format(format, (int)position);
            AzimuthPositionAnnunciator.Text = formattedPosition;
            }

        private void SetShutterMotorCurrent(int current)
            {
            var format = ShutterCurrentAnnunciator.Tag.ToString();
            var formattedValue = string.Format(format, current);
            ShutterCurrentAnnunciator.Text = formattedValue;
            // Auto-scale the progress bar
            ShutterCurrentBar.Maximum = Math.Max(ShutterCurrentBar.Maximum, current);
            ShutterCurrentBar.Value = current;
            }

        private void SetRotationDirection(RotationDirection direction)
            {
            CounterClockwiseAnnunciator.Mute = direction != RotationDirection.CounterClockwise;
            ClockwiseAnnunciator.Mute = direction != RotationDirection.Clockwise;
            }

        private void SetShutterDirection(ShutterDirection direction)
            {
            ShutterOpeningAnnunciator.Mute = direction != ShutterDirection.Opening;
            ShutterClosingAnnunciator.Mute = direction != ShutterDirection.Closing;
            }


        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
            {
            clientStatusSubscription?.Dispose();
            UnsubscribePropertyChangeNotifications();
            var clients = SharedResources.ConnectionManager.Clients;
            foreach (var client in clients) SharedResources.ConnectionManager.GoOffline(client.ClientId);
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