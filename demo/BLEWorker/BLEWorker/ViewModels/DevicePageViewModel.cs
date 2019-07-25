using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace BLEWorker.ViewModels
{
    public class DevicePageViewModel : BaseViewModel
    {
        private DeviceViewModel _device;

        private Guid _uuid;
        public Guid UUID
        {
            get { return _uuid; }
            set { SetProperty(ref _uuid, value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            set { SetProperty(ref _isConnected, value); }
        }

        private int _interval;
        public int Interval
        {
            get { return _interval; }
            set { SetProperty(ref _interval, value); }
        }

        private bool _isContinuousWrite;
        public bool IsContinuousWrite
        {
            get { return _isContinuousWrite; }
            set { SetProperty(ref _isContinuousWrite, value); }
        }

        private int _sendCount;
        public int SendCount
        {
            get { return _sendCount; }
            set { SetProperty(ref _sendCount, value); }
        }

        private int _receivedCount;
        public int ReceivedCount
        {
            get { return _receivedCount; }
            set { SetProperty(ref _receivedCount, value); }
        }

        private bool _isContinuousWriting;
        public bool IsContinuousWriting
        {
            get { return _isContinuousWriting; }
            set { SetProperty(ref _isContinuousWriting, value); }
        }

        protected IPageDialogService DialogService { get; }

        public ObservableCollection<string> Values { get; set; }

        public DevicePageViewModel(INavigationService navigationService, IPageDialogService dialogService)
            : base(navigationService)
        {
            DialogService = dialogService;
            Values = new ObservableCollection<string>();
        }

        private DelegateCommand<string> _writeCommand;
        public DelegateCommand<string> WriteCommand =>
            _writeCommand ?? (_writeCommand = new DelegateCommand<string>(ExecuteWriteCommand, CanExecuteWriteCommand).ObservesProperty(() => IsConnected).ObservesProperty(() => IsContinuousWriting));

        bool CanExecuteWriteCommand(string value)
            => IsConnected && !IsContinuousWriting && !string.IsNullOrEmpty(value);

        async void ExecuteWriteCommand(string value)
        {
            var cmd = $"{Config.DIAG_COMM_QUAL_ECHO} \"{value}\"";

            if (_device == null)
                return;

            if (IsContinuousWrite)
            {
                await Task.Run(() => ContinuousWrite(cmd));
            }
            else
            {
                await _device.WriteAsync(cmd);
                SendCount++;
            }
        }

        private async void ContinuousWrite(string cmd)
        {
            IsContinuousWriting = true;
            do
            {
                await _device.WriteAsync(cmd);
                SendCount++;
                await Task.Delay(1000);
            } while (IsContinuousWrite);
            IsContinuousWriting = false;
        }

        private DelegateCommand _resetCommand;
        public DelegateCommand ResetCommand =>
            _resetCommand ?? (_resetCommand = new DelegateCommand(ExecuteResetCommand));

        void ExecuteResetCommand()
        {
            SendCount = ReceivedCount = 0;
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var key = "Device";
            if (!parameters.ContainsKey(key) || !(parameters[key] is DeviceViewModel device))
                return;

            _device = device;
            _device.ErrorOccured += OnErrorOccured;
            _device.ConnectionStateChanged += OnConnectionStateChanged;
            _device.CommunicationQualityReceived += OnCommunicationQualityReceived;

            UUID = _device.UUID;
            Title = Name = _device.Name;
            IsConnected = _device.IsConnected;

            if (_device.ConnectCommand.CanExecute())
                _device.ConnectCommand.Execute();
        }

        private async void OnErrorOccured(object sender, string error)
        {
            await DialogService.DisplayAlertAsync("遇到问题", error, "返回");
        }

        private void OnCommunicationQualityReceived(object sender, string state, string value)
        {
            Values.Add($"{state} - {value}");
            ReceivedCount++;
        }

        private void OnConnectionStateChanged(object sender, bool state)
        {
            IsConnected = state;
        }

        public override void Destroy()
        {
            if (_device != null)
            {
                _device.ErrorOccured -= OnErrorOccured;
                _device.ConnectionStateChanged -= OnConnectionStateChanged;
                _device.CommunicationQualityReceived -= OnCommunicationQualityReceived;

                if (_device.DisconnectCommand.CanExecute())
                    _device.DisconnectCommand.Execute();
            }

            base.Destroy();
        }
    }
}
