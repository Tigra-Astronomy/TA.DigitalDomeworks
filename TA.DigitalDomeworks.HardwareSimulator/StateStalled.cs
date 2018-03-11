// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: StateStalled.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:56 by Tim

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    /// <summary>
    ///     This state is used when an unrecoverable error has occurred.
    /// </summary>
    internal class StateStalled : SimulatorState
        {
        internal StateStalled(SimulatorStateMachine machine) : base(machine) { }

        protected override string Name => "Stalled";
        }
    }