// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2014 TiGra Astronomy, all rights reserved.
// 
// File: StateEventArgs.cs  Created: 2014-10-29@20:45
// Last modified: 2014-11-12@05:54 by Tim

using System;

namespace TI.DigitalDomeWorks.Simulator
    {
    /// <summary>
    ///     Event arguments used by state machine events.
    /// </summary>
    public class StateEventArgs : EventArgs
        {
        /// <summary>
        ///     Initializes a new instance of the <see cref="StateEventArgs" /> class.
        /// </summary>
        /// <param name="stateName">Name of the new state.</param>
        public StateEventArgs(string stateName)
            {
            StateName = stateName;
            }

        /// <summary>
        ///     Gets or sets the name of the new state.
        /// </summary>
        /// <value>The name of the state.</value>
        public string StateName { get; }
        }
    }