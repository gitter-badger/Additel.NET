using Additel.Core.Animation;
using SkiaSharp;
using System.ComponentModel;

namespace Additel.Controls
{
    public partial class SKCanvasView : IAnimatable
    {
        protected virtual void OnPaintSurface(SKSurface surface, SKImageInfo info)
        {

        }

        protected virtual bool OnTouch(long id, SKTouchAction action, SKPoint location)
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
