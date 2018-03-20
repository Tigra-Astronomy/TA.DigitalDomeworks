// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: DeviceController.cs  Last modified: 2018-03-20@01:30 by Tim Long

using System;
using System.ComponentModel;
using System.Linq;
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
    internal class DeviceController : INotifyPropertyChanged
        {
        [NotNull] private readonly ICommunicationChannel channel;
        [NotNull] private readonly ControllerStateMachine stateMachine;
        [NotNull] private readonly ControllerStatusFactory statusFactory;
        [CanBeNull] private IDisposable azimuthEncoderSubscription;
        [NotNull] private IHardwareStatus currentStatus;
        [CanBeNull] private IDisposable rotationDirectionSubscription;
        [CanBeNull] private IDisposable shutterCurrentSubscription;
        [CanBeNull] private IDisposable shutterDirectionSubscription;
        [CanBeNull] private IDisposable statusUpdateSubscription;
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
            var statusUpdates = channel.ObservableReceivedCharacters.DelimitedMessageStrings('V', '\n');
            statusUpdateSubscription = statusUpdates
                .Trace("StatusUpdate")
                .Subscribe(StatusUpdateOnNext,
                    ex => throw new InvalidOperationException(
                        "Status Update sequence produced an unexpected error (see ineer exception)", ex),
                    () => throw new InvalidOperationException(
                        "Status Update sequence completed unexpectedly, this is probably a bug")
                );
            }

        private void StatusUpdateOnNext(string statusNotification)
            {
            try
                {
                var status = statusFactory.FromStatusPacket(statusNotification);
                stateMachine.HardwareStatusReceived(status);
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
                                           where Enumerable.Contains(shutterMovementIndicators, c)
                                           let ordinal = shutterMovementIndicators.IndexOf(c)
                                           let direction = (ShutterDirection) ordinal
                                           select direction;
            shutterDirectionSubscription = ObservableDiagnosticExtensions
                .Trace<ShutterDirection>(shutterDirectionSequence, "ShutterDirection")
                .Subscribe(
                    stateMachine.ShutterDirectionReceived,
                    ex => throw new InvalidOperationException(
                        "Shutter Direction sequence produced an unexpected error (see ineer exception)", ex),
                    () => throw new InvalidOperationException(
                        "Shutter Direction sequence completed unexpectedly, this is probably a bug")
                );
            }

        private void SubscribeShutterCurrentReadings()
            {
            var shutterCurrentReadings = channel.ObservableReceivedCharacters.ShutterCurrentReadings();
            shutterCurrentSubscription = shutterCurrentReadings.Subscribe(
                stateMachine.ShutterMotorCurrentReceived,
                ex => throw new InvalidOperationException(
                    "Shutter Current sequence produced an unexpected error (see ineer exception)", ex),
                () => throw new InvalidOperationException(
                    "ShutterCurrent sequence completed unexpectedly, this is probably a bug")
            );
            }

        private void SubscribeRotationDirection()
            {
            var rotationDirectionSequence = from c in channel.ObservableReceivedCharacters
                                            where c == 'L' || c == 'R'
                                            let direction = c == 'L'
                                                ? RotationDirection.CounterClockwise
                                                : RotationDirection.Clockwise
                                            select direction;
            rotationDirectionSubscription = ObservableDiagnosticExtensions
                .Trace<RotationDirection>(rotationDirectionSequence, "RotationDirection")
                .Subscribe(
                    stateMachine.RotationDirectionReceived,
                    ex => throw new InvalidOperationException(
                        "RotationDirection sequence produced an unexpected error (see ineer exception)", ex),
                    () => throw new InvalidOperationException(
                        "RotationDirection sequence completed unexpectedly, this is probably a bug")
                );
            }

        private void SubscribeAzimuthEncoderTicks()
            {
            var azimuthEncoderTicks = channel.ObservableReceivedCharacters.AzimuthEncoderTicks();
            azimuthEncoderSubscription = azimuthEncoderTicks.Subscribe(
                stateMachine.AzimuthEncoderTickReceived,
                ex => throw new InvalidOperationException(
                    "Encoder tick sequence produced an unexpected error (see ineer exception)", ex),
                () => throw new InvalidOperationException(
                    "Encoder tick sequence completed unexpectedly, this is probably a bug")
            );
            }

        private void RotationDirectionOnNext(RotationDirection direction) { }

        public void Close()
            {
            UnsubscribeControllerEvents();
            transactionProcessor?.Dispose();
            transactionProcessor = null;
            }

        private void UnsubscribeControllerEvents()
            {
            azimuthEncoderSubscription?.Dispose();
            azimuthEncoderSubscription = null;
            rotationDirectionSubscription?.Dispose();
            rotationDirectionSubscription = null;
            shutterCurrentSubscription?.Dispose();
            shutterCurrentSubscription = null;
            shutterDirectionSubscription?.Dispose();
            shutterDirectionSubscription = null;
            statusUpdateSubscription?.Dispose();
            statusUpdateSubscription = null;
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
        }
    }