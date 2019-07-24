using Android.App;
using System;

namespace Additel.Core.Lifecycle
{
    internal class ActivityEventArgs : EventArgs
    {
        public Activity Activity { get; }

        public ActivityEventArgs(Activity activity)
        {
            Activity = activity;
        }
    }
}
