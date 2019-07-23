using Foundation;
using System;

namespace Additel.Core
{
    public static partial class Device
    {
        public static void BeginInvokeOnMainThread(Action action)
        {
            NSRunLoop.Main.BeginInvokeOnMainThread(action.Invoke);
        }

        public static bool IsInvokeRequired => !NSThread.IsMain;
    }
}
