using System;
using System.Collections.Generic;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.HardwareSimulator;

namespace TA.DigitalDomeworks.Specifications.Helpers
    {
    internal class SimulatorCommunicationsChannel : ICommunicationChannel
        {
        readonly SimulatorStateMachine simulator;

        public SimulatorCommunicationsChannel(SimulatorEndpoint endpoint)
            {
            Endpoint = endpoint;
            simulator = new SimulatorStateMachine(endpoint.Realtime);
            }

        public void Dispose()
            {
            simulator?.InputObserver.OnCompleted();
            }

        public void Open()
            {
            IsOpen = true;
            }

        public void Close()
            {
            IsOpen = false;
            }

        public void Send(string txData)
            {
            SendLog.Add(txData);
            foreach (var c in txData) simulator.InputObserver.OnNext(c);
            }

        public List<string> SendLog { get; } = new List<string>();

        public IObservable<char> ObservableReceivedCharacters => simulator.ObservableResponses;

        public bool IsOpen { get; private set; }

        public DeviceEndpoint Endpoint { get; }
        }
    }