// This file is part of the TA.DigitalDomeworks project
// 
// Copyright © 2016-2018 Tigra Astronomy, all rights reserved.
// 
// File: ControllerStateMachineSpecs.cs  Last modified: 2018-03-17@22:53 by Tim Long

using System;
using FakeItEasy;
using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface.StateMachine;
using TA.DigitalDomeworks.SharedTypes;
using TI.DigitalDomeWorks;

namespace TA.DigitalDomeworks.Specifications
    {
    #region  Context base classes
    internal class with_default_controller_state_machine
        {
        Establish context = () =>
            {
            FakeControllerActions = A.Fake<IControllerActions>();
            Machine = new ControllerStateMachine(FakeControllerActions);
            };
        Cleanup after = () =>
            {
            StatusRequested = false;
            Machine = null;
            };
        protected static ControllerStateMachine Machine;
        protected static bool StatusRequested;
        protected static Exception Exception;
        protected static IControllerActions FakeControllerActions;

        static void SimulateRequestStatus()
            {
            StatusRequested = true;
            }
        }

    internal class with_controller_state_machine_in_ready_state : with_default_controller_state_machine
        {
        Establish context = () => Machine.Initialize(new Ready(Machine));
        }

    internal class with_controller_state_machine_in_rotating_state : with_default_controller_state_machine
        {
        Establish context = () => Machine.Initialize(new Rotating(Machine));

        static void SimulateRequestStatus() { }
        }
    #endregion

    [Subject(typeof(ControllerStateMachine), "construction")]
    internal class when_the_state_machine_is_constructed : with_default_controller_state_machine
        {
        It should_start_in_the_uninitialized_state = () => Machine.CurrentState.Name.ShouldEqual(nameof(Uninitialized));
        }

    [Subject(typeof(ControllerStateMachine), "initialization")]
    internal class when_the_user_fails_to_initialize_the_state_machine : with_default_controller_state_machine
        {
        Because of = () => Exception = Catch.Exception(() => Machine.AzimuthEncoderTickReceived(0));
        It should_throw = () => Exception.ShouldBeOfExactType<InvalidOperationException>();
        }

    [Subject(typeof(ControllerStateMachine), "startup")]
    internal class when_the_state_machine_starts : with_default_controller_state_machine
        {
        Because of = () =>
            {
            Machine.Initialize(new RequestStatus(Machine));
            var factory = new ControllerStatusFactory(new SystemDateTimeUtcClock());
            var newStatus = factory.FromStatusPacket(Constants.StrSimulatedStatusResponse);
            Machine.HardwareStatusReceived(newStatus);
            };
        It should_request_the_hardware_status = () =>
            A.CallTo(() => FakeControllerActions.RequestHardwareStatus()).MustHaveHappened();
        It should_finish_in_the_ready_state = () => Machine.CurrentState.Name.ShouldEqual(nameof(Ready));
        }


    [Subject(typeof(ControllerStateMachine), "local operations")]
    internal class when_idle_and_an_azimuth_encoder_tick_is_received : with_controller_state_machine_in_ready_state
        {
        Because of = () => Machine.AzimuthEncoderTickReceived(100);
        It should_transition_to_rotating_state = () => Machine.CurrentState.Name.ShouldEqual(nameof(Rotating));
        It should_update_the_azimuth_property = () => Machine.AzimuthEncoderPosition.ShouldEqual(100);
        }

    [Behaviors]
    internal class ShutterMoving
        {
        protected static ControllerStateMachine Machine;
        It should_indicate_shutter_motor_active = () => Machine.ShutterMotorActive.ShouldBeTrue();
        It should_not_indicate_azimuth_movement = () => Machine.AzimuthMotorActive.ShouldBeFalse();
        It should_not_indicate_rotation_direction = () => Machine.AzimuthDirection.ShouldEqual(RotationDirection.None);
    }

    [Behaviors]
    internal class AzimuthRotation
        {
        protected static ControllerStateMachine Machine;
        It should_not_indicate_shutter_motor_active = () => Machine.ShutterMotorActive.ShouldBeFalse();
        It should_not_indicate_shutter_direction = () => Machine.ShutterMovementDirection.ShouldEqual(ShutterDirection.None);
        It should_not_indicate_any_shutter_motor_current = () => Machine.ShutterMotorCurrent.ShouldEqual(0);
        It should_indicate_azimuth_movement = () => Machine.AzimuthMotorActive.ShouldBeTrue();
    }

    [Subject(typeof(ControllerStateMachine), "local operations")]
    internal class when_idle_and_a_shutter_current_measurement_is_received : with_controller_state_machine_in_ready_state
        {
        Because of = () => Machine.ShutterMotorCurrentReceived(15);
        Behaves_like<ShutterMoving> the_shutter_is_moving;
        It should_transition_to_shutter_moving_state = () => Machine.CurrentState.Name.ShouldEqual(nameof(ShutterMoving));
        It should_update_the_shutter_current_property = () => Machine.ShutterMotorCurrent.ShouldEqual(15);
        It should_not_set_a_shutter_direction =
            () => Machine.ShutterMovementDirection.ShouldEqual(ShutterDirection.None);
        }

}