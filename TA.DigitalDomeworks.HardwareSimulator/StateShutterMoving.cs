// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: StateShutterMoving.cs  Last modified: 2018-04-06@01:49 by Tim Long

using System;
using System.Timers;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    internal class StateShutterMoving : SimulatorState
        {
        private readonly MotorConfiguration direction;
        private readonly Random motorCurrent = new Random();
        private readonly Timer shutter_tick_timer = new Timer {AutoReset = false};

        protected StateShutterMoving(SimulatorStateMachine machine, MotorConfiguration direction)
            : base(machine)
            {
            this.direction = direction;
            }

        protected int ShutterTicksRemaining { get; private set; }

        protected override string Name => "Shutter Moving";

        /// <summary>
        ///     Called (by the state machine) when entering the state.
        ///     Sets the Shutter Sensor to indeterminate during movement and configures
        ///     a timer to simulate shutter motion.
        /// </summary>
        public override void OnEnter()
            {
            base.OnEnter();
            ShutterTicksRemaining = Machine.HardwareStatus.ShutterSensor == SensorState.Indeterminate
                ? 20
                : 40;
            Machine.HardwareStatus.ShutterSensor = SensorState.Indeterminate;
            Machine.SimulatedShutterSensor = SensorState.Indeterminate;
            //SimulatorStateMachine.InvokeStatusChanged(new StatusChangedEventArgs(SimulatorStateMachine.HardwareStatus));
            Machine.InvokeMotorConfigurationChanged(new MotorConfigurationEventArgs
                {
                AzimuthMotor = MotorConfiguration.Stopped,
                ShutterMotor = direction
                });
            shutter_tick_timer.Interval = Machine.RealTime
                ? Properties.Settings.Default.RotationRateMsPerTick
                : 1;
            shutter_tick_timer.Elapsed += ShutterTickTimerElapsed;
            shutter_tick_timer.Start();
            }

        /// <summary>
        ///     Handles the Elapsed event of the shutter_tick_timer control.
        /// </summary>
        /// <param name="sender">The source of the event (not used).</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs" /> instance containing the event data (not used).</param>
        private void ShutterTickTimerElapsed(object sender, ElapsedEventArgs e)
            {
            shutter_tick_timer.Stop();
            Machine.WriteLine(string.Format("SZ{0:D3}", motorCurrent.Next(6, 25)));
            if (--ShutterTicksRemaining > 0)
                shutter_tick_timer.Start();
            else
                Transition(new StateSendStatus(Machine));
            }

        /// <summary>
        ///     Called (by the state machine) when exiting from the state.
        ///     Configures the new shutter state based on the previous state.
        /// </summary>
        public override void OnExit()
            {
            base.OnExit();
            shutter_tick_timer.Stop();
            }

        /// <summary>
        ///     Provides a stimulus (input) to the state.
        ///     Any stimulus during movement is interpreted as Emergency Stop.
        /// </summary>
        /// <param name="value">The input value or stimulus.</param>
        public override void Stimulus(char value)
            {
            base.Stimulus(value);
            Transition(new StateEmergencyStop(Machine));
            }
        }
    }