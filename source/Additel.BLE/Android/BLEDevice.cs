using Android.App;
using Android.Bluetooth;
using System;
using System.Linq;

namespace Additel.BLE
{
    public partial class BLEDevice
    {
        #region 字段

        private BluetoothManager _manager;
        private BluetoothDevice _device;
        private BLEGATTCallback _callback;
        private BluetoothGatt _gatt;
        #endregion

        #region 属性

        private Guid PlatformUUID
            => _device.GetUUID();

        private string PlatformName
            => _device.Name;

        private BLEDeviceState PlatformState
            => _manager.GetConnectionState(_device, ProfileType.Gatt).ToShared();
        #endregion

        #region 构造

        internal BLEDevice(BluetoothManager manager, BluetoothDevice device)
            : this()
        {
            _manager = manager;
            _device = device;
            _callback = new BLEGATTCallback();

            _callback.ConnectionStateChanged += OnConnectionStateChanged;
            _callback.ReadRemoteRSSI += OnReadRemoteRSSI;
            _callback.ServicesDiscovered += OnServicesDiscovered;
        }
        #endregion

        #region 方法

        private void OnServicesDiscovered(object sender, BLEGATTStatusEventArgs e)
        {
            if (e.Status != GattStatus.Success)
            {
                RaiseDiscoverServicesFailed(e.Status.ToShared());
            }
            else
            {
                var services = _gatt
                    .Services
                    .Select(s => new BLEService(_gatt, _callback, s))
                    .ToList();
                RaiseServicesDiscovered(services);
            }
        }

        private void OnReadRemoteRSSI(object sender, BLEGATTRSSIStatusEventArgs e)
        {
            if (e.Status != GattStatus.Success)
                RaiseReadRSSIFailed(e.Status.ToShared());
            else
                RaiseRSSIRead(e.RSSI);
        }

        private void OnConnectionStateChanged(object sender, BLEGATTProfileStateStatusEventArgs e)
        {
            switch (e.NewState)
            {
                case ProfileState.Connected:
                    {
                        // Android <= 4.4 时，若连接失败，触发此处
                        if (e.Status != GattStatus.Success)
                        {
                            CloseGATT();

                            RaiseConnectFailed(e.Status.ToShared());
                        }
                        else
                        {
                            RaiseConnected();
                        }

                        break;
                    }
                case ProfileState.Disconnected:
                    {
                        // 手动断开后再次连接会非常慢，所以释放掉再次连接
                        if (e.Status != GattStatus.Success)
                            CloseGATT();

                        switch ((int)e.Status)
                        {
                            case 0:
                                {
                                    RaiseDisconnected();
                                    break;
                                }
                            case 8: // 对方蓝牙关闭
                            case 19:    // 对方蓝牙主动断开
                                {
                                    RaiseConnectionLost(e.Status.ToShared());
                                    break;
                                }
                            case 133:
                                {
                                    RaiseConnectFailed(e.Status.ToShared());
                                    break;
                                }
                            default:    // Android > 5.0 时，若连接失败，触发此处
                                {
                                    RaiseConnectFailed(e.Status.ToShared());
                                    break;
                                }
                        }

                        break;
                    }
                case ProfileState.Connecting:
                case ProfileState.Disconnecting:
                default:
                    {
                        break;
                    }
            }
        }

        private void CloseGATT()
        {
            if (_gatt != null)
            {
                _gatt.Close();
                _gatt.Dispose();
                _gatt = null;
            }
        }

        private void PlatformConnect()
        {
            if (_gatt == null)
            {
                _gatt = _device.ConnectGatt(Application.Context, false, _callback);
            }
            else
            {
                _gatt.Connect();
            }
        }

        private void PlatformDisconnect()
            => _gatt.Disconnect();

        private void PlatformReadRSSI()
            => _gatt.ReadRemoteRssi();

        private void PlatformDiscoverServices()
            => _gatt.DiscoverServices();

        private void PlatformDispose(bool disposing)
        {
            if (disposing)
            {
                CloseGATT();

                _callback.ConnectionStateChanged -= OnConnectionStateChanged;
                _callback.ReadRemoteRSSI -= OnReadRemoteRSSI;
                _callback.ServicesDiscovered -= OnServicesDiscovered;

                _device.Dispose();
                _callback.Dispose();
            }
        }
        #endregion
    }
}