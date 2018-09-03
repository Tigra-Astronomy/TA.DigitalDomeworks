// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: HardwareSimulationBuilder.cs  Last modified: 2018-03-28@18:17 by Tim Long

using System;
using System.Reactive.Linq;
using TA.DigitalDomeworks.HardwareSimulator;
using TA.DigitalDomeworks.Specifications.Fakes;

namespace TA.DigitalDomeworks.Specifications.Builders
    {
    internal class HardwareSimulationBuilder
        {
        IObservable<char> inputSequence = Observable.Empty<char>();

        public SimulatorStateMachine Build()
            {
            var machine = new SimulatorStateMachine(realTime: false, timeSource: new FakeClock(DateTime.UtcNow));
            return machine;
            }
        }
    }