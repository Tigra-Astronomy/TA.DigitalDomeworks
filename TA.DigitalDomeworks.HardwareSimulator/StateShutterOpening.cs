// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: StateShutterOpening.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:56 by Tim

using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    /// <summary>
    ///     Variant of <see cref="StateShutterMoving" /> used when the shutter is opening.
    ///     The finishing state can be either indeterminate or open.
    /// </summary>
    internal class StateShutterOpening : StateShutterMoving
        {
        public StateShutterOpening(SimulatorStateMachine machine) : base(machine, MotorConfiguration.Forward) { }

        protected override string Name => "Shutter Opening";

        /// <summary>
        ///     Called (by the state machine) when entering the state.
        ///     If the shutter is already open, transitions directly to <see cref="StateSendStatus" />. Otherwise,
        ///     otherwise delegates shutter movement to the base class.
        /// </summary>
        public override void OnEnter()
            {
            if (machine.SimulatedShutterSensor == SensorState.Open || machine.ShutterStuck)
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
                    : SensorState.Open;
            machine.HardwareStatus.ShutterSensor = machine.SimulatedShutterSensor;
            }
        }
    }