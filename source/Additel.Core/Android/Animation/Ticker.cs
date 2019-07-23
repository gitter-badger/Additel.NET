using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using System;

namespace Additel.Core.Animation
{
    internal class Ticker : BaseTicker, IDisposable
    {
        ValueAnimator _val;
        bool _systemEnabled;

        public Ticker()
        {
            _val = new ValueAnimator();
            _val.SetIntValues(0, 100); // avoid crash
            _val.RepeatCount = ValueAnimator.Infinite;
            _val.Update += OnValOnUpdate;
            CheckPowerSaveModeStatus();
        }

        public override bool SystemEnabled => _systemEnabled;

        internal void CheckPowerSaveModeStatus()
        {
            // Android disables animations when it's in power save mode
            // So we need to keep track of whether we're in that mode and handle animations accordingly
            // We can't just check ValueAnimator.AreAnimationsEnabled() because there's no event for that, and it's
            // only supported on API >= 26

            if (Build.VERSION.SdkInt < BuildVersionCodes.Lollipop)
            {
                _systemEnabled = true;
                return;
            }

            var powerManager = (PowerManager)Application.Context.GetSystemService(Context.PowerService);

            var powerSaveOn = powerManager.IsPowerSaveMode;

            // If power saver is active, then animations will not run
            _systemEnabled = !powerSaveOn;

            // Notify the ticker that this value has changed, so it can manage animations in progress
            OnSystemEnabledChanged();
        }

        public void Dispose()
        {
            if (_val != null)
            {
                _val.Update -= OnValOnUpdate;
                _val.Dispose();
            }
            _val = null;
        }

        protected override void DisableTimer()
        {
            if (Device.IsInvokeRequired)
            {
                Device.BeginInvokeOnMainThread(new Action(() =>
                {
                    _val?.Cancel();
                }));
            }
            else
            {
                _val?.Cancel();
            }
        }

        protected override void EnableTimer()
        {
            _val?.Start();
        }

        void OnValOnUpdate(object sender, ValueAnimator.AnimatorUpdateEventArgs e)
        {
            SendSignals();
        }
    }
}
