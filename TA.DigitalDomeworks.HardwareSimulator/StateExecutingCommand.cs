// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2016 TiGra Astronomy, all rights reserved.
// 
// File: StateExecutingCommand.cs  Created: 2016-06-20@18:14
// Last modified: 2016-09-11@00:42 by Tim

using System.Globalization;
using TA.DigitalDomeworks.SharedTypes;
using TI.DigitalDomeWorks;

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    internal class StateExecutingCommand : SimulatorState
        {
        protected internal StateExecutingCommand(SimulatorStateMachine machine) : base(machine) { }

        protected override string Name => "Executing Command";

        /// <summary>
        ///     Called (by the state machine) when entering the state. At this point, there should be a received command
        ///     packet in the receive buffer. We need to parse it and act appropriately.
        /// </summary>
        public override void OnEnter()
            {
            base.OnEnter();
            Log.Debug($"Processing command [{machine.ReceivedChars}]");
            if (machine.ReceivedChars.Length == 0)
                {
                Log.Warn("Nothing received");
                Transition(new StateReceivingCommand(machine));
                }

            if (machine.ReceivedChars.Length != 4)
                {
                Log.Warn("Received string not 4 characters");
                Transition(new StateReceivingCommand(machine));
                }

            ExecuteCommand();
            }

        /// <summary>
        ///     Any received character at this point should be interpreted as an 'All Stop' instruction,
        ///     unless it is a carriage return, in which case it should be ignored.
        /// </summary>
        /// <param name="value">The input value or stimulus.</param>
        public override void Stimulus(char value)
            {
            base.Stimulus(value);
            if (value == (char) AsciiSymbols.CR)
                return; // Ignore carriage returns

            Transition(new StateEmergencyStop(machine));
            }

        /// <summary>
        ///     Parses and executes the received command.
        /// </summary>
        /// <remarks>
        ///     The possible valid commands are:
        ///     <list>
        ///         <listheader>
        ///             <item>Command</item>
        ///             <description>Description</description>
        ///         </listheader>
        ///         <item>GXXX</item>
        ///         <description>XXX is a 3-digit decimal representing the target dome azimuth in degrees (range 0 to 359).</description>
        ///         <item>GHOM</item>
        ///         <description>Move to the HOME position.</description>
        ///     </list>
        /// </remarks>
        private void ExecuteCommand()
            {
            var command = machine.ReceivedChars.ToString();
            switch (command)
                {
                    case Constants.CmdCancelTimeout:
                        return;
                    case Constants.CmdClose:
                        Transition(new StateRotatingForShutterClose(machine));
                        return;
                    case Constants.CmdFastTrack:
                        return;
                    case Constants.CmdGetInfo:
                        Transition(new StateSendStatus(machine));
                        return;
                    case Constants.CmdGotoHome:
                        Transition(new StateRotatingToHome(machine));
                        return;
                    case Constants.CmdOpen:
                        Transition(new StateRotatingForShutterOpen(machine));
                        return;
                    case Constants.CmdPark:
                        return;
                    case Constants.CmdSetUserPins: // ToDo - requires special handling [this can never match]
                        break;
                    case Constants.CmdSlaveOff:
                        return;
                    case Constants.CmdSlaveOn:
                        return;
                    case Constants.CmdTest:
                        return;
                    case Constants.CmdTrain:
                        return;
                    case Constants.CmdUnpark:
                        return;
                }

            // Other possibilities are: Gxxx GPnn and Csdr
            if (command.StartsWith("GP"))
                {
                // User pin manipulation
                byte pins;
                if (byte.TryParse(command.Substring(2, 2),
                    NumberStyles.HexNumber,
                    CultureInfo.InvariantCulture,
                    out pins))
                    {
                    // Successfully parsed a user I/O pin packet.
                    machine.HardwareStatus.UserPins = pins;
                    Transition(new StateSendStatus(machine));
                    }
                }
            else if (machine.ReceivedChars[0] == 'G')
                {
                int targetDegrees;
                if (int.TryParse(command.Substring(1, 3),
                    NumberStyles.Integer,
                    CultureInfo.CurrentCulture,
                    out targetDegrees))
                    {
                    // Successfully recognised a rotation command.
                    if (machine.InDeadZone(targetDegrees))
                        {
                        Transition(new StateSendStatus(machine));
                        return;
                        }

                    machine.TargetAzimuthDegrees = targetDegrees;
                    Transition(new StateRotating(machine));
                    }
                else
                    {
                    Log.Warn("Gxxx command had invalid numeric part (ignored)");
                    Transition(new StateReceivingCommand(machine));
                    }
                }
            //ToDo: Smart Tracking will likely be implemented in its own state.
            //else if (false) // if it's a smart-tracking command
            //{
            //    //ToDo: Implement smart tracking command (maybe not)
            //    Diagnostics.TraceWarning("Command may be 'smart tracking' but SmartTracking is not implemented.");
            //}
            else
                {
                // Anything else must be an invalid or corrupted command, so discard it.
                Transition(new StateReceivingCommand(machine));
                }
            }
        }
    }