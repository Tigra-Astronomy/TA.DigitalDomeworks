// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: DeviceController.cs  Last modified: 2018-03-12@17:03 by Tim Long

using System;
using System.ComponentModel;
using System.Globalization;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using NLog.Fluent;
using PostSharp.Patterns.Model;
using TA.Ascom.ReactiveCommunications;
using TA.Ascom.ReactiveCommunications.Diagnostics;
using TA.DigitalDomeworks.HardwareSimulator;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface
    {
    [NotifyPropertyChanged]
    internal class DeviceController : INotifyPropertyChanged
        {
        [NotNull] private readonly ICommunicationChannel channel;
        [NotNull] private readonly ControllerStatusFactory statusFactory;
        [NotNull] private IControllerStatus currentStatus;
        [CanBeNull] private ReactiveTransactionProcessor transactionProcessor;
        [CanBeNull] private IDisposable azimuthEncoderSubscription;
        [CanBeNull] private IDisposable rotationDirectionSubscription;
        [CanBeNull] private IDisposable shutterCurrentSubscription;
        [CanBeNull] private IDisposable shutterDirectionSubscription;
        [CanBeNull] private IDisposable statusUpdateSubscription;

        public DeviceController(ICommunicationChannel channel, ControllerStatusFactory factory)
            {
            this.channel = channel;
            statusFactory = factory;
            }

        [NotNull]
        public IControllerStatus CurrentStatus
            {
            get => currentStatus;
            private set
                {
                currentStatus = value;
                AzimuthEncoderSteps = currentStatus.CurrentAzimuth;
                }
            }

        public bool IsOnline => channel.IsOpen;

        public int AzimuthEncoderSteps { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void Open(bool performOnConnectActions = true)
            {
            var observer = new TransactionObserver(channel);
            transactionProcessor = new ReactiveTransactionProcessor();
            transactionProcessor.SubscribeTransactionObserver(observer);
            channel.Open();
            if (performOnConnectActions)
                PerformTasksOnConnect();
            SubscribeControllerEvents();
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
                SetStatus(status);

                }
            catch (Exception ex)
                {
                Log.Error()
                    .Exception(ex)
                    .Message($"Error while processing status notification: {statusNotification}")
                    .Write();
                }
            }

        private void SetStatus(IControllerStatus status)
            {
            CurrentStatus = status;
            AzimuthEncoderSteps = status.CurrentAzimuth;
            DomeRotationInProgress = false;
            RotationDirection = RotationDirection.None;
            ShutterCurrent = 0;
            ShutterDirection = ShutterDirection.None;
            ShutterMovementInProgress = false;
            }

        private void SubscribeShutterDirection()
            {
            var shutterDirectionSequence = from c in channel.ObservableReceivedCharacters
                                            where c == 'C' || c == 'O'
                                            let direction = (c == 'C')
                                                ? ShutterDirection.Closing
                                                : ShutterDirection.Opening
                                            select direction;
            shutterDirectionSubscription = shutterDirectionSequence.Trace("ShutterDirection")
                .Subscribe(
                    ShutterDirectionOnNext,
                    ex => throw new InvalidOperationException(
                        "Shutter Direction sequence produced an unexpected error (see ineer exception)", ex),
                    () => throw new InvalidOperationException(
                        "Shutter Direction sequence completed unexpectedly, this is probably a bug")
                );

        }

        private void ShutterDirectionOnNext(ShutterDirection direction)
            {
            ShutterDirection = direction;
            ShutterMovementInProgress = true;
            DomeRotationInProgress = false;
            RotationDirection = RotationDirection.None;
            }

        private void SubscribeShutterCurrentReadings()
            {
            var shutterCurrentReadings = channel.ObservableReceivedCharacters.ShutterCurrentReadings();
            shutterCurrentSubscription = shutterCurrentReadings.Subscribe(
                ShutterCurrentOnNext,
                ex => throw new InvalidOperationException(
                    "Shutter Current sequence produced an unexpected error (see ineer exception)", ex),
                () => throw new InvalidOperationException(
                    "ShutterCurrent sequence completed unexpectedly, this is probably a bug")
            );
            }

        private void ShutterCurrentOnNext(int shutterCurrent)
            {
            ShutterCurrent = shutterCurrent;
            DomeRotationInProgress = false;
            ShutterMovementInProgress = true;
            }

        private void SubscribeRotationDirection()
            {
            var rotationDirectionSequence = from c in channel.ObservableReceivedCharacters
                                            where c == 'L' || c == 'R'
                                            let direction = (c == 'L')
                                                ? RotationDirection.CounterClockwise
                                                : RotationDirection.Clockwise
                                            select direction;
            rotationDirectionSubscription = rotationDirectionSequence.Trace("RotationDirection")
                .Subscribe(
                    RotationDirectionOnNext,
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
                AzimuthEncoderOnNext,
                ex => throw new InvalidOperationException(
                    "Encoder tick sequence produced an unexpected error (see ineer exception)", ex),
                () => throw new InvalidOperationException(
                    "Encoder tick sequence completed unexpectedly, this is probably a bug")
            );
            }

        private void AzimuthEncoderOnNext(int azimuth)
            {
            AzimuthEncoderSteps = azimuth;
            DomeRotationInProgress = true;
            ShutterMovementInProgress = false;
        }

        private void RotationDirectionOnNext(RotationDirection direction)
            {
            RotationDirection = direction;
            DomeRotationInProgress = true;
            ShutterMovementInProgress = false;
            }

        /// <summary>
        /// <c>true</c> if the azimuth motors are active
        /// </summary>
        public bool DomeRotationInProgress { get; private set; }

        /// <summary>
        /// <c>true</c> if any part of the building is moving.
        /// </summary>
        public bool IsMoving => DomeRotationInProgress | ShutterMovementInProgress;

        /// <summary>
        /// <c>true</c> if the shutter motor is active.
        /// </summary>
        public bool ShutterMovementInProgress { get; private set; }

        public RotationDirection RotationDirection { get; set; }

        public int ShutterCurrent { get; private set; }

        public ShutterDirection ShutterDirection { get; private set; }

        private void PerformTasksOnConnect()
            {
            var transaction = new StatusTransaction(statusFactory);
            transactionProcessor.CommitTransaction(transaction);
            transaction.WaitForCompletionOrTimeout(); // Synchronous
            transaction.ThrowIfFailed();
            CurrentStatus = transaction.ControllerStatus;
            }

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

        public async Task<IControllerStatus> GetStatus()
            {
            var getStatusTransaction = new StatusTransaction(statusFactory);
            transactionProcessor.CommitTransaction(getStatusTransaction);
            await getStatusTransaction.WaitForCompletionOrTimeoutAsync(CancellationToken.None);
            getStatusTransaction.ThrowIfFailed();
            return getStatusTransaction.ControllerStatus;
            }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }