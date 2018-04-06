// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: DeviceControllerContextBuilder.cs  Last modified: 2018-04-06@02:24 by Tim Long

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.DeviceInterface.StateMachine;
using TA.DigitalDomeworks.HardwareSimulator;
using TA.DigitalDomeworks.SharedTypes;
using TA.DigitalDomeworks.Specifications.Contexts;
using TA.DigitalDomeworks.Specifications.Fakes;

namespace TA.DigitalDomeworks.Specifications.Builders
    {
    internal class DeviceControllerContextBuilder
        {
        public DeviceControllerContextBuilder()
            {
            channelFactory = new ChannelFactory();
            channelFactory.RegisterChannelType(
                p => p.StartsWith("Fake", StringComparison.InvariantCultureIgnoreCase),
                connection => new FakeEndpoint(),
                endpoint => new FakeCommunicationChannel(fakeResponseBuilder.ToString())
            );
            channelFactory.RegisterChannelType(
                SimulatorEndpoint.IsConnectionStringValid,
                SimulatorEndpoint.FromConnectionString,
                endpoint => new SimulatorCommunicationsChannel(endpoint as SimulatorEndpoint)
            );
            }

        bool channelShouldBeOpen;
        readonly StringBuilder fakeResponseBuilder = new StringBuilder();
        readonly IClock timeSource = new FakeClock(DateTime.MinValue.ToUniversalTime());
        readonly ChannelFactory channelFactory;
        string connectionString = "Fake";
        readonly DeviceControllerOptions controllerOptions = new DeviceControllerOptions
            {
            KeepAliveTimerInterval = TimeSpan.FromMinutes(3),
            MaximumFullRotationTime = TimeSpan.FromMinutes(1),
            MaximumShutterCloseTime = TimeSpan.FromMinutes(1),
            PerformShutterRecovery = true
            };
        PropertyChangedEventHandler propertyChangedAction;
        List<Tuple<string, Action>> propertyChangeObservers = new List<Tuple<string, Action>>();

        public DeviceControllerContext Build()
            {
            // Build the communications channel
            var channel = channelFactory.FromConnectionString(connectionString);
            if (channelShouldBeOpen)
                channel.Open();

            // Build the ControllerStatusFactory
            var statusFactory = new ControllerStatusFactory(timeSource);

            var controllerActions = new RxControllerActions(channel);
            var controllerStateMachine = new ControllerStateMachine(controllerActions, controllerOptions);

            // Build the device controller
            var controller = new DeviceController(channel, statusFactory, controllerStateMachine, controllerOptions);

            // Assemble the device controller test context
            var context = new DeviceControllerContext
                {
                Channel = channel,
                Controller = controller,
                StateMachine = controllerStateMachine,
                Actions = controllerActions
                };

            // Wire up any Property Changed notifications
            if (propertyChangedAction != null) controller.PropertyChanged += propertyChangedAction;

            return context;
            }

        public DeviceControllerContextBuilder WithOpenConnection(string connection)
            {
            connectionString = connection;
            channelShouldBeOpen = true;
            return this;
            }

        public DeviceControllerContextBuilder WithFakeResponse(string fakeResponse)
            {
            fakeResponseBuilder.Append(fakeResponse);
            return this;
            }

        public DeviceControllerContextBuilder WithClosedConnection(string connection)
            {
            connectionString = connection;
            channelShouldBeOpen = false;
            return this;
            }

        public DeviceControllerContextBuilder OnPropertyChanged(PropertyChangedEventHandler action)
            {
            propertyChangedAction = action;
            return this;
            }
        }
    }