using Foundation;
using System;
using UIKit;

namespace Additel.Core
{
    public static partial class Device
    {
        public static void StartTimer(TimeSpan interval, Func<bool> callback)
        {
            var timer = NSTimer.CreateRepeatingTimer(interval, t =>
            {
                if (callback())
                    return;

                t.Invalidate();
            });

            NSRunLoop.Main.AddTimer(timer, NSRunLoopMode.Common);
        }

        public static T InvokeOnMainThread<T>(Func<T> func)
        {
            var value = default(T);
            NSRunLoop.Main.InvokeOnMainThread(() => value = func.Invoke());

            return value;
        }

        public static void BeginInvokeOnMainThread(Action action)
        {
            NSRunLoop.Main.BeginInvokeOnMainThread(action.Invoke);
        }

        public static bool IsInvokeRequired => !NSThread.IsMain;
    }
}
