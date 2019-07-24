using Prism.Commands;
using Prism.Navigation;
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

        public ObservableCollection<string> Values { get; set; }

        public DevicePageViewModel(INavigationService navigationService)
            : base(navigationService)
        {
            Values = new ObservableCollection<string>();
        }

        private DelegateCommand<string> _writeCommand;
        public DelegateCommand<string> WriteCommand =>
            _writeCommand ?? (_writeCommand = new DelegateCommand<string>(ExecuteWriteCommand));

        void ExecuteWriteCommand(string value)
        {
            var cmd = $"{Config.DIAG_COMM_QUAL_ECHO} {value}";

            if (_device == null || !_device.WriteCommand.CanExecute(cmd))
                return;

            if (IsContinuousWrite)
            {
                Task.Run(() => ContinuousWrite(cmd));
            }
            else
            {
                _device.WriteCommand.Execute(cmd);
            }
        }

        private void ContinuousWrite(string cmd)
        {
            do
            {
                _device.WriteCommand.Execute(cmd);
                Thread.Sleep(Interval);
            } while (IsContinuousWrite);
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            base.OnNavigatedTo(parameters);

            var key = "Device";
            if (!parameters.ContainsKey(key) || !(parameters[key] is DeviceViewModel device))
                return;

            _device = device;
            _device.ConnectionStateChanged += OnConnectionStateChanged;
            _device.CommunicationQualityReceived += OnCommunicationQualityReceived;

            UUID = _device.UUID;
            Name = _device.Name;
            IsConnected = _device.IsConnected;

            if (_device.ConnectCommand.CanExecute())
                _device.ConnectCommand.Execute();
        }

        private void OnCommunicationQualityReceived(object sender, string state, string value)
        {
            Values.Add(value);
        }

        private void OnConnectionStateChanged(object sender, bool state)
        {
            IsConnected = state;
        }

        public override void Destroy()
        {
            if (_device != null) _device.Dispose();

            base.Destroy();
        }
    }
}
