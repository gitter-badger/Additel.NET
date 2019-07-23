using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace Additel.BLE
{
    public partial class BLEAdvertisement
    {
        public static IList<BLEAdvertisement> Parse(byte[] value)
        {
            var records = new List<BLEAdvertisement>();

            var index = 0;
            while (index < value?.Length)
            {
                var length = value[index++];
                // Done once we run out of records 
                // 1 byte for type and length-1 bytes for data
                if (length == 0)
                    break;
                var type = (BLEAdvertisementType)value[index];
                // Done if our record isn't a valid type
                if (type == 0)
                    break;
                // Advertisment record type not defined:
                if (!Enum.IsDefined(typeof(BLEAdvertisementType), type))
                {
                    Debug.WriteLine($"Advertisment record type not defined: {type}");
                    break;
                }

                //data length is length -1 because type takes the first byte
                var data = new byte[length - 1];
                Array.Copy(value, index + 1, data, 0, length - 1);

                // don't forget that data is little endian so reverse
                // Supplement to Bluetooth Core Specification 1
                // NOTE: all relevant devices are already little endian, so this is not necessary for any type except UUIDs
                switch (type)
                {
                    case BLEAdvertisementType.ServiceDataUuid32Bit:
                    case BLEAdvertisementType.SsUuids128Bit:
                    case BLEAdvertisementType.SsUuids16Bit:
                    case BLEAdvertisementType.SsUuids32Bit:
                    case BLEAdvertisementType.UuidCom32Bit:
                    case BLEAdvertisementType.UuidsComplete128Bit:
                    case BLEAdvertisementType.UuidsComplete16Bit:
                    case BLEAdvertisementType.UuidsIncomple16Bit:
                    case BLEAdvertisementType.UuidsIncomplete128Bit:
                        Array.Reverse(data);
                        break;
                }
                var record = new BLEAdvertisement(type, data);
                Debug.WriteLine(record.ToString());
                records.Add(record);
                // Advance
                index += length;
            }

            return records;
        }
    }
}