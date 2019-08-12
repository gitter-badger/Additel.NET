using Additel.Core.Animation;
using SkiaSharp;
using System.ComponentModel;

namespace Additel.SkiaViews
{
    public partial class CanvasView : IAnimatable
    {
        public LoadState State { get; protected set; }

        protected virtual void OnLoaded()
        {
            State = LoadState.Loaded;
        }

        protected virtual void OnUnloaded()
        {
            State = LoadState.Unloaded;
        }

        protected virtual void OnDispose()
        {

        }

        protected virtual void OnPaintSurface(SKSurface surface, SKImageInfo info)
        {

        }

        protected virtual bool OnTouch(long id, SkTouchAction action, SKPoint location)
        {
            return false;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void BatchBegin()
        {
            // 暂时无需处理
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public void BatchCommit()
        {
            // 暂时无需处理
        }
    }
}
