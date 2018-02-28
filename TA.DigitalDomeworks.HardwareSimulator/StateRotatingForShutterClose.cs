// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: StateRotatingForShutterClose.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:56 by Tim

namespace TI.DigitalDomeWorks.Simulator
    {
    internal class StateRotatingForShutterClose : StateRotating
        {
        internal StateRotatingForShutterClose(SimulatorStateMachine machine) : base(machine) { }

        /// <summary>
        ///     Gets the descriptive name of the current state.
        /// </summary>
        /// <value>The state's descriptive name, as a string.</value>
        protected override string Name => "Moving to Home for Shutter Close";

        /// <summary>
        ///     Transitions to the next logical state. overrides the default behaviour and starts the shutter closing.
        /// </summary>
        protected override void TransitionToNextState()
            {
            Transition(new StateShutterClosing(machine));
            }

        /// <summary>
        ///     Upon entering the state, sets the dome's target azimuth to the home position.
        /// </summary>
        public override void OnEnter()
            {
            machine.TargetAzimuthTicks = machine.HardwareStatus.HomePosition;
            base.OnEnter();
            }
        }
    }