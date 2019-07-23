using System;
using System.Collections.Generic;

namespace Additel.BLE
{
    public class BLEServicesEventArgs : EventArgs
    {
        public IList<BLEService> Services { get; }

        public BLEServicesEventArgs(IList<BLEService> services)
        {
            Services = services;
        }
    }
}
