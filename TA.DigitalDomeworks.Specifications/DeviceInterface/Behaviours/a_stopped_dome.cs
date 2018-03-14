using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.SharedTypes;
using TA.DigitalDomeworks.Specifications.Contexts;

namespace TA.DigitalDomeworks.Specifications.DeviceInterface.Behaviours
    {
    [Behaviors]
    internal class a_stopped_dome : device_controller_behaviour
        {
        It should_not_be_rotating = () => Controller.DomeRotationInProgress.ShouldBeFalse();
        It should_should_not_be_moving_at_all = () => Controller.IsMoving.ShouldBeFalse();
        It should_not_have_a_rotation_direction =
            () => Controller.RotationDirection.ShouldEqual(RotationDirection.None);
        It should_draw_no_shutter_current = () => Controller.ShutterCurrent.ShouldEqual(0);
        It should_not_have_a_shutter_direction = () => Controller.ShutterDirection.ShouldEqual(ShutterDirection.None);
        It should_have_a_stationary_shutter = () => Controller.ShutterMovementInProgress.ShouldBeFalse();
        }
    }