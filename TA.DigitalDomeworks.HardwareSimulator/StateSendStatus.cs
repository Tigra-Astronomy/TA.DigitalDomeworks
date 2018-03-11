// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: StateSendStatus.cs  Created: 2014-10-05@00:56
// Last modified: 2014-11-12@05:56 by Tim

using System.Timers;

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    internal class StateSendStatus : SimulatorState
        {
        /// <summary>
        ///     Times the pause between command completion and sending the status response
        /// </summary>
        private readonly Timer pauseTimer = new Timer {AutoReset = false};

        protected internal StateSendStatus(SimulatorStateMachine machine) : base(machine) { }

        protected override string Name => "Send Status";

        /// <summary>
        ///     Called (by the state machine) when entering the state.
        /// </summary>
        public override void OnEnter()
            {
            base.OnEnter();
            machine.InvokeMotorConfigurationChanged(MotorConfigurationEventArgs.AllStopped);
            if (machine.RealTime)
                {
                pauseTimer.Elapsed += PauseTimerElapsed;
                pauseTimer.Interval = machine.RealTime ? 500 : 1;
                pauseTimer.Start();
                }
            else
                {
                SendStatus();
                }
            }

        /// <summary>
        ///     Handles the Elapsed event from the timer.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs" /> instance containing the event data.</param>
        private void PauseTimerElapsed(object sender, ElapsedEventArgs e)
            {
            pauseTimer.Stop();
            SendStatus();
            }

        private void SendStatus()
            {
            // Update state that could have been affected by the last operation.
            machine.SetAzimuthDependentSensorsAndStates();
            machine.WriteLine(machine.HardwareStatus.ToString());
            Transition(new StateReceivingCommand(machine));
            }

        public override void OnExit()
            {
            if (machine.RealTime)
                {
                pauseTimer.Stop();
                pauseTimer.Elapsed -= PauseTimerElapsed;
                }

            base.OnExit();
            }
        }
    }