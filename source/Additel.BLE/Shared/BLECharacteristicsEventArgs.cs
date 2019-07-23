using System;
using System.Collections.Generic;

namespace Additel.BLE
{
    public class BLECharacteristicsEventArgs : EventArgs
    {
        public IList<BLECharacteristic> Characteristics { get; }

        public BLECharacteristicsEventArgs(IList<BLECharacteristic> characteristics)
        {
            Characteristics = characteristics;
        }
    }
}
