using CoreBluetooth;
using Foundation;
using System;

namespace Additel.BLE
{
    internal static class BLENativeExtensions
    {
        public static BLEAdapterState ToShared(this CBCentralManagerState state)
            => (BLEAdapterState)state;

        public static Guid ToShared(this NSUuid uuid)
            => Guid.Parse(uuid.Description);

        public static BLEDeviceState ToShared(this CBPeripheralState state)
            => (BLEDeviceState)state;

        public static BLEErrorCode ToShared(this NSError error)
            => (BLEErrorCode)(int)error.Code;

        public static Guid ToShared(this CBUUID uuid)
        {
            // 有时只返回标志位，因此需要补全
            var input = uuid.ToString();
            if (input.Length == 4)
                input = $"0000{input}-0000-1000-8000-00805F9B34FB";
            return Guid.Parse(input);
        }

        public static BLECharacteristicProperty ToShared(this CBCharacteristicProperties properties)
            => (BLECharacteristicProperty)properties;
    }
}