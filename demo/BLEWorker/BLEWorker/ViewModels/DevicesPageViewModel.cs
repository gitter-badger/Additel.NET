using Additel.Authorization;
using Additel.BLE;
using BLEWorker.Services;
using Prism.Commands;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using Xamarin.Forms;

namespace BLEWorker.ViewModels
{
    public class DevicesPageViewModel : BaseViewModel
    {
        private readonly BLEAdapter _adapter;

        protected INativeService NativeServie { get; }

        public ObservableCollection<DeviceViewModel> Devices { get; set; }

        public bool IsScanning
            => _adapter.IsScanning;

        public DevicesPageViewModel(INavigationService navigationService, INativeService nativeService)
            : base(navigationService)
        {
            Title = "扫描";

            NativeServie = nativeService;

            _adapter = BLEManager.GetAdapter();
            _adapter.ScanStateChanged += OnScanStateChanged;
            _adapter.DeviceDiscovered += OnDeviceDiscovered;

            Devices = new ObservableCollection<DeviceViewModel>();
        }

        private void OnDeviceDiscovered(object sender, BLEDeviceEventArgs e)
        {
            foreach (var item in Devices)
            {
                if (item.UUID == e.Device.UUID)
                    return;
            }

            var device = new DeviceViewModel(e.Device);

            Devices.Add(device);
        }

        private void OnScanStateChanged(object sender, EventArgs e)
            => RaisePropertyChanged(nameof(IsScanning));

        private DelegateCommand _switchScanCommand;
        public DelegateCommand SwitchScanCommand =>
            _switchScanCommand ?? (_switchScanCommand = new DelegateCommand(ExecuteSwitchScanCommand));

        async void ExecuteSwitchScanCommand()
        {
            if (IsScanning) _adapter.StopScan();
            else
            {
                var state = await NativeServie.RequestBluetoothAuthorizationAsync();

                if (state != AuthorizationState.Authorized)
                    return;

                var uuid = Guid.Parse(Config.ADT_S1_UUID);
                _adapter.StartScan(uuid);
            }
        }

        private DelegateCommand<object> _onDeviceSelectedCommand;
        public DelegateCommand<object> OnDeviceSelectedCommand =>
            _onDeviceSelectedCommand ?? (_onDeviceSelectedCommand = new DelegateCommand<object>(ExecuteOnDeviceSelectedCommand));

        async void ExecuteOnDeviceSelectedCommand(object item)
        {
            if (!(item is DeviceViewModel device))
                return;

            var args = new NavigationParameters() { { "Device", device } };

            await NavigationService.NavigateAsync("DevicePage", args);
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {
            base.OnNavigatedFrom(parameters);

            // 停止搜索
            if (IsScanning) _adapter.StopScan();
        }

        public override void Destroy()
        {
            // 注销事件
            _adapter.ScanStateChanged -= OnScanStateChanged;
            _adapter.DeviceDiscovered -= OnDeviceDiscovered;
            // 停止搜索
            if (IsScanning) _adapter.StopScan();
            // 清理设备
            foreach (var device in Devices)
                device.Dispose();

            Devices.Clear();

            base.Destroy();
        }
    }
}
