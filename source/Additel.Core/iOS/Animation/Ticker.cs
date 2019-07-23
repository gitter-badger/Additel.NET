using CoreAnimation;
using Foundation;
using System;
using System.Collections.Concurrent;
using System.Threading;
using UIKit;

namespace Additel.Core.Animation
{
    internal class Ticker : BaseTicker
    {
        readonly BlockingCollection<Action> _queue = new BlockingCollection<Action>();
        CADisplayLink _link;

        public Ticker()
        {
            var thread = new Thread(StartThread);
            thread.Start();
        }

        protected override void DisableTimer()
        {
            if (_link != null)
            {
                _link.RemoveFromRunLoop(NSRunLoop.Current, NSRunLoop.NSRunLoopCommonModes);
                _link.Dispose();
            }
            _link = null;
        }

        protected override void EnableTimer()
        {
            _link = CADisplayLink.Create(() => SendSignals());
            _link.AddToRunLoop(NSRunLoop.Current, NSRunLoop.NSRunLoopCommonModes);
        }

        void StartThread()
        {
            while (true)
            {
                var action = _queue.Take();
                var previous = UIApplication.CheckForIllegalCrossThreadCalls;
                UIApplication.CheckForIllegalCrossThreadCalls = false;

                CATransaction.Begin();
                action.Invoke();

                while (_queue.TryTake(out action))
                    action.Invoke();
                CATransaction.Commit();

                UIApplication.CheckForIllegalCrossThreadCalls = previous;
            }
        }
    }
}
