// This file is part of the TI.DigitalDomeWorks project
// 
// Copyright © 2015-2016 Tigra Networks., all rights reserved.
// 
// File: StateRotating.cs  Last modified: 2016-09-12@23:06 by Tim Long

using System;
using System.Timers;

namespace TA.DigitalDomeworks.HardwareSimulator
    {
    internal class StateRotating : SimulatorState
        {
        /// <summary>
        ///     Times the azimuth ticks during simulated dome rotation and pumps the state machine.
        /// </summary>
        private readonly Timer rotationTimer = new Timer {AutoReset = false};

        /// <summary>
        ///     Determines the direction of rotation of the dome.
        ///     Defaults to Clockwise.
        /// </summary>
        private RotationDirection direction = RotationDirection.Clockwise;

        internal StateRotating(SimulatorStateMachine machine) : base(machine) { }

        /// <summary>
        ///     Gets the descriptive name of the current state.
        /// </summary>
        /// <value>The state's descriptive name, as a string.</value>
        protected override string Name => "Rotating Dome";

        public override void OnEnter()
            {
            base.OnEnter();
            int target;
            var azimuth = machine.HardwareStatus.CurrentAzimuth;

            /*
             * If the Dome Support Ring is open and we are in the Home position,
             * then rotation is disallowed. However, if the current azimuth equals
             * the target azimuth (i.e. no rotation required) then we allow the operation
             * to continue, so that shutter operations can complete even when DSR is open.
             */
            if (machine.DomeSupportRingOpen)
                if (machine.InHomeRange(azimuth) && azimuth != machine.TargetAzimuthTicks)
                    {
                    Transition(new StateSendStatus(machine));
                    return;
                    }

            if (machine.TargetAzimuthTicks < machine.HardwareStatus.CurrentAzimuth)
                target = machine.TargetAzimuthTicks + 360;
            else
                target = machine.TargetAzimuthTicks;

            var clockwiseDistance = target - azimuth;
            var counterclockwiseDistance = 360 - clockwiseDistance;

            // Figure out the absolute number of ticks to be moved and the direction of rotation.
            int delta;
            MotorConfiguration azimuthMotorConfiguration;
            if (target == azimuth)
                {
                direction = RotationDirection.Clockwise;
                delta = 0;
                azimuthMotorConfiguration = MotorConfiguration.Stopped;
                //ToDo: Should we emit a "R" at this point if no movement is going to happen?
                }
            else if (clockwiseDistance <= counterclockwiseDistance)
                {
                direction = RotationDirection.Clockwise;
                azimuthMotorConfiguration = MotorConfiguration.Forward;
                delta = clockwiseDistance;
                machine.WriteLine("R");
                }
            else
                {
                direction = RotationDirection.CounterClockwise;
                azimuthMotorConfiguration = MotorConfiguration.Reverse;
                delta = counterclockwiseDistance;
                machine.WriteLine("L");
                }

            Log.Debug("Rotating {0} delta={1} ticks", direction, delta);
            var motorEventArgs = new MotorConfigurationEventArgs
                {
                AzimuthMotor = azimuthMotorConfiguration,
                ShutterMotor = MotorConfiguration.Stopped
                };
            machine.InvokeMotorConfigurationChanged(motorEventArgs);
            rotationTimer.Interval = machine.RealTime
                ? Properties.Settings.Default.RotationRateMsPerTick
                : 1;
            rotationTimer.Elapsed += RotationTimerElapsed;
            rotationTimer.Start();
            }

        /// <summary>
        ///     Called (by the state machine) when exiting from the state
        /// </summary>
        public override void OnExit()
            {
            base.OnExit();
            rotationTimer.Stop();
            rotationTimer.Elapsed -= RotationTimerElapsed;
            // Set the AtHome property.
            machine.HardwareStatus.AtHome =
                machine.InHomeRange(machine.HardwareStatus.CurrentAzimuth);
            }

        /// <summary>
        ///     Provides a stimulus (input) to the state.
        ///     Any stimulus while rotating is interpreted as Emergency Stop.
        /// </summary>
        /// <param name="value">The input value or stimulus.</param>
        public override void Stimulus(char value)
            {
            base.Stimulus(value);
            Transition(new StateEmergencyStop(machine));
            }

        /// <summary>
        ///     Handles the Elapsed event of the rotationTimer control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Timers.ElapsedEventArgs" /> instance containing the event data.</param>
        private void RotationTimerElapsed(object sender, ElapsedEventArgs e)
            {
            if (machine.HardwareStatus.CurrentAzimuth == machine.TargetAzimuthTicks)
                {
                rotationTimer.Stop();
                TransitionToNextState();
                }
            else
                {
                int newAzimuth;
                switch (direction)
                    {
                        case RotationDirection.CounterClockwise:
                            newAzimuth = machine.HardwareStatus.CurrentAzimuth - 1;
                            break;
                        case RotationDirection.Clockwise:
                            newAzimuth = machine.HardwareStatus.CurrentAzimuth + 1;
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }

                // When wrapping the azimuth value, remember that if the circumference is 360 encoder ticks,
                // then those are numbered 0 to 359, so the highest azimuth position is actually one less
                // than the circumference.
                if (newAzimuth >= machine.HardwareStatus.DomeCircumference)
                    newAzimuth = 0;
                if (newAzimuth < 0)
                    newAzimuth = machine.HardwareStatus.DomeCircumference - 1;
                machine.HardwareStatus.CurrentAzimuth = newAzimuth;
                machine.SetAzimuthDependentSensorsAndStates();

                var tx = string.Format("P{0:D3}", newAzimuth);
                Log.Debug("=>{0}", tx);
                machine.WriteLine(tx);
                machine.InvokeAzimuthChanged(new AzimuthChangedEventArgs {NewAzimuth = newAzimuth});
                rotationTimer.Start(); // Prime the pump for the next tick.
                }
            }

        /// <summary>
        ///     Transitions to the next logical state. The default implementation is to transition to the
        ///     <see cref="StateSendStatus" /> state, this behaviour can be overridden in derived classes.
        /// </summary>
        protected virtual void TransitionToNextState()
            {
            Transition(new StateSendStatus(machine));
            }
        }
    }