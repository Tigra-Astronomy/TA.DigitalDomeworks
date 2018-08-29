// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: SimulatorEndpoint.cs  Last modified: 2018-03-28@15:51 by Tim Long

using System;
using System.Diagnostics.Contracts;
using System.Text.RegularExpressions;
using NLog.Fluent;
using TA.Ascom.ReactiveCommunications;

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    /// <summary>
    ///     Endpoint representing the hardware simulator.
    ///     Connection string format: Simulator:Realtime or Simulator:Fast
    /// </summary>
    public class SimulatorEndpoint : DeviceEndpoint
        {
        private const string realtime = "Realtime";
        private const string fast = "Fast";
        private const string connectionPattern = @"^Simulator(:(?<Speed>(Realtime)|(Fast)))?$";
        private static readonly Regex connectionRegex = new Regex(connectionPattern, Options);
        private string connectionString;

        private SimulatorEndpoint(string connectionString)
            {
            this.connectionString = connectionString;
            }

        /// <summary>
        ///     When <c>true</c>, delays are added to simulate realtime operation.
        ///     When <c>false</c>, all operations complete nearly instantly. This is
        ///     useful for unit testing.
        /// </summary>
        public bool Realtime { get; set; }

        /// <summary>
        ///     Creates a simulator endpoint from a valid simulator connection string.
        /// </summary>
        /// <param name="connection">A valid simulator connection string.</param>
        /// <returns>Creates and returns an endpoint object for the connection string.</returns>
        /// <exception cref="ArgumentException">
        ///     Thrown if the connection string is empty or invalid.
        /// </exception>
        public static SimulatorEndpoint FromConnectionString(string connection)
            {
            Contract.Requires(!string.IsNullOrWhiteSpace(connection));
            Contract.Ensures(Contract.Result<SimulatorEndpoint>() != null);
            var matches = connectionRegex.Match(connection);
            if (!matches.Success)
                throw new ArgumentException($"The connection string '{connection}' is not valid for the simulator",
                    nameof(connection));
            var speed = CaptureGroupOrDefault(matches, "Speed", "Fast");
            var timing = speed.Equals(realtime, StringComparison.InvariantCultureIgnoreCase);
            return new SimulatorEndpoint(connection) {Realtime = timing};
            }

        /// <summary>
        /// Tests whether a connection string is valid for the simulator.
        /// </summary>
        /// <param name="connection"></param>
        /// <returns>
        /// <c>true</c> if the connection string is valid; <c>false</c> otherwise.
        /// </returns>
        [Pure]
        public static bool IsConnectionStringValid(string connection)
            {
            Contract.Requires(connection != null);
            try
                {
                return connectionRegex.IsMatch(connection);
                }
            catch (RegexMatchTimeoutException ex)
                {
                Log.Error()
                    .Message("Regex match timeout when validating simulator connection string {connection}")
                    .Exception(ex)
                    .Property("connection", connection)
                    .Property(nameof(connectionRegex), connectionRegex.ToString())
                    .Write();
                return false;
                }
            }

        /// <summary>
        /// Gets the connection string for this endpoint.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
            {
            Contract.Ensures(IsConnectionStringValid(Contract.Result<string>()));
            return $"Simulator:{(Realtime ? realtime : fast)}";
            }
        }
    }