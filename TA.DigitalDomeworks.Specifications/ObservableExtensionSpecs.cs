// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ObservableExtensionSpecs.cs  Last modified: 2018-08-30@01:43 by Tim Long

using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using Machine.Specifications;
using TA.Ascom.ReactiveCommunications.Diagnostics;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.Specifications.Helpers;
using ObservableExtensions = TA.DigitalDomeworks.DeviceInterface.ObservableExtensions;

namespace TA.DigitalDomeworks.Specifications
    {
    [Subject(typeof(ObservableExtensions), "Encoder Ticks")]
    internal class when_an_encoder_tick_is_received
        {
        Establish context = () => source = "P99\nP100\nP101\n".ToObservable();
        Because of = () => source.AzimuthEncoderTicks().SubscribeAndWaitForCompletion(tick => tickHistory.Add(tick));
        It should_receive_the_encoder_ticks = () => tickHistory.ShouldEqual(expectedTicks);
        static List<int> expectedTicks = new List<int> {99, 100, 101};
        static IObservable<char> source;
        static List<int> tickHistory = new List<int>();
        }

    [Subject(typeof(ObservableExtensions), "Shutter Current Readings")]
    internal class when_a_shutter_current_reading_is_received
        {
        Establish context = () => source = "Z8\nZ10\nZ11\n".ToObservable();
        Because of = () => source
            .ShutterCurrentReadings().Trace("Unbelievable")
            .SubscribeAndWaitForCompletion(item => elementHistory.Add(item));
        It should_receive_the_current_readings = () => elementHistory.ShouldEqual(expectedElements);
        static List<int> elementHistory = new List<int>();
        static List<int> expectedElements = new List<int> {8, 10, 11};
        static IObservable<char> source;
        }
    }