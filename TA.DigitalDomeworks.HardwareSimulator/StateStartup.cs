// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2016 TiGra Astronomy, all rights reserved.
// 
// File: StateStartup.cs  Created: 2016-06-20@18:14
// Last modified: 2016-06-21@09:59 by Tim

using System;

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    internal class StateStartup : SimulatorState
        {
        public StateStartup(SimulatorStateMachine machine) : base(machine) { }

        protected override string Name => "Startup";

        /// <summary>
        ///     Called (by the state machine) when entering the state.
        ///     The Startup state initialises and items that must be initialised,
        ///     then immediately transitions to the <see cref="StateReceivingCommand" /> state.
        /// </summary>
        public override void OnEnter()
            {
            //ToDo: Perform any initialization here.
            try
                {
                // If all initialization succeeded, immediately transition to the Ready state.
                Transition(new StateReceivingCommand(machine));
                }
            catch (Exception ex)
                {
                Log.Error(ex, "Exception opening serial port");
                Transition(new StateStalled(machine));
                }
            }
        }
    }