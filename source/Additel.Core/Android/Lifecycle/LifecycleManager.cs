using Android.App;
using Android.Content.PM;
using Android.OS;
using System;

namespace Additel.Core.Lifecycle
{
    public static partial class LifecycleManager
    {
        #region 事件

        public static event EventHandler Resumed;
        public static event EventHandler Paused;
        public static event EventHandler<PermissionsEventArgs> PermissionsRequested;
        #endregion

        #region 字段

        /// <summary>
        ///  使用弱引用 <see cref="WeakReference{T}"/> ，防止内存泄漏
        /// </summary>
        private static WeakReference<Activity> _currentActivity;
        #endregion

        #region 属性

        public static Activity CurrentActivity
        {
            get
            {
                if (_currentActivity == null)
                    throw new InvalidOperationException("未在 MainActivity 中调用 Init 初始化方法");

                return _currentActivity.TryGetTarget(out var a) ? a : null;
            }
        }
        #endregion

        #region 方法

        public static void Init(Activity activity, Bundle bundle)
        {
            _currentActivity = new WeakReference<Activity>(activity);

            var callback = new ActivityLifecycleCallbacks();
            callback.ActivityPaused += OnActivityPaused;
            callback.ActivityResumed += OnActivityResumed;

            activity.Application.RegisterActivityLifecycleCallbacks(callback);
        }

        private static void OnActivityResumed(object sender, ActivityEventArgs e)
        {
            _currentActivity.SetTarget(e.Activity);

            Resumed?.Invoke(null, EventArgs.Empty);
        }

        private static void OnActivityPaused(object sender, ActivityEventArgs e)
        {
            Paused?.Invoke(null, EventArgs.Empty);
        }

        public static void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
            => PermissionsRequested?.Invoke(null, new PermissionsEventArgs(requestCode, permissions, grantResults));
        #endregion
    }
}
