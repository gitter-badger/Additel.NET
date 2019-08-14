using Additel.Core.Animation;
using SkiaSharp;
using System.ComponentModel;

namespace Additel.SkiaViews
{
    public partial class CanvasView : IAnimatable
    {
        public ViewState State { get; private set; }

        protected virtual void OnLoaded()
        {
            State = ViewState.Loaded;
        }

        protected virtual void OnUnloaded()
        {
            State = ViewState.Unloaded;
        }

        protected virtual void OnDispose()
        {
            State = ViewState.Disposed;
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
