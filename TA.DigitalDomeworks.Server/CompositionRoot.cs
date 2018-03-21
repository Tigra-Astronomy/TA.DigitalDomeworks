// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: CompositionRoot.cs  Last modified: 2018-03-20@13:00 by Tim Long

using Ninject;
using Ninject.Modules;
using NodaTime;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.DeviceInterface.StateMachine;

namespace TA.DigitalDomeworks.Server
    {
    public static class CompositionRoot
        {
        static CompositionRoot()
            {
            Kernel = new StandardKernel(new CoreModule());
            }

        public static IKernel Kernel { get; }
        }

    internal class CoreModule : NinjectModule
        {
        public override void Load()
            {
            Bind<DeviceController>().ToSelf().InSingletonScope();
            Bind<ICommunicationChannel>()
                .ToMethod(ctx => Kernel.Get<ChannelFactory>().FromConnectionString(Properties.Settings.Default.ConnectionString))
                .InSingletonScope();
            Bind<ChannelFactory>().ToSelf().InSingletonScope();
            Bind<IClock>().ToMethod(ctx => SystemClock.Instance);
            Bind<IControllerActions>().To<RxControllerActions>().InSingletonScope();
            }
        }
    }