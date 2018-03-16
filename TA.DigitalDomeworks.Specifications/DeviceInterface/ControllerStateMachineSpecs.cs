
using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface.StateMachine;
using TA.DigitalDomeworks.Specifications.Contexts;
using TA.DigitalDomeworks.Specifications.DeviceInterface.Behaviours;

namespace TA.DigitalDomeworks.Specifications
    {
    [Subject(typeof(ControllerStateMachine), "Startup")]
    internal class when_the_state_machine_starts
        {
        Establish context = () => machine = new ControllerStateMachine();
        It should_start_in_the_idle_state = () => machine.CurrentState.Name.ShouldEqual(nameof(Ready));
        static ControllerStateMachine machine;
        }
    [Subject(typeof(ControllerStateMachine), "local operations")]
    internal class when_the_local_user_rotates_the_dome : with_device_controller_context
        {
        Establish context = () => machine = new ControllerStateMachine();
        Because of = () => machine.AzimuthEncoderTickReceived(100);
        It should_transition_to_rotating_state = () => machine.CurrentState.Name.ShouldEqual(nameof(Rotating));
        static ControllerStateMachine machine;
        //Goal: Behaves_like<a_directionless_rotating_dome> a_rotating_dome;
        }
    }