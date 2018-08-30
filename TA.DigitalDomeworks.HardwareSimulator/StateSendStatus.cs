// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: StateSendStatus.cs  Last modified: 2018-08-30@02:17 by Tim Long

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
            Machine.InvokeMotorConfigurationChanged(MotorConfigurationEventArgs.AllStopped);
            if (Machine.RealTime)
                {
                pauseTimer.Elapsed += PauseTimerElapsed;
                pauseTimer.Interval = Machine.RealTime ? 500 : 1;
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
            Machine.SetAzimuthDependentSensorsAndStates();
            Machine.WriteLine(Machine.HardwareStatus.ToDdwStatusString());
            Transition(new StateReceivingCommand(Machine));
            }

        public override void OnExit()
            {
            if (Machine.RealTime)
                {
                pauseTimer.Stop();
                pauseTimer.Elapsed -= PauseTimerElapsed;
                }

            base.OnExit();
            }
        }
    }