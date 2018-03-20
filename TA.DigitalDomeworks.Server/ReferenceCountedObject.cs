using System.Runtime.InteropServices;

namespace TA.DigitalDomeworks.Server
{
    [ComVisible(false)]
    public abstract class ReferenceCountedObject
    {
        public ReferenceCountedObject()
        {
            // We increment the global count of objects.
            Server.CountObject();
        }

        ~ReferenceCountedObject()
        {
            // We decrement the global count of objects.
            Server.UncountObject();
            // We then immediately test to see if we the conditions
            // are right to attempt to terminate this server application.
            Server.ExitIf();
        }
    }
}
