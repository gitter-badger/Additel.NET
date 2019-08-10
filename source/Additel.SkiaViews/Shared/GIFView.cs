using SkiaSharp;
using System.Diagnostics;
using Additel.Core;
using System;

#if __ANDROID__
using Android.Content;
#endif

namespace Additel.SkiaViews
{
    public partial class GIFView : CanvasView
    {
        private readonly SKBitmap[] _images;
        private readonly int[] _durations;
        private readonly int[] _accumulation;
        private readonly int _total;
        private readonly Stopwatch _watcher = new Stopwatch();

        private bool _isAnimating;
        private int _current;

        public string Source { get; set; } = "newton.gif";

        protected override void OnPaintSurface(SKSurface surface, SKImageInfo info)
        {
            base.OnPaintSurface(surface, info);

            if (_images == null)
                return;

            var canvas = surface.Canvas;
            canvas.Clear();

            var image = _images[_current];
            canvas.DrawBitmap(image, info.Rect, SKStretch.Uniform);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (_images == null)
                return;

            _isAnimating = true;
            _watcher.Start();
            Device.StartTimer(TimeSpan.FromMilliseconds(16), OnTimerTick);
        }

        protected override void OnUnloaded()
        {
            base.OnUnloaded();

            if (_images == null)
                return;

            _watcher.Stop();
            _isAnimating = false;

            foreach (var image in _images)
            {
                image.Dispose();
            }
        }

        private bool OnTimerTick()
        {
            if (_isAnimating)
            {
                var msec = (int)(_watcher.ElapsedMilliseconds % _total);

                // Find the frame based on the elapsed time
                int frame;
                for (frame = 0; frame < _accumulation.Length; frame++)
                {
                    if (msec < _accumulation[frame])
                        break;
                }

                // Save in a field and invalidate the SKCanvasView.
                if (_current != frame)
                {
                    // 在主线程中重绘，防止由于对象被释放引发异常
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        if (!_isAnimating)
                            return;

                        _current = frame;
                        InvalidateSurface();
                    });
                }
            }

            return _isAnimating;
        }

#if __ANDROID__
        public GIFView(Context context)
           : base(context)
#else
        public GIFView()
#endif
        {
            if (string.IsNullOrWhiteSpace(Source))
                return;

            using (var stream = GetSourceStream())
            {
                if (stream == null)
                    return;

                using (var codec = SKCodec.Create(stream))
                {
                    // 获取图片帧数并分配
                    var count = codec.FrameCount;
                    _images = new SKBitmap[count];
                    _durations = new int[count];
                    _accumulation = new int[count];
                    //codec.RepetitionCount
                    // 循环图片帧
                    for (int i = 0; i < count; i++)
                    {
                        // 获取每帧的时长
                        var duration = codec.FrameInfo[i].Duration;
                        _durations[i] = duration;
                        // 计算总时长
                        _total += duration;
                        // 计算累加时长
                        var former = i == 0 ? 0 : _accumulation[i - 1];
                        _accumulation[i] = duration + former;
                        // 创建图片
                        var info = new SKImageInfo(codec.Info.Width, codec.Info.Height);
                        _images[i] = new SKBitmap(info);
                        // 获取像素地址
                        var address = _images[i].GetPixels();
                        // 创建 SKCodecOptions 描述图片帧
                        var options = new SKCodecOptions(i);
                        // 将像素复制到图片中
                        codec.GetPixels(info, address, options);
                    }
                }
            }
        }
    }
}
