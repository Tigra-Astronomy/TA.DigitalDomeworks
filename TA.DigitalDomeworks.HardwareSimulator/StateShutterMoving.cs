// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: StateShutterMoving.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:56 by Tim

using System;
using System.Timers;
using TA.DigitalDomeworks.SharedTypes;

namespace TI.DigitalDomeWorks.Simulator
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
            ShutterTicksRemaining = machine.HardwareStatus.ShutterSensor == SensorState.Indeterminate
                ? 20
                : 40;
            machine.HardwareStatus.ShutterSensor = SensorState.Indeterminate;
            machine.SimulatedShutterSensor = SensorState.Indeterminate;
            //SimulatorStateMachine.InvokeStatusChanged(new StatusChangedEventArgs(SimulatorStateMachine.HardwareStatus));
            machine.InvokeMotorConfigurationChanged(new MotorConfigurationEventArgs
                {
                AzimuthMotor = MotorConfiguration.Stopped,
                ShutterMotor = direction
                });
            shutter_tick_timer.Interval = machine.RealTime
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
            machine.WriteLine(string.Format("Z{0:D3}", motorCurrent.Next(6, 12)));
            if (--ShutterTicksRemaining > 0)
                shutter_tick_timer.Start();
            else
                Transition(new StateSendStatus(machine));
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
            Transition(new StateEmergencyStop(machine));
            }
        }
    }