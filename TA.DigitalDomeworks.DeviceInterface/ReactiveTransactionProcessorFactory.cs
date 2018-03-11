﻿// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ReactiveTransactionProcessorFactory.cs  Last modified: 2018-03-02@00:31 by Tim Long

using System;
using System.Text;
using System.Threading;
using TA.Ascom.ReactiveCommunications;

namespace TA.DigitalDomeworks.DeviceInterface
    {
    /// <summary>
    ///     An implementation of <see cref="ITransactionProcessorFactory" /> that can create a 
    ///     transaction processor that uses the Reactive Extensions for .NET to process
    ///     transactions.
    /// </summary>
    public class ReactiveTransactionProcessorFactory : ITransactionProcessorFactory
        {
        private readonly string connectionString;
        private TransactionObserver observer;
        private ReactiveTransactionProcessor processor;

        /// <summary>
        ///     Initializes a new instance of <see cref="ReactiveTransactionProcessor" /> using the supplied connection string to
        ///     determine
        ///     the <see cref="DeviceEndpoint" /> (and indirectly the <see cref="ICommunicationChannel" /> to connect to.
        /// </summary>
        /// <param name="connectionString"></param>
        public ReactiveTransactionProcessorFactory(string connectionString)
            {
            this.connectionString = connectionString;
            }

        /// <inheritdoc />
        /// <summary>
        ///     Gets the <see cref="ICommunicationChannel" /> that will be used to send and receive data to the device.
        /// </summary>
        public ICommunicationChannel Channel { get; private set; }

        /// <inheritdoc />
        /// <summary>
        ///     Creates the transaction processor ready for use. Also creates and initialises the
        ///     device endpoint and the communications channel and opens the channel.
        /// </summary>
        /// <returns>ITransactionProcessor.</returns>
        public ITransactionProcessor CreateTransactionProcessor()
            {
            var factory = new ChannelFactory(); // ToDo - inject the factory in the constructor

            Channel = factory.FromConnectionString(connectionString);
            observer = new TransactionObserver(Channel);
            processor = new ReactiveTransactionProcessor();
            processor.SubscribeTransactionObserver(observer);
            Channel.Open();
            return processor;
            }

        /// <inheritdoc />
        /// <summary>
        ///     Destroys the transaction processor and its dependencies. Ensures that the <see cref="Channel" /> is closed. Once
        ///     this method has been called, the <see cref="Channel" /> and <see cref="Endpoint" /> properties will be null. A new
        ///     connection to the same endpoint can be created by calling <see cref="CreateTransactionProcessor" /> again.
        /// </summary>
        public void DestroyTransactionProcessor()
            {
            processor?.Dispose();
            processor = null; // [Sentinel]
            observer = null;
            if (Channel?.IsOpen ?? false)
                Channel.Close();
            Channel?.Dispose();
            Channel = null; // [Sentinel]
            GC.Collect(3, GCCollectionMode.Forced, blocking: true);
            }

        /// <inheritdoc />
        public DeviceEndpoint Endpoint { get; set; }
        }
    }