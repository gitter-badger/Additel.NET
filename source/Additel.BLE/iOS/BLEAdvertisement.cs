
using CoreBluetooth;
using Foundation;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Additel.BLE
{
    public partial class BLEAdvertisement
    {
        public static IList<BLEAdvertisement> Parse(NSDictionary value)
        {
            var records = new List<BLEAdvertisement>();

            foreach (var o in value.Keys)
            {
                var key = (NSString)o;
                if (key == CBAdvertisement.DataLocalNameKey)
                {
                    records.Add(new BLEAdvertisement(BLEAdvertisementType.CompleteLocalName,
                        NSData.FromString(value.ObjectForKey(key) as NSString).ToArray()));
                }
                else if (key == CBAdvertisement.DataManufacturerDataKey)
                {
                    var arr = ((NSData)value.ObjectForKey(key)).ToArray();
                    records.Add(new BLEAdvertisement(BLEAdvertisementType.ManufacturerSpecificData, arr));
                }
                else if (key == CBAdvertisement.DataServiceUUIDsKey)
                {
                    var array = (NSArray)value.ObjectForKey(key);

                    var dataList = new List<NSData>();
                    for (nuint i = 0; i < array.Count; i++)
                    {
                        var cbuuid = array.GetItem<CBUUID>(i);
                        dataList.Add(cbuuid.Data);
                    }
                    records.Add(new BLEAdvertisement(BLEAdvertisementType.UuidsComplete128Bit,
                        dataList.SelectMany(d => d.ToArray()).ToArray()));
                }
                else if (key == CBAdvertisement.DataTxPowerLevelKey)
                {
                    //iOS stores TxPower as NSNumber. Get int value of number and convert it into a signed Byte
                    //TxPower has a range from -100 to 20 which can fit into a single signed byte (-128 to 127)
                    sbyte byteValue = Convert.ToSByte(((NSNumber)value.ObjectForKey(key)).Int32Value);
                    //add our signed byte to a new byte array and return it (same parsed value as android returns)
                    byte[] arr = { (byte)byteValue };
                    records.Add(new BLEAdvertisement(BLEAdvertisementType.TxPowerLevel, arr));
                }
                else if (key == CBAdvertisement.DataServiceDataKey)
                {
                    //Service data from CoreBluetooth is returned as a key/value dictionary with the key being
                    //the service uuid (CBUUID) and the value being the NSData (bytes) of the service
                    //This is where you'll find eddystone and other service specific data
                    NSDictionary serviceDict = (NSDictionary)value.ObjectForKey(key);
                    //There can be multiple services returned in the dictionary, so loop through them
                    foreach (CBUUID dKey in serviceDict.Keys)
                    {
                        //Get the service key in bytes (from NSData)
                        byte[] keyAsData = dKey.Data.ToArray();

                        //Service UUID's are read backwards (little endian) according to specs, 
                        //CoreBluetooth returns the service UUIDs as Big Endian
                        //but to match the raw service data returned from Android we need to reverse it back
                        //Note haven't tested it yet on 128bit service UUID's, but should work
                        Array.Reverse(keyAsData);

                        //The service data under this key can just be turned into an arra
                        var data = (NSData)serviceDict.ObjectForKey(dKey);
                        byte[] valueAsData = data.Length > 0 ? data.ToArray() : new byte[0];

                        //Now we append the key and value data and return that so that our parsing matches the raw
                        //byte value returned from the Android library (which matches the raw bytes from the device)
                        byte[] arr = new byte[keyAsData.Length + valueAsData.Length];
                        Buffer.BlockCopy(keyAsData, 0, arr, 0, keyAsData.Length);
                        Buffer.BlockCopy(valueAsData, 0, arr, keyAsData.Length, valueAsData.Length);

                        records.Add(new BLEAdvertisement(BLEAdvertisementType.ServiceData, arr));
                    }
                }
                else if (key == CBAdvertisement.IsConnectable)
                {
                    // A Boolean value that indicates whether the advertising event type is connectable.
                    // The value for this key is an NSNumber object. You can use this value to determine whether a peripheral is connectable at a particular moment.
                    records.Add(new BLEAdvertisement(BLEAdvertisementType.IsConnectable, new byte[] { ((NSNumber)value.ObjectForKey(key)).ByteValue }));
                }
                else
                {
                    Debug.Write($"Parsing Advertisement: Ignoring Advertisement entry for key {key.ToString()}, since we don't know how to parse it yet. Maybe you can open a Pull Request and implement it :)");
                }
            }

            return records;
        }
    }
}