// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: DeviceControllerContextBuilder.cs  Last modified: 2018-03-08@19:17 by Tim Long

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Text;
using NodaTime;
using NodaTime.Testing;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.SharedTypes;
using TA.DigitalDomeworks.Specifications.Contexts;
using TA.DigitalDomeworks.Specifications.Fakes;
using TA.DigitalDomeworks.Specifications.Helpers;

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
        readonly IClock timeSource = new FakeClock(Instant.MinValue);
        readonly ChannelFactory channelFactory;
        string connectionString = "Fake";
        PropertyChangedEventHandler propertyChangedAction;
        List<Tuple<string,Action>> propertyChangeObservers = new List<Tuple<string, Action>>();

        public DeviceControllerContext Build()
            {
            // Build the communications channel
            var channel = channelFactory.FromConnectionString(connectionString);
            if (channelShouldBeOpen)
                channel.Open();

            // Build the ControllerStatusFactory
            var statusFactory = new ControllerStatusFactory(timeSource);

            // Build the device controller
            var controller = new DeviceController(channel, statusFactory);

            // Assemble the device controller test context
            var context = new DeviceControllerContext
                {
                Channel = channel,
                Controller = controller
                };

            // Wire up any Property Changed notifications
            if (propertyChangedAction != null)
                {
                controller.PropertyChanged += propertyChangedAction;
                }

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