// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: CompositionRoot.cs  Last modified: 2018-03-28@22:42 by Tim Long

using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Syntax;
using NLog.Fluent;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.DeviceInterface.StateMachine;
using TA.DigitalDomeworks.HardwareSimulator;
using TA.DigitalDomeworks.Server.Properties;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.Server
    {
    public static class CompositionRoot
        {
        static CompositionRoot()
            {
            Kernel = new StandardKernel(new CoreModule());
            }

        private static ScopeObject CurrentScope { get; set; }

        public static IKernel Kernel { get; }

        public static void BeginSessionScope()
            {
            var scope = new ScopeObject();
            Log.Info()
                .Message($"Beginning session scope id={scope.ScopeId}")
                .Write();
            CurrentScope = scope;
            }

        public static IBindingNamedWithOrOnSyntax<T> InSessionScope<T>(this IBindingInSyntax<T> binding)
            {
            return binding.InScope(ctx => CurrentScope);
            }
        }

    internal class ScopeObject
        {
        private static int scopeId;

        public ScopeObject()
            {
            ++scopeId;
            }

        public int ScopeId => scopeId;
        }

    internal class CoreModule : NinjectModule
        {
        public override void Load()
            {
            Bind<DeviceController>().ToSelf().InSessionScope();
            Bind<ICommunicationChannel>()
                .ToMethod(BuildCommunicationsChannel)
                .InSessionScope();
            Bind<ChannelFactory>().ToMethod(BuildChannelFactory).InSingletonScope();
            Bind<IClock>().To<SystemDateTimeUtcClock>().InSingletonScope();
            Bind<IControllerActions>().To<RxControllerActions>().InSessionScope();
            Bind<DeviceControllerOptions>().ToMethod(BuildDeviceOptions).InSessionScope();
            }

        private ICommunicationChannel BuildCommunicationsChannel(IContext context)
            {
            var channelFactory = Kernel.Get<ChannelFactory>();
            var channel = channelFactory.FromConnectionString(Settings.Default.ConnectionString);
            return channel;
            }

        private ChannelFactory BuildChannelFactory(IContext arg)
            {
            var factory = new ChannelFactory();
            factory.RegisterChannelType(
                SimulatorEndpoint.IsConnectionStringValid,
                SimulatorEndpoint.FromConnectionString,
                endpoint => new SimulatorCommunicationsChannel((SimulatorEndpoint) endpoint)
            );
            return factory;
            }

        private DeviceControllerOptions BuildDeviceOptions(IContext arg)
            {
            var options = new DeviceControllerOptions
                {
                PerformShutterRecovery = Settings.Default.PerformShutterRecovery,
                MaximumShutterCloseTime = Settings.Default.ShutterCloseTimeout,
                MaximumFullRotationTime = Settings.Default.FullRotationTimeout,
                KeepAliveTimerInterval = Settings.Default.KeepAliveTimerPeriod
                };
            return options;
            }
        }
    }