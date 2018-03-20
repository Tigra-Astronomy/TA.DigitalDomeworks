// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: StateRotatingForShutterOpen.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:56 by Tim

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    internal class StateRotatingForShutterOpen : StateRotating
        {
        internal StateRotatingForShutterOpen(SimulatorStateMachine machine) : base(machine) { }

        /// <summary>
        ///     Gets the descriptive name of the current state.
        /// </summary>
        /// <value>The state's descriptive name, as a string.</value>
        protected override string Name => "Moving To Home for Shutter Open";

        /// <summary>
        ///     Transitions to the next logical state. Overrides the default behaviour and begins opening the shutter.
        /// </summary>
        protected override void TransitionToNextState()
            {
            Transition(new StateShutterOpening(Machine));
            }

        /// <summary>
        ///     Upon entering the state, sets the dome's target azimuth to the home position.
        /// </summary>
        public override void OnEnter()
            {
            Machine.TargetAzimuthTicks = Machine.HardwareStatus.HomePosition;
            base.OnEnter();
            }
        }
    }