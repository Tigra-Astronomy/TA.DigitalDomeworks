// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2016 TiGra Astronomy, all rights reserved.
// 
// File: StateEmergencyStop.cs  Created: 2016-06-20@18:14
// Last modified: 2016-06-21@09:59 by Tim

using System.Threading;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    /// <summary>
    ///     The emergency stop state doesn't really provide any functionality but it does reflect a genuine
    ///     hardware state and is useful as a place to put logging. The state sleeps for one second
    ///     before transitioning to <see cref="StateSendStatus" />.
    /// </summary>
    internal class StateEmergencyStop : SimulatorState
        {
        internal StateEmergencyStop(SimulatorStateMachine machine) : base(machine) { }

        /// <summary>
        ///     Gets the descriptive name of the current state.
        /// </summary>
        /// <value>The state's descriptive name, as a string.</value>
        protected override string Name => "Emergency Stop";

        /// <summary>
        ///     Called (by the state machine) when entering the state.
        ///     Sleeps for 1 second and transitions to the <see cref="StateSendStatus" /> state.
        /// </summary>
        public override void OnEnter()
            {
            base.OnEnter();
            machine.InvokeMotorConfigurationChanged(MotorConfigurationEventArgs.AllStopped);
            Thread.Sleep(1000);
            Transition(new StateSendStatus(machine));
            }

        /// <summary>
        ///     Provides a stimulus (input) to the state.
        ///     Any stimulus during an emergency stop is discarded.
        /// </summary>
        /// <param name="value">The input value or stimulus.</param>
        public override void Stimulus(char value)
            {
            base.Stimulus(value);
            Log.Info($"Discarding input: '{value.ExpandAscii()}'");
            }
        }
    }