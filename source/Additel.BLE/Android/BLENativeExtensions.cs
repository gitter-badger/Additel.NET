using Android.Bluetooth;
using Android.Bluetooth.LE;
using Java.Util;
using System;
using System.Linq;
using ScanMode = Android.Bluetooth.LE.ScanMode;

namespace Additel.BLE
{
    internal static class BLENativeExtensions
    {
        public static BLEAdapterState ToShared(this State state)
        {
            switch (state)
            {
                case State.Connected:
                case State.Connecting:
                case State.Disconnected:
                case State.Disconnecting:
                case State.On:
                    return BLEAdapterState.On;
                case State.Off:
                    return BLEAdapterState.Off;
                case State.TurningOn:
                    return BLEAdapterState.TurningOn;
                case State.TurningOff:
                    return BLEAdapterState.TurningOff;
                default:
                    return BLEAdapterState.Unknown;
            }
        }

        public static BLEErrorCode ToShared(this ScanFailure failure)
            => (BLEErrorCode)failure;

        public static Guid GetUUID(this BluetoothDevice device)
        {
            var mac = device.Address.Replace(":", string.Empty);
            var buffer = new byte[16];
            Enumerable
                .Range(0, mac.Length)
                .Where(x => x % 2 == 0)
                .Select(x => Convert.ToByte(mac.Substring(x, 2), 16))
                .ToArray()
                .CopyTo(buffer, 10);
            var uuid = new Guid(buffer);
            return uuid;
        }

        public static BLEDeviceState ToShared(this ProfileState state)
            => (BLEDeviceState)state;

        public static BLEErrorCode ToShared(this GattStatus status)
            => (BLEErrorCode)status;

        public static Guid ToShared(this UUID uuid)
            => uuid != null && Guid.TryParse(uuid.ToString(), out var result)
            ? result
            : Guid.Empty;

        public static BLECharacteristicProperty ToShared(this GattProperty properties)
            => (BLECharacteristicProperty)properties;

        public static ScanMode ToAndroid(this BLEScanMode mode)
            => (ScanMode)mode;
    }
}