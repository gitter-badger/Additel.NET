using Additel.Core;

namespace Additel.BLE
{
    public partial class BLEAdvertisement
    {
        public BLEAdvertisementType Type { get; }
        public byte[] Data { get; }

        public BLEAdvertisement(BLEAdvertisementType type, byte[] data)
        {
            Type = type;
            Data = data;
        }

        public override string ToString()
            => $"Advertisement: [Type {Type}; Data {Data.GetHexString()}]";
    }
}
