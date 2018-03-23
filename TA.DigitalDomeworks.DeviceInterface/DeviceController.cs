// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: DeviceController.cs  Last modified: 2018-03-21@18:34 by Tim Long

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NLog.Fluent;
using PostSharp.Patterns.Model;
using TA.Ascom.ReactiveCommunications;
using TA.Ascom.ReactiveCommunications.Diagnostics;
using TA.DigitalDomeworks.DeviceInterface.StateMachine;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface
    {
    [NotifyPropertyChanged]
    public class DeviceController : INotifyPropertyChanged
        {
        [NotNull] private readonly ICommunicationChannel channel;
        [NotNull] private readonly List<IDisposable> disposableSubscriptions = new List<IDisposable>();
        [NotNull] private readonly ControllerStateMachine stateMachine;
        [NotNull] private readonly ControllerStatusFactory statusFactory;
        [CanBeNull] private ReactiveTransactionProcessor transactionProcessor;

        public DeviceController(ICommunicationChannel channel, ControllerStatusFactory factory,
            ControllerStateMachine machine)
            {
            this.channel = channel;
            statusFactory = factory;
            stateMachine = machine;
            }

        public INotifyHardwareStateChanged HardwareState => stateMachine;

        /// <summary>
        ///     <c>true</c> if any part of the building is moving.
        /// </summary>
        public bool IsMoving => HardwareState.AzimuthMotorActive || HardwareState.ShutterMotorActive;

        public event PropertyChangedEventHandler PropertyChanged;

        public void Open(bool performOnConnectActions = true)
            {
            var observer = new TransactionObserver(channel);
            transactionProcessor = new ReactiveTransactionProcessor();
            transactionProcessor.SubscribeTransactionObserver(observer);
            channel.Open();
            SubscribeControllerEvents();
            if (performOnConnectActions)
                stateMachine.Initialize(new RequestStatus(stateMachine));
            else
                stateMachine.Initialize(new Ready(stateMachine));
            stateMachine.WaitForReady(TimeSpan.FromSeconds(5));
            }

        private void SubscribeControllerEvents()
            {
            SubscribeAzimuthEncoderTicks();
            SubscribeRotationDirection();
            SubscribeShutterCurrentReadings();
            SubscribeShutterDirection();
            SubscribeStatusUpdates();
            }

        private void SubscribeStatusUpdates()
            {
            var statusUpdates = channel.ObservableReceivedCharacters.StatusUpdates(statusFactory);
            var statusUpdateSubscription = statusUpdates
                //.ObserveOn(Scheduler.Default)
                .Subscribe(StatusUpdateOnNext,
                    ex => throw new InvalidOperationException(
                        "Status Update sequence produced an unexpected error (see inner exception)", ex),
                    () => throw new InvalidOperationException(
                        "Status Update sequence completed unexpectedly, this is probably a bug")
                );
            disposableSubscriptions.Add(statusUpdateSubscription);
            }

        private void StatusUpdateOnNext(IHardwareStatus statusNotification)
            {
            try
                {
                stateMachine.HardwareStatusReceived(statusNotification);
                }
            catch (Exception ex)
                {
                Log.Error()
                    .Exception(ex)
                    .Message($"Error while processing status notification: {statusNotification}")
                    .Write();
                }
            }

        private void SubscribeShutterDirection()
            {
            // Note: The zero-based index in the string must match the ordinal values in ShutterDirection
            const string shutterMovementIndicators = "SCO";
            var shutterDirectionSequence = from c in channel.ObservableReceivedCharacters
                                           where shutterMovementIndicators.Contains(c)
                                           let ordinal = shutterMovementIndicators.IndexOf(c)
                                           let direction = (ShutterDirection) ordinal
                                           select direction;
            var shutterDirectionSubscription = channel.ObservableReceivedCharacters.ShutterDirectionUpdates()
                //.ObserveOn(Scheduler.Default)
                .Subscribe(
                    stateMachine.ShutterDirectionReceived,
                    ex => throw new InvalidOperationException(
                        "Shutter Direction sequence produced an unexpected error (see ineer exception)", ex),
                    () => throw new InvalidOperationException(
                        "Shutter Direction sequence completed unexpectedly, this is probably a bug")
                );
            disposableSubscriptions.Add(shutterDirectionSubscription);
            }

        private void SubscribeShutterCurrentReadings()
            {
            var shutterCurrentReadings = channel.ObservableReceivedCharacters.ShutterCurrentReadings();
            var shutterCurrentSubscription = shutterCurrentReadings
                //.ObserveOn(Scheduler.Default)
                .Subscribe(
                    stateMachine.ShutterMotorCurrentReceived,
                    ex => throw new InvalidOperationException(
                        "Shutter Current sequence produced an unexpected error (see inner exception)", ex),
                    () => throw new InvalidOperationException(
                        "ShutterCurrent sequence completed unexpectedly, this is probably a bug")
                );
            disposableSubscriptions.Add(shutterCurrentSubscription);
            }

        private void SubscribeRotationDirection()
            {
            var rotationDirectionSequence = from c in channel.ObservableReceivedCharacters
                                            where c == 'L' || c == 'R'
                                            let direction = c == 'L'
                                                ? RotationDirection.CounterClockwise
                                                : RotationDirection.Clockwise
                                            select direction;
            var rotationDirectionSubscription = rotationDirectionSequence
                .Trace("RotationDirection")
                //.ObserveOn(Scheduler.Default)
                .Subscribe(
                    stateMachine.RotationDirectionReceived,
                    ex => throw new InvalidOperationException(
                        "RotationDirection sequence produced an unexpected error (see ineer exception)", ex),
                    () => throw new InvalidOperationException(
                        "RotationDirection sequence completed unexpectedly, this is probably a bug")
                );
            disposableSubscriptions.Add(rotationDirectionSubscription);
            }

        private void SubscribeAzimuthEncoderTicks()
            {
            var azimuthEncoderTicks = channel.ObservableReceivedCharacters.AzimuthEncoderTicks();
            var azimuthEncoderSubscription = azimuthEncoderTicks
                //.ObserveOn(Scheduler.Default)
                .Subscribe(
                    stateMachine.AzimuthEncoderTickReceived,
                    ex => throw new InvalidOperationException(
                        "Encoder tick sequence produced an unexpected error (see ineer exception)", ex),
                    () => throw new InvalidOperationException(
                        "Encoder tick sequence completed unexpectedly, this is probably a bug")
                );
            disposableSubscriptions.Add(azimuthEncoderSubscription);
            }

        private void RotationDirectionOnNext(RotationDirection direction) { }

        public void Close()
            {
            UnsubscribeControllerEvents();
            transactionProcessor?.Dispose();
            transactionProcessor = null;
            channel.Close();
            }

        private void UnsubscribeControllerEvents()
            {
            disposableSubscriptions.ForEach(m => m.Dispose());
            disposableSubscriptions.Clear();
            }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }

        public void RequestEmergencyStop()
            {
            var pause = TimeSpan.FromSeconds(1);
            stateMachine.AllStop();
            Task.Delay(pause).Wait();
            stateMachine.AllStop();
            Task.Delay(pause).Wait();
            stateMachine.AllStop();
            }

        public void SlewToAzimuth(double azimuth)
            {
            stateMachine.RotateToAzimuthDegrees(azimuth);
            }
        }
    }