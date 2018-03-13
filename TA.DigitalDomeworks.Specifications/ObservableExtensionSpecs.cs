// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ObservableExtensionSpecs.cs  Last modified: 2018-03-12@19:34 by Tim Long


using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Threading;
using Machine.Specifications;
using NLog;
using TA.Ascom.ReactiveCommunications.Diagnostics;
using TA.DigitalDomeworks.DeviceInterface;
using ObservableExtensions = TA.DigitalDomeworks.DeviceInterface.ObservableExtensions;

namespace TA.DigitalDomeworks.Specifications
    {
    [Subject(typeof(ObservableExtensions), "very")]
    internal class when_an_encoder_tick_is_received
        {
        Establish context = () => source = "P99\nP100\nP101\n".ToObservable();
        Because of = () => source.AzimuthEncoderTicks().Subscribe(tick => tickHistory.Add(tick));
        It should_receive_the_encoder_ticks = () => tickHistory.ShouldEqual(expectedTicks);
        static IObservable<char> source;
        static int lastTick;
        static List<int> tickHistory = new List<int>();
        static List<int> expectedTicks = new List<int> {99, 100, 101};
        }
    }