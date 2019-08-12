using SkiaSharp;
using System.Diagnostics;
using Additel.Core;
using System;
using System.Threading.Tasks;

namespace Additel.SkiaViews
{
    public partial class GIFView : CanvasView
    {
        private readonly Stopwatch _watcher = new Stopwatch();

        private SKBitmap[] _images;
        private int[] _durations;
        private int[] _accumulations;
        private int _total;
        private bool _isAnimating;
        private int _current;
        private string _source;
        private SKStretch _stretch;

        public string Source
        {
            get => _source;
            set
            {
                if (_source == value)
                    return;

                _source = value;
                UpdateSource();
            }
        }

        private async void UpdateSource()
        {
            // 重置字段
            if (_images != null)
            {
                foreach (var image in _images)
                {
                    image.Dispose();
                }
                _images = null;
                _durations = null;
                _accumulations = null;
                _total = 0;
                _current = 0;
            }
            // 更新字段
            // TODO: 考虑是否有必要添加缓存
            SKBitmap[] images = null;
            int[] durations = null;
            int[] accumulations = null;
            int total = 0;
            using (var stream = GetSourceStream())
            {
                if (stream != null)
                {
                    // 异步加载 GIF，防止卡 UI 线程
                    await Task.Run(() =>
                    {
                        using (var codec = SKCodec.Create(stream))
                        {
                            // 获取图片帧数并分配
                            var count = codec.FrameCount;
                            images = new SKBitmap[count];
                            durations = new int[count];
                            accumulations = new int[count];
                            //codec.RepetitionCount
                            // 循环图片帧
                            for (int i = 0; i < count; i++)
                            {
                                // 获取每帧的时长
                                var duration = codec.FrameInfo[i].Duration;
                                durations[i] = duration;
                                // 计算总时长
                                total += duration;
                                // 计算累加时长
                                var former = i == 0 ? 0 : accumulations[i - 1];
                                accumulations[i] = duration + former;
                                // 创建图片
                                var info = new SKImageInfo(codec.Info.Width, codec.Info.Height);
                                images[i] = new SKBitmap(info);
                                // 获取像素地址
                                var address = images[i].GetPixels();
                                // 创建 SKCodecOptions 描述图片帧
                                var options = new SKCodecOptions(i);
                                // 将像素复制到图片中
                                codec.GetPixels(info, address, options);
                            }
                        }
                    });
                }
            }
            // 重绘界面
            if (_isAnimating)
            {
                if (images == null)
                {
                    StopGIF();
                }
                else
                {
                    _images = images;
                    _durations = durations;
                    _accumulations = accumulations;
                    _total = total;

                    InvalidateSurface();
                }
            }
            else
            {
                if (images == null)
                    return;

                switch (State)
                {
                    case LoadState.Loading:
                        {
                            _images = images;
                            _durations = durations;
                            _accumulations = accumulations;
                            _total = total;
                        }
                        break;
                    case LoadState.Loaded:
                        {
                            _images = images;
                            _durations = durations;
                            _accumulations = accumulations;
                            _total = total;

                            PlayGIF();
                        }
                        break;
                    case LoadState.Unloaded:
                        {
                            foreach (var image in images)
                            {
                                image.Dispose();
                            }
                            images = null;
                            break;
                        }
                }
            }
        }

        private void PlayGIF()
        {
            _isAnimating = true;
            _watcher.Start();
            Device.StartTimer(TimeSpan.FromMilliseconds(16), OnTimerTick);
        }

        private void StopGIF()
        {
            _watcher.Stop();
            _isAnimating = false;
        }

        public SKStretch Stretch
        {
            get => _stretch;
            set
            {
                if (_stretch == value)
                    return;

                _stretch = value;
                UpdateStretch();
            }
        }

        private void UpdateStretch()
        {
            if (!_isAnimating)
                return;

            InvalidateSurface();
        }

        protected override void OnPaintSurface(SKSurface surface, SKImageInfo info)
        {
            base.OnPaintSurface(surface, info);

            if (_images == null)
                return;

            var canvas = surface.Canvas;
            canvas.Clear();

            var image = _images[_current];
            canvas.DrawBitmap(image, info.Rect, Stretch);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (_isAnimating || _images == null)
                return;

            PlayGIF();
        }

        protected override void OnUnloaded()
        {
            base.OnUnloaded();

            if (!_isAnimating)
                return;

            StopGIF();
        }

        protected override void OnDispose()
        {
            base.OnDispose();

            if (_images == null)
                return;

            foreach (var image in _images)
            {
                image.Dispose();
            }
            _images = null;
        }

        private bool OnTimerTick()
        {
            if (_isAnimating)
            {
                var msec = (int)(_watcher.ElapsedMilliseconds % _total);

                // Find the frame based on the elapsed time
                int frame;
                for (frame = 0; frame < _accumulations.Length; frame++)
                {
                    if (msec < _accumulations[frame])
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
    }
}
