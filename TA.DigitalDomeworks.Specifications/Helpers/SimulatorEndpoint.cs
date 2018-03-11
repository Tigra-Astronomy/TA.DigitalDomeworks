// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: SimulatorEndpoint.cs  Last modified: 2018-03-08@19:15 by Tim Long

using System;
using System.Text.RegularExpressions;
using TA.Ascom.ReactiveCommunications;

namespace TA.DigitalDomeworks.Specifications.Helpers
    {
    /// <summary>
    ///     Endpoint representing the hardware simulator.
    ///     Connection string format: Simulator:Realtime or Simulator:Fast
    /// </summary>
    internal class SimulatorEndpoint : DeviceEndpoint
        {
        const string realtime = "Realtime";
        const string fast = "Fast";
        string connectionString;
        const string connectionPattern = @"^Simulator(:(?<Speed>(Realtime)|(Fast)))?$";
        static readonly Regex connectionRegex = new Regex(connectionPattern, Options);

        public static SimulatorEndpoint FromConnectionString(string connection)
            {
            if (!IsConnectionStringValid(connection))
                throw new ArgumentException($"Invalid connection string: {connection}");
            var matches = connectionRegex.Match(connection);
            var speed = CaptureGroupOrDefault(matches, "Speed", "Fast");
            var timing = speed.Equals(realtime, StringComparison.InvariantCultureIgnoreCase);
            return new SimulatorEndpoint(connection) {Realtime = timing};
            }

        public static bool IsConnectionStringValid(string connection)
            {
            return connectionRegex.IsMatch(connection);
            }

        SimulatorEndpoint(string connectionString)
            {
            this.connectionString = connectionString;
            }

        public bool Realtime { get; set; }

        public override string ToString()
            {
            return $"Simulator:{(Realtime ? realtime : fast)}";
            }
        }
    }