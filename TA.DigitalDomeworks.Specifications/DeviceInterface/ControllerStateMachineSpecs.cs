
using System;
using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface.StateMachine;
using TA.DigitalDomeworks.Specifications.Contexts;
using TA.DigitalDomeworks.Specifications.DeviceInterface.Behaviours;

namespace TA.DigitalDomeworks.Specifications
    {
    [Subject(typeof(ControllerStateMachine), "construction")]
    internal class when_the_state_machine_is_constructed
        {
        Establish context = () => machine = new ControllerStateMachine();
        It should_start_in_the_uninitialized_state = () => machine.CurrentState.Name.ShouldEqual(nameof(Uninitialized));
        static ControllerStateMachine machine;
        }
    [Subject(typeof(ControllerStateMachine), "local operations")]
    internal class when_the_local_user_rotates_the_dome : with_controller_in_ready_state
        {
        Because of = () => machine.AzimuthEncoderTickReceived(100);
        It should_transition_to_rotating_state = () => machine.CurrentState.Name.ShouldEqual(nameof(Rotating));
        It should_update_the_azimuth_property = () => machine.AzimuthEncoderPosition.ShouldEqual(100);
        }

    [Subject(typeof(ControllerStateMachine), "initialization")]
    internal class when_the_user_fails_to_initialize_the_state_machine
        {
        Establish context = () => machine = new ControllerStateMachine() ;
        Because of = () => Exception = Catch.Exception(() => machine.AzimuthEncoderTickReceived(0));
        It should_throw = () => Exception.ShouldBeOfExactType<InvalidOperationException>();
        static Exception Exception;
        static ControllerStateMachine machine;
        }

    internal class with_controller_in_ready_state {
        Establish context = () =>
            {
            machine = new ControllerStateMachine();
            machine.Initialize(new Ready(machine));
            };
        protected static ControllerStateMachine machine;
        }
    internal class with_controller_in_rotating_state {
        Establish context = () =>
            {
            machine = new ControllerStateMachine();
            machine.Initialize(new Rotating(machine));
            };
        protected static ControllerStateMachine machine;
        }
    }