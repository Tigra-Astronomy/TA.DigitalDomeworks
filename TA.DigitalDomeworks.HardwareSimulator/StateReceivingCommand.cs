// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2016 TiGra Astronomy, all rights reserved.
// 
// File: StateReceivingCommand.cs  Created: 2016-06-20@18:14
// Last modified: 2016-06-21@09:59 by Tim

using System;
using System.Timers;

namespace TI.DigitalDomeWorks.Simulator
    {
    internal class StateReceivingCommand : SimulatorState
        {
        private readonly Timer interCharTimeout = new Timer(Properties.Settings.Default.InterCharTimeoutMs);

        internal StateReceivingCommand(SimulatorStateMachine machine) : base(machine) { }

        /// <summary>
        ///     Gets the descriptive name of the current state.
        /// </summary>
        /// <value>The state's descriptive name, as a string.</value>
        protected override string Name => "ReceiveCommand";

        /// <summary>
        ///     Provides a stimulus (input) to the state.
        /// </summary>
        /// <param name="value">The input value or stimulus.</param>
        public override void Stimulus(char value)
            {
            base.Stimulus(value);
            machine.ReceivedChars.Append(value);
            machine.InvokeReceivedData(EventArgs.Empty);
            if (machine.ReceivedChars.Length >= 4) // All DDW commands are 4 characters.
                {
                Transition(new StateExecutingCommand(machine));
                }
            else if (machine.RealTime)
                {
                // Reset the inter-character timeout.
                interCharTimeout.Stop();
                interCharTimeout.Start();
                }
            }

        /// <summary>
        ///     Called (by the state machine) when exiting from the state.
        ///     Ensures that the inter-character timeout timer is stopped.
        /// </summary>
        public override void OnExit()
            {
            machine.InReadyState.Reset(); // Signal dependent threads that they have to wait for us.
            if (machine.RealTime)
                {
                interCharTimeout.Stop(); // Ensure the timeout timer is stopped.
                interCharTimeout.Elapsed -= InterCharTimeoutElapsed; // Withdraw our delegate.
                }

            base.OnExit();
            }

        /// <summary>
        ///     Called (by the state machine) when entering the state.
        ///     Coinfigures the inter-character timeout timer.
        /// </summary>
        public override void OnEnter()
            {
            base.OnEnter();
            machine.ReceivedChars.Clear();
            if (machine.RealTime)
                interCharTimeout.Elapsed += InterCharTimeoutElapsed;
            machine.InReadyState.Set(); // Signal waiting threads that we are ready to rock.
            }

        /// <summary>
        ///     Handles the Elapsed event of the interCharTimeout control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs" /> instance containing the event data.</param>
        /// <remarks>
        ///     Causes the state to transition to itself, which invokes both <see cref="OnExit" /> and <see cref="OnEnter" />
        ///     and essentially resets the state machine.
        /// </remarks>
        private void InterCharTimeoutElapsed(object sender, ElapsedEventArgs e)
            {
            Log.Info("Inter-character timeout");
            Transition(this);
            }
        }
    }