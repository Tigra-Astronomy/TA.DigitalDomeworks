// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: SimulatorEndpointSpecs.cs  Last modified: 2018-03-28@18:30 by Tim Long

using System;
using Machine.Specifications;
using TA.DigitalDomeworks.HardwareSimulator;

namespace TA.DigitalDomeworks.Specifications
    {
    [Subject(typeof(SimulatorEndpoint), "code contracts")]
    internal class when_violating_a_code_contract
        {
        Because of = () => Exception = Catch.Exception(() => SimulatorEndpoint.IsConnectionStringValid(null));
        It should_throw = () => Exception.ShouldNotBeNull();
        static Exception Exception;
        }

    [Subject(typeof(SimulatorEndpoint), "connection string")]
    internal class when_validating_a_valid_connection_string
        {
        It should_succeed_for_realtime =
            () => SimulatorEndpoint.IsConnectionStringValid("Simulator:Realtime").ShouldBeTrue();
        It should_succeed_for_fast = () => SimulatorEndpoint.IsConnectionStringValid("Simulator:Fast").ShouldBeTrue();
        It should_succeed_for_default = () => SimulatorEndpoint.IsConnectionStringValid("Simulator").ShouldBeTrue();
        }
    }