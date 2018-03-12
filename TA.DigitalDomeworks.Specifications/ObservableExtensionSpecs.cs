// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ObservableExtensionSpecs.cs  Last modified: 2018-03-12@19:34 by Tim Long


using System;
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
        Establish context = () => source = "P0099\nP0100\nP0101\n".ToObservable();
        Because of = () => source.AzimuthEncoderTicks().Subscribe(tick => lastTick = tick);
        It should_parse_the_azimuth_value = () => lastTick.ShouldEqual(101);
        static IObservable<char> source;
        static int lastTick;
        }
    }