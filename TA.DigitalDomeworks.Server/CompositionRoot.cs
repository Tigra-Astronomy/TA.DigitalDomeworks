// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: CompositionRoot.cs  Last modified: 2018-03-20@13:00 by Tim Long

using System.Threading;
using Ninject;
using Ninject.Activation;
using Ninject.Modules;
using Ninject.Syntax;
using NLog.Fluent;
using NodaTime;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.DeviceInterface.StateMachine;
using TA.DigitalDomeworks.Server.Properties;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.Server
    {
    public static class CompositionRoot
        {
        private static ScopeObject CurrentScope { get; set; }

        public static void BeginSessionScope()
            {
            var scope = new ScopeObject();
            Log.Info()
                .Message($"Beginning session scope id={scope.ScopeId}")
                .Write();
            CurrentScope = scope;
            }

        static CompositionRoot()
            {
            Kernel = new StandardKernel(new CoreModule());
            }

        public static IKernel Kernel { get; }

        public static IBindingNamedWithOrOnSyntax<T> InSessionScope<T>(this IBindingInSyntax<T> binding)
            {
            return binding.InScope(ctx => CompositionRoot.CurrentScope);
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
                .ToMethod(ctx => Kernel.Get<ChannelFactory>().FromConnectionString(Properties.Settings.Default.ConnectionString))
                .InSessionScope();
            Bind<ChannelFactory>().ToSelf().InSessionScope();
            Bind<IClock>().ToMethod(ctx => SystemClock.Instance);
            Bind<IControllerActions>().To<RxControllerActions>().InSessionScope();
            Bind<DeviceControllerOptions>().ToMethod(BuildDeviceOptions).InSessionScope();
            }

        private DeviceControllerOptions BuildDeviceOptions(IContext arg)
            {
            var options = new DeviceControllerOptions
                {
                PerformShutterRecovery = Settings.Default.PerformShutterRecovery,
                MaximumShutterCloseTime = Settings.Default.ShutterCloseTimeout,
                MaximumFullRotationTime = Settings.Default.FullRotationTimeout
                };
            return options;
            }
        }
    }