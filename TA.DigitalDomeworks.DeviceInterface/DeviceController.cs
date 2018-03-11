using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using PostSharp.Patterns.Model;
using TA.Ascom.ReactiveCommunications;
using TA.DigitalDomeworks.SharedTypes;

namespace TA.DigitalDomeworks.DeviceInterface
    {
    [NotifyPropertyChanged]
    internal class DeviceController : INotifyPropertyChanged
        {
        [NotNull] private readonly ICommunicationChannel channel;
        [NotNull] private readonly ControllerStatusFactory statusFactory;
        [CanBeNull] private ReactiveTransactionProcessor transactionProcessor;

        public DeviceController(ICommunicationChannel channel, ControllerStatusFactory factory)
            {
            this.channel = channel;
            statusFactory = factory;
            }

        public void Open()
            {
            var observer = new TransactionObserver(channel);
            transactionProcessor = new ReactiveTransactionProcessor();
            transactionProcessor.SubscribeTransactionObserver(observer);
            channel.Open();
            PerformTasksOnConnect();
            }

        private void PerformTasksOnConnect()
            {
            var transaction = new StatusTransaction(statusFactory);
            transactionProcessor.CommitTransaction(transaction);
            transaction.WaitForCompletionOrTimeout();   // Synchronous
            transaction.ThrowIfFailed();
            CurrentStatus = transaction.ControllerStatus;
            }

        public IControllerStatus CurrentStatus { get; private set; }

        public void Close()
            {
            transactionProcessor?.Dispose();
            transactionProcessor = null;
            }

        public bool IsOnline => channel.IsOpen;

        public async Task<IControllerStatus> GetStatus()
            {
            var getStatusTransaction = new StatusTransaction(statusFactory);
            transactionProcessor.CommitTransaction(getStatusTransaction);
            await getStatusTransaction.WaitForCompletionOrTimeoutAsync(CancellationToken.None);
            getStatusTransaction.ThrowIfFailed();
            return getStatusTransaction.ControllerStatus;
            }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
}