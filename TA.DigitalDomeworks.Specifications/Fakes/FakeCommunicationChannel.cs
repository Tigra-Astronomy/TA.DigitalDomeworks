using System;
using System.Reactive.Linq;
using System.Text;
using TA.Ascom.ReactiveCommunications;

namespace TA.DigitalDomeworks.Specifications.Fakes
    {
    /// <summary>
    ///     A fake communication channel that logs any sent data in <see cref="SendLog" />
    ///     and receives a fake pre-programmed response passed into the constructor.
    ///     The class also keeps a count of how many times each method of <see cref="ICommunicationChannel" /> was called.
    /// </summary>
    public class FakeCommunicationChannel : ICommunicationChannel
        {
        readonly IObservable<char> receivedCharacters;
        readonly StringBuilder sendLog;

        /// <summary>
        ///     Dependency injection constructor.
        ///     Initializes a new instance of the <see cref="SafetyMonitorDriver" /> class.
        /// </summary>
        /// <param name="fakeResponse">Implementation of the injected dependency.</param>
        public FakeCommunicationChannel(string fakeResponse)
            {
            Endpoint = new InvalidEndpoint();
            Response = fakeResponse;
            receivedCharacters = fakeResponse.ToCharArray().ToObservable();
            ObservableReceivedCharacters = receivedCharacters.Concat(Observable.Never<char>());
            sendLog = new StringBuilder();
            IsOpen = false;
            }

        /// <summary>
        ///     Gets the send log.
        /// </summary>
        /// <value>The send log.</value>
        public string SendLog => sendLog.ToString();

        /// <summary>
        ///     Gets a copy of the fake pre-programmed response.
        /// </summary>
        /// <value>The response.</value>
        public string Response { get; }

        public int TimesDisposed { get; set; }

        public int TimesClosed { get; set; }

        public int TimesOpened { get; set; }

        public void Dispose()
            {
            TimesDisposed++;
            }

        public void Open()
            {
            TimesOpened++;
            IsOpen = true;
            }

        public void Close()
            {
            TimesClosed++;
            IsOpen = false;
            }

        public void Send(string txData)
            {
            sendLog.Append(txData);
            }

        public IObservable<char> ObservableReceivedCharacters { get; }

        public bool IsOpen { get; set; }

        public DeviceEndpoint Endpoint { get; }
        }
    }