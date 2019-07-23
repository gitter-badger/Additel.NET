using System;
using System.Windows.Threading;

namespace Additel.Core.Animation
{
    internal class Ticker : BaseTicker
    {
        readonly DispatcherTimer _timer;

        public Ticker()
        {
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(15) };
            _timer.Tick += (sender, args) => SendSignals();
        }

        protected override void DisableTimer()
        {
            _timer.Stop();
        }

        protected override void EnableTimer()
        {
            _timer.Start();
        }
    }
}