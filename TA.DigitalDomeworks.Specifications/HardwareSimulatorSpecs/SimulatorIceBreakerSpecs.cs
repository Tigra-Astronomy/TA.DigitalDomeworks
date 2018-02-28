using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Text;
using Machine.Specifications;
using TA.DigitalDomeworks.Specifications.Builders;
using TI.DigitalDomeWorks.Simulator;

namespace TA.DigitalDomeworks.Specifications
    {
    [Subject(typeof(SimulatorStateMachine), "I/O")]
    internal class when_sending_a_get_status_command_to_the_simulator
        {
        Establish context = () =>
            {
            simulator = new HardwareSimulationBuilder().Build();
            subscription = simulator.ObservableResponses
                .ObserveOn(ImmediateScheduler.Instance)
                .Subscribe(
                rx => responses.Append(rx),
                ()=> result = responses.ToString()
                );
            };
        Because of = () =>
            {
            var inputString = "GINF";
            foreach (var c in inputString)
                {
                simulator.InputObserver.OnNext(c);
                }
            simulator.InputObserver.OnCompleted();
            simulator.InReadyState.WaitOne();
            };

        It should_receive_a_status_response = () => result.Length.ShouldBeGreaterThan(0);
        Cleanup after = () => subscription.Dispose();
        static StringBuilder responses = new StringBuilder();
        static SimulatorStateMachine simulator;
        static IDisposable subscription;
        static string result;
        }
    }