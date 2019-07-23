using System;
using Windows.ApplicationModel.Core;
using Windows.UI.Xaml.Media;

namespace Additel.Core.Animation
{
    internal class Ticker : BaseTicker
    {
        [ThreadStatic]
        private static BaseTicker s_ticker;

        protected override void DisableTimer()
        {
            CompositionTarget.Rendering -= RenderingFrameEventHandler;
        }

        protected override void EnableTimer()
        {
            CompositionTarget.Rendering += RenderingFrameEventHandler;
        }

        private void RenderingFrameEventHandler(object sender, object args)
        {
            SendSignals();
        }

        protected override BaseTicker GetTickerInstance()
        {
            if (CoreApplication.Views.Count > 1)
            {
                // We've got multiple windows open, we'll need to use the local ThreadStatic Ticker instead of the 
                // singleton in the base class 
                if (s_ticker == null)
                {
                    s_ticker = new Ticker();
                }

                return s_ticker;
            }

            return base.GetTickerInstance();
        }
    }
}