// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: SimulatorIceBreakerSpecs.cs  Last modified: 2018-09-03@14:54 by Tim Long

using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using Machine.Specifications;
using TA.DigitalDomeworks.HardwareSimulator;
using TA.DigitalDomeworks.Specifications.Builders;

namespace TA.DigitalDomeworks.Specifications.HardwareSimulatorSpecs
    {
    [Subject(typeof(SimulatorStateMachine), "I/O")]
    internal class when_sending_a_get_status_command_to_the_simulator
        {
        Establish context = () =>
            {
            simulator = new HardwareSimulationBuilder().Build();
            subscription = simulator.ObservableResponses
                .ObserveOn(ImmediateScheduler.Instance)
                .Subscribe(
                    rx => responses.Append(rx),
                    () => result = responses.ToString()
                );
            };
        Because of = () =>
            {
            var inputString = "GINF";
            foreach (var c in inputString) simulator.InputObserver.OnNext(c);
            simulator.InputObserver.OnCompleted();
            simulator.InReadyState.WaitOne();
            };

        It should_receive_a_status_response = () => result.Length.ShouldBeGreaterThan(0);
        Cleanup after = () => subscription.Dispose();
        static readonly StringBuilder responses = new StringBuilder();
        static string result;
        static SimulatorStateMachine simulator;
        static IDisposable subscription;
        }
    }