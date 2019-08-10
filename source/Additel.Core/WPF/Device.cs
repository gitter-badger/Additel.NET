using System;
using System.Windows;
using System.Windows.Threading;

namespace Additel.Core
{
    public static partial class Device
    {
        public static void StartTimer(TimeSpan interval, Func<bool> callback)
        {
            var timer = new DispatcherTimer { Interval = interval };
            timer.Start();
            timer.Tick += (sender, args) =>
            {
                bool result = callback();
                if (!result)
                    timer.Stop();
            };
        }
        public static void BeginInvokeOnMainThread(Action action)
        {
            Application.Current.Dispatcher.BeginInvoke(action);
        }

        public static bool IsInvokeRequired => !Application.Current.Dispatcher.CheckAccess();
    }
}
