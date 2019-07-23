using Android.Bluetooth.LE;
using Android.Runtime;
using System;
using System.Collections.Generic;

namespace Additel.BLE
{
    internal class BLEScanCallback21 : ScanCallback
    {
        #region 事件

        public event EventHandler<BLEScanResultsEventArgs> BatchScanResults;
        public event EventHandler<BLEScanFailureEventArgs> ScanFailed;
        public event EventHandler<BLEScanResultEventArgs> ScanResult;
        #endregion

        #region ScanCallback

        public override void OnBatchScanResults(IList<ScanResult> results)
        {
            base.OnBatchScanResults(results);

            BatchScanResults?.Invoke(this, new BLEScanResultsEventArgs(results));
        }

        public override void OnScanFailed([GeneratedEnum] ScanFailure errorCode)
        {
            base.OnScanFailed(errorCode);

            ScanFailed?.Invoke(this, new BLEScanFailureEventArgs(errorCode));
        }

        public override void OnScanResult([GeneratedEnum] ScanCallbackType callbackType, ScanResult result)
        {
            base.OnScanResult(callbackType, result);

            ScanResult?.Invoke(this, new BLEScanResultEventArgs(callbackType, result));
        }
        #endregion
    }
}