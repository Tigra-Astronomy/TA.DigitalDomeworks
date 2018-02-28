using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using TI.DigitalDomeWorks.Simulator;

namespace TA.DigitalDomeworks.Specifications.Builders
{
    internal class HardwareSimulationBuilder
    {
    private IObservable<char> inputSequence = Observable.Empty<char>();

    public SimulatorStateMachine Build()
        {
        var machine = new SimulatorStateMachine(realTime: false);
        return machine;
        }
    }
}
