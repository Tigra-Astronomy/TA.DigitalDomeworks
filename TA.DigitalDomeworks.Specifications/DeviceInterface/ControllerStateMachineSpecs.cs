// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ControllerStateMachineSpecs.cs  Last modified: 2018-03-17@22:53 by Tim Long

using System;
using Machine.Specifications;
using NodaTime;
using NodaTime.Testing;
using TA.DigitalDomeworks.DeviceInterface.StateMachine;
using TA.DigitalDomeworks.SharedTypes;
using TI.DigitalDomeWorks;

namespace TA.DigitalDomeworks.Specifications
    {
    #region  Context base classes
    internal class with_default_controller_state_machine
        {
        Establish context = () => machine = new ControllerStateMachine(SimulateRequestStatus);
        Cleanup after = () =>
            {
            statusRequested = false;
            machine = null;
            };
        protected static ControllerStateMachine machine;
        protected static bool statusRequested;
        protected static Exception Exception;

        static void SimulateRequestStatus()
            {
            statusRequested = true;
            }
        }

    internal class with_controller_state_machine_in_ready_state : with_default_controller_state_machine
        {
        Establish context = () => machine.Initialize(new Ready(machine));
        }

    internal class with_controller_state_machine_in_rotating_state : with_default_controller_state_machine
        {
        Establish context = () => machine.Initialize(new Rotating(machine));
        protected static ControllerStateMachine machine;

        static void SimulateRequestStatus() { }
        }
    #endregion

    [Subject(typeof(ControllerStateMachine), "construction")]
    internal class when_the_state_machine_is_constructed : with_default_controller_state_machine
        {
        It should_start_in_the_uninitialized_state = () => machine.CurrentState.Name.ShouldEqual(nameof(Uninitialized));
        }

    [Subject(typeof(ControllerStateMachine), "initialization")]
    internal class when_the_user_fails_to_initialize_the_state_machine : with_default_controller_state_machine
        {
        Because of = () => Exception = Catch.Exception(() => machine.AzimuthEncoderTickReceived(0));
        It should_throw = () => Exception.ShouldBeOfExactType<InvalidOperationException>();
        }

    [Subject(typeof(ControllerStateMachine), "startup")]
    internal class when_the_state_machine_starts : with_default_controller_state_machine
        {
        Because of = () =>
            {
            machine.Initialize(new RequestStatus(machine));
            var factory = new ControllerStatusFactory(SystemClock.Instance);
            var newStatus = factory.FromStatusPacket(Constants.StrSimulatedStatusResponse);
            machine.HardwareStatusReceived(newStatus);
            };
        It should_request_the_hardware_status = () => statusRequested.ShouldBeTrue();
        It should_finish_in_the_ready_state = () => machine.CurrentState.Name.ShouldEqual(nameof(Ready));
        }


    [Subject(typeof(ControllerStateMachine), "local operations")]
    internal class when_idle_and_an_azimuth_encoder_tick_is_received : with_controller_state_machine_in_ready_state
        {
        Because of = () => machine.AzimuthEncoderTickReceived(100);
        It should_transition_to_rotating_state = () => machine.CurrentState.Name.ShouldEqual(nameof(Rotating));
        It should_update_the_azimuth_property = () => machine.AzimuthEncoderPosition.ShouldEqual(100);
        }

    [Behaviors]
    internal class ShutterMoving
        {
        protected static ControllerStateMachine machine;
        It should_indicate_shutter_motor_active = () => machine.ShutterMotorActive.ShouldBeTrue();
        It should_not_indicate_azimuth_movement = () => machine.AzimuthMotorActive.ShouldBeFalse();
        It should_not_indicate_rotation_direction = () => machine.AzimuthDirection.ShouldEqual(RotationDirection.None);
    }

    [Behaviors]
    internal class AzimuthRotation
        {
        protected static ControllerStateMachine machine;
        It should_not_indicate_shutter_motor_active = () => machine.ShutterMotorActive.ShouldBeFalse();
        It should_not_indicate_shutter_direction = () => machine.ShutterMovementDirection.ShouldEqual(ShutterDirection.None);
        It should_not_indicate_any_shutter_motor_current = () => machine.ShutterMotorCurrent.ShouldEqual(0);
        It should_indicate_azimuth_movement = () => machine.AzimuthMotorActive.ShouldBeTrue();
    }

    [Subject(typeof(ControllerStateMachine), "local operations")]
    internal class when_idle_and_a_shutter_current_measurement_is_received : with_controller_state_machine_in_ready_state
        {
        Because of = () => machine.ShutterMotorCurrentUpdated(15);
        Behaves_like<ShutterMoving> the_shutter_is_moving;
        It should_transition_to_shutter_moving_state = () => machine.CurrentState.Name.ShouldEqual(nameof(ShutterMoving));
        It should_update_the_shutter_current_property = () => machine.ShutterMotorCurrent.ShouldEqual(15);
        It should_not_set_a_shu8tter_direction =
            () => machine.ShutterMovementDirection.ShouldEqual(ShutterDirection.None);
        }

}