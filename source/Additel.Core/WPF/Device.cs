using System;
using System.Windows;

namespace Additel.Core
{
    public static partial class Device
    {
        public static void BeginInvokeOnMainThread(Action action)
        {
            Application.Current.Dispatcher.BeginInvoke(action);
        }

        public static bool IsInvokeRequired => !Application.Current.Dispatcher.CheckAccess();
    }
}
