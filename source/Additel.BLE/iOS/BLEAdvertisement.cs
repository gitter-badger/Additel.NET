
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
            var advertisements = new List<BLEAdvertisement>();

            foreach (var o in value.Keys)
            {
                var key = (NSString)o;
                if (key == CBAdvertisement.DataLocalNameKey)
                {
                    var data = NSData.FromString((NSString)value.ObjectForKey(key)).ToArray();
                    var item = new BLEAdvertisement(BLEAdvertisementType.CompleteLocalName, data);
                    advertisements.Add(item);
                }
                else if (key == CBAdvertisement.DataManufacturerDataKey)
                {
                    var data = ((NSData)value.ObjectForKey(key)).ToArray();
                    var item = new BLEAdvertisement(BLEAdvertisementType.ManufacturerSpecificData, data);
                    advertisements.Add(item);
                }
                else if (key == CBAdvertisement.DataServiceUUIDsKey)
                {
                    var array = (NSArray)value.ObjectForKey(key);
                    var datas = new List<NSData>();
                    for (nuint i = 0; i < array.Count; i++)
                    {
                        var uuid = array.GetItem<CBUUID>(i);
                        datas.Add(uuid.Data);
                    }
                    var data = datas.SelectMany(d => d.ToArray()).ToArray();
                    var item = new BLEAdvertisement(BLEAdvertisementType.CompleteServiceUUIDs128Bit, data);
                    advertisements.Add(item);
                }
                else if (key == CBAdvertisement.DataTxPowerLevelKey)
                {
                    //iOS stores TxPower as NSNumber. Get int value of number and convert it into a signed Byte
                    //TxPower has a range from -100 to 20 which can fit into a single signed byte (-128 to 127)
                    var byteValue = Convert.ToSByte(((NSNumber)value.ObjectForKey(key)).Int32Value);
                    //add our signed byte to a new byte array and return it (same parsed value as android returns)
                    var data = new byte[] { (byte)byteValue };
                    var item = new BLEAdvertisement(BLEAdvertisementType.TxPowerLevel, data);
                    advertisements.Add(item);
                }
                else if (key == CBAdvertisement.DataServiceDataKey)
                {
                    //Service data from CoreBluetooth is returned as a key/value dictionary with the key being
                    //the service uuid (CBUUID) and the value being the NSData (bytes) of the service
                    //This is where you'll find eddystone and other service specific data
                    var datas = (NSDictionary)value.ObjectForKey(key);
                    //There can be multiple services returned in the dictionary, so loop through them
                    foreach (CBUUID dataKey in datas.Keys)
                    {
                        //Get the service key in bytes (from NSData)
                        var keyData = dataKey.Data.ToArray();

                        //Service UUID's are read backwards (little endian) according to specs, 
                        //CoreBluetooth returns the service UUIDs as Big Endian
                        //but to match the raw service data returned from Android we need to reverse it back
                        //Note haven't tested it yet on 128bit service UUID's, but should work
                        Array.Reverse(keyData);

                        //The service data under this key can just be turned into an arra
                        var dataValue = (NSData)datas.ObjectForKey(dataKey);
                        var valueData = dataValue.Length > 0 ? dataValue.ToArray() : new byte[0];

                        //Now we append the key and value data and return that so that our parsing matches the raw
                        //byte value returned from the Android library (which matches the raw bytes from the device)
                        var data = new byte[keyData.Length + valueData.Length];
                        Buffer.BlockCopy(keyData, 0, data, 0, keyData.Length);
                        Buffer.BlockCopy(valueData, 0, data, keyData.Length, valueData.Length);

                        var item = new BLEAdvertisement(BLEAdvertisementType.ServiceData, data);
                        advertisements.Add(item);
                    }
                }
                else if (key == CBAdvertisement.IsConnectable)
                {
                    // A Boolean value that indicates whether the advertising event type is connectable.
                    // The value for this key is an NSNumber object. You can use this value to determine whether a peripheral is connectable at a particular moment.
                    var byteValue = ((NSNumber)value.ObjectForKey(key)).ByteValue;
                    var data = new byte[] { byteValue };
                    var item = new BLEAdvertisement(BLEAdvertisementType.IsConnectable, data);
                    advertisements.Add(item);
                }
                else
                {
                    Debug.Write($"Parsing Advertisement: Ignoring Advertisement entry for key {key.ToString()}, since we don't know how to parse it yet. Maybe you can open a Pull Request and implement it :)");
                }
            }

            return advertisements;
        }
    }
}