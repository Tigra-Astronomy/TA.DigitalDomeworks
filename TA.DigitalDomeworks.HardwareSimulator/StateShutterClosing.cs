// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: StateShutterClosing.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:56 by Tim

using TA.DigitalDomeworks.SharedTypes;

namespace TI.DigitalDomeWorks.Simulator
    {
    /// <summary>
    ///     Variant of <see cref="StateShutterMoving" /> used when the shutter is closing.
    ///     The finishing state can be either indeterminate or closed.
    /// </summary>
    internal class StateShutterClosing : StateShutterMoving
        {
        public StateShutterClosing(SimulatorStateMachine machine) : base(machine, MotorConfiguration.Reverse) { }

        protected override string Name => "Shutter Closing";

        /// <summary>
        ///     Called (by the state machine) when entering the state.
        ///     If the shutter is already closed, transitions directly to <see cref="StateSendStatus" />. Otherwise,
        ///     sets the Shutter Sensor to indeterminate during movement and configures
        ///     a timer to simulate shutter motion.
        /// </summary>
        public override void OnEnter()
            {
            if (machine.SimulatedShutterSensor == SensorState.Closed || machine.ShutterStuck)
                Transition(new StateSendStatus(machine));
            else
                base.OnEnter();
            }

        public override void OnExit()
            {
            base.OnExit();
            if (!machine.ShutterStuck)
                machine.SimulatedShutterSensor = ShutterTicksRemaining > 0
                    ? SensorState.Indeterminate
                    : SensorState.Closed;
            machine.HardwareStatus.ShutterSensor = machine.SimulatedShutterSensor;
            }
        }
    }