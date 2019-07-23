using Android.OS;
using System;

namespace Additel.Core
{
    public static partial class Device
    {
        static Handler s_handler;

        public static void BeginInvokeOnMainThread(Action action)
        {
            if (s_handler == null || s_handler.Looper != Looper.MainLooper)
            {
                s_handler = new Handler(Looper.MainLooper);
            }

            s_handler.Post(action);
        }

        public static bool IsInvokeRequired
        {
            get
            {
                return Looper.MainLooper != Looper.MyLooper();
            }
        }
    }
}
