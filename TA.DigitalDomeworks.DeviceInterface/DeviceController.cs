﻿// This file is part of the TA.DigitalDomeworks project
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
            var azimuthEncoderTicks = channel.ObservableReceivedCharacters.AzimuthEncoderTicks();
            azimuthEncoderSubscription = azimuthEncoderTicks.Subscribe(
                AzimuthEncoderOnNext,
                ex => throw new InvalidOperationException("Encoder tick sequence produced an unexpected error (see ineer exception)", ex),
                ()=>throw new InvalidOperationException("Encoder tick sequence completed unexpectedly, this is probably a bug")
            );
            var rotationDirectionSequence = from c in channel.ObservableReceivedCharacters
                                            where c == 'L' || c == 'R'
                                            let direction = (c == 'L')
                                                ? RotationDirection.CounterClockwise
                                                : RotationDirection.Clockwise
                                            select direction;
            rotationDirectionSubscription = rotationDirectionSequence.Trace("Direction")
                .Subscribe(
                    RotationDirectionOnNext,
                    ex => throw new InvalidOperationException(
                        "Direction sequence produced an unexpected error (see ineer exception)", ex),
                    () => throw new InvalidOperationException(
                        "Direction sequence completed unexpectedly, this is probably a bug")
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
            Direction = direction;
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

        public RotationDirection Direction { get; set; }

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