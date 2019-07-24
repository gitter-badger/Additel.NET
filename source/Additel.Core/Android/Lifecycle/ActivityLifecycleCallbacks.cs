using Android.App;
using Android.OS;
using System;
using JavaObject = Java.Lang.Object;

namespace Additel.Core.Lifecycle
{
    internal class ActivityLifecycleCallbacks : JavaObject, Application.IActivityLifecycleCallbacks
    {
        #region 事件

        public event EventHandler<ActivityEventArgs> ActivityPaused;
        public event EventHandler<ActivityEventArgs> ActivityResumed;
        #endregion

        #region Application.IActivityLifecycleCallbacks

        public void OnActivityCreated(Activity activity, Bundle savedInstanceState)
        {

        }

        public void OnActivityDestroyed(Activity activity)
        {

        }

        public void OnActivityPaused(Activity activity)
        {
            ActivityPaused?.Invoke(this, new ActivityEventArgs(activity));
        }

        public void OnActivityResumed(Activity activity)
        {
            ActivityResumed?.Invoke(this, new ActivityEventArgs(activity));
        }

        public void OnActivitySaveInstanceState(Activity activity, Bundle outState)
        {

        }

        public void OnActivityStarted(Activity activity)
        {

        }

        public void OnActivityStopped(Activity activity)
        {

        }
        #endregion
    }
}