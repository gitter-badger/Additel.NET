using Android.App;
using Android.Bluetooth;
using Android.Bluetooth.LE;
using Android.Content;
using Android.OS;
using Java.Util;
using System;
using System.Linq;

namespace Additel.BLE
{
    public partial class BLEAdapter
    {
        #region 字段

        private BluetoothManager _manager;
        private BLEBroadcastReceiver _receiver;
        private BLEScanCallback18 _callback18;
        private BLEScanCallback21 _callback21;

        private bool _isScanning;
        #endregion

        #region 属性

        private BLEAdapterState PlatformState
            => _manager.Adapter.State.ToShared();

        private bool PlatformIsScanning
        {
            get => _isScanning;
            set
            {
                if (_isScanning == value)
                    return;

                _isScanning = value;

                RaiseScanStateChanged();
            }
        }

        public BLEScanMode ScanMode { get; set; }
        #endregion

        #region 构造

        internal BLEAdapter(BluetoothManager manager)
            : this()
        {
            _manager = manager;
            _receiver = new BLEBroadcastReceiver();
            _receiver.AdapterStateChanged += OnAdapterStateChanged;

            Application.Context.RegisterReceiver(_receiver, new IntentFilter(BluetoothAdapter.ActionStateChanged));

            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
            {
                _callback18 = new BLEScanCallback18();
                _callback18.LEScan += OnLEScan;
            }
            else
            {
                _callback21 = new BLEScanCallback21();
                _callback21.ScanResult += OnScanResult;
                _callback21.ScanFailed += OnScanFailed;
            }
        }
        #endregion

        #region 方法

        private void OnScanFailed(object sender, BLEScanFailureEventArgs e)
        {
            // 如果扫描已经开始，不需要更改扫描状态
            // 否则，扫描状态为 false
            if (e.ErrorCode != ScanFailure.AlreadyStarted)
                PlatformIsScanning = false;

            RaiseScanFailed(e.ErrorCode.ToShared());
        }

        private void OnScanResult(object sender, BLEScanResultEventArgs e)
        {
            var device = new BLEDevice(_manager, e.Result.Device);
            var rssi = e.Result.Rssi;
            var advertisements = BLEAdvertisement.Parse(e.Result.ScanRecord.GetBytes());
            RaiseDeviceDiscovered(device, rssi, advertisements);
        }

        private void OnLEScan(object sender, BLELEScanEventArgs e)
        {
            var device = new BLEDevice(_manager, e.Device);
            var rssi = e.RSSI;
            var advertisements = BLEAdvertisement.Parse(e.ScanRecord);
            RaiseDeviceDiscovered(device, rssi, advertisements);
        }

        private void OnAdapterStateChanged(object sender, BLEActionStateEventArgs e)
        {
            if (e.NewState == Android.Bluetooth.State.Off &&
                IsScanning)
            {
                PlatformIsScanning = false;
            }

            RaiseStateChanged(e.OldState.ToShared(), e.NewState.ToShared());
        }

        private void PlatformStartScan(Guid[] uuids)
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
                StartScan18(uuids);
            else
                StartScan21(uuids);
        }

        private void StartScan21(Guid[] uuids)
        {
            using (var builder1 = new ScanFilter.Builder())
            using (var builder2 = new ScanSettings.Builder())
            {
                var filters = uuids
                    .Select(u => builder1.SetServiceUuid(ParcelUuid.FromString(u.ToString())).Build())
                    .ToList();
                var settings = builder2.SetScanMode(ScanMode.ToAndroid()).Build();

                _manager.Adapter.BluetoothLeScanner.StartScan(filters, settings, _callback21);
            }

            PlatformIsScanning = true;
        }

        private void StartScan18(Guid[] uuids)
        {
            var serviceUuids = uuids?.Select(u => UUID.FromString(u.ToString())).ToArray();

#pragma warning disable CS0618
            if (_manager.Adapter.StartLeScan(serviceUuids, _callback18))
                PlatformIsScanning = true;
#pragma warning restore CS0618
            else
                RaiseScanFailed(BLEErrorCode.InternalError);
        }

        private void PlatformStopScan()
        {
            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
#pragma warning disable 618
                _manager.Adapter.StopLeScan(_callback18);
#pragma warning restore 618
            else
                _manager.Adapter.BluetoothLeScanner.StopScan(_callback21);

            PlatformIsScanning = false;
        }

        private void PlatformDispose(bool disposing)
        {
            if (disposing)
            {
                _receiver.AdapterStateChanged -= OnAdapterStateChanged;

                Application.Context.UnregisterReceiver(_receiver);

                if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
                {
                    _callback18.LEScan -= OnLEScan;
                    _callback18.Dispose();
                }
                else
                {
                    _callback21.ScanResult -= OnScanResult;
                    _callback21.ScanFailed -= OnScanFailed;
                    _callback21.Dispose();
                }

                _receiver.Dispose();
                _manager.Dispose();
            }
        }
        #endregion
    }
}