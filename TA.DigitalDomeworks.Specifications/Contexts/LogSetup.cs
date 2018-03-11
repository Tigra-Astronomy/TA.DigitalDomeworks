// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: LogSetup.cs  Last modified: 2018-03-11@21:11 by Tim Long

using JetBrains.Annotations;
using Machine.Specifications;
using NLog;
using NLog.Config;
using NLog.Targets;

namespace TA.DigitalDomeworks.Specifications.Contexts
    {
    [UsedImplicitly]
    public class LogSetup : IAssemblyContext
        {
        static Logger log;

        public void OnAssemblyStart()
            {
            var configuration = new LoggingConfiguration();
            var unitTestRunnerTarget = new TraceTarget();
            configuration.AddTarget("Unit test runner", unitTestRunnerTarget);
            var logEverything = new LoggingRule("*", LogLevel.Debug, unitTestRunnerTarget);
            configuration.LoggingRules.Add(logEverything);
            LogManager.Configuration = configuration;
            log = LogManager.GetCurrentClassLogger();
            log.Info("Logging initialized");
            }

        public void OnAssemblyComplete() { }
        }
    }