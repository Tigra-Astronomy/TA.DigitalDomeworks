// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: SimulatorCommunicationsChannel.cs  Last modified: 2018-03-28@22:45 by Tim Long

using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    /// <summary>
    ///     Provides a direct in-memory communications link to the hardware simulator.
    /// </summary>
    public class SimulatorCommunicationsChannel : ICommunicationChannel
        {
        private readonly SimulatorStateMachine simulator;

        /// <summary>
        ///     Creates a simulator communications channel from a valid endpoint.
        /// </summary>
        /// <param name="endpoint">A valid simulator endpoint.</param>
        public SimulatorCommunicationsChannel(SimulatorEndpoint endpoint)
            {
            Contract.Requires(endpoint != null);
            Endpoint = endpoint;
            simulator = new SimulatorStateMachine(endpoint.Realtime, new SystemDateTimeUtcClock());
            }

        /// <summary>
        ///     Keeps a log of all commands sent to the simulator.
        /// </summary>
        public List<string> SendLog { get; } = new List<string>();

        /// <inheritdoc />
        public void Dispose()
            {
            simulator?.InputObserver.OnCompleted();
            }

        /// <inheritdoc />
        public void Open()
            {
            IsOpen = true;
            }

        /// <inheritdoc />
        public void Close()
            {
            IsOpen = false;
            }

        /// <inheritdoc />
        public void Send(string txData)
            {
            SendLog.Add(txData);
            foreach (var c in txData) simulator.InputObserver.OnNext(c);
            }

        /// <inheritdoc />
        public IObservable<char> ObservableReceivedCharacters => simulator.ObservableResponses;

        /// <inheritdoc />
        public bool IsOpen { get; private set; }

        /// <inheritdoc />
        public DeviceEndpoint Endpoint { get; }
        }
    }