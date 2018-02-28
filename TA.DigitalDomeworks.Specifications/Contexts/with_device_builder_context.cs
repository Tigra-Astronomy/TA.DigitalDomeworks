using Machine.Specifications;
using TA.DigitalDomeworks.DeviceInterface;
using TA.DigitalDomeworks.Specifications.Builders;
using TA.DigitalDomeworks.Specifications.DeviceInterface;
using TA.DigitalDomeworks.Specifications.Fakes;

namespace TA.DigitalDomeworks.Specifications.Contexts
    {
    class with_device_controller_context
        {
        Establish context = () => DeviceControllerContextBuilder = new DeviceControllerContextBuilder();
        Cleanup after = () =>
            {
            DeviceControllerContextBuilder = null;
            Context = null;
            };

        #region Convenience properties
        public static DeviceController Controller => Context.Controller;

        public static FakeCommunicationChannel FakeChannel => Context.Channel;
        #endregion Convenience properties

        protected static DeviceControllerContextBuilder DeviceControllerContextBuilder;
        protected static DeviceControllerContext Context;
        }
    }