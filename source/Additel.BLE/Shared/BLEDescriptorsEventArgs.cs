using System;
using System.Collections.Generic;

namespace Additel.BLE
{
    public class BLEDescriptorsEventArgs : EventArgs
    {
        public IList<BLEDescriptor> Descriptors { get; }

        public BLEDescriptorsEventArgs(IList<BLEDescriptor> descriptors)
        {
            Descriptors = descriptors;
        }
    }
}
