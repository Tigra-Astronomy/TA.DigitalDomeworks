using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TA.DigitalDomeworks.Specifications.Helpers
{
    internal static class ObservableTestExtensions
    {

    public static void SubscribeAndWaitForCompletion<T>(this IObservable<T> sequence, Action<T> observer)
        {
        var sequenceComplete = new ManualResetEvent(false);
        var subscription = sequence.Subscribe(
            onNext: observer,
            onCompleted: () => sequenceComplete.Set()
            );
        sequenceComplete.WaitOne();
        subscription.Dispose();
        sequenceComplete.Dispose();
        }
    }
}
