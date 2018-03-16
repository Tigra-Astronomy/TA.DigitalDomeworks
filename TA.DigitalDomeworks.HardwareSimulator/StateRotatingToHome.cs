// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: StateRotatingToHome.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:56 by Tim

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    internal class StateRotatingToHome : StateRotating
        {
        internal StateRotatingToHome(SimulatorStateMachine machine) : base(machine) { }

        protected override string Name => "Rotating to Home";

        public override void OnEnter()
            {
            Machine.TargetAzimuthTicks = Machine.HardwareStatus.HomePosition;
            base.OnEnter();
            }
        }
    }