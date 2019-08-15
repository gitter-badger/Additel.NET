using SkiaSharp;
using System.Diagnostics;
using Additel.Core;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace Additel.SkiaViews
{
    public partial class GIFView : CanvasView
    {
        /// <summary>
        /// 全局 GIF 资源缓存
        /// </summary>
        private static readonly IDictionary<string, GIFCache> _caches = new Dictionary<string, GIFCache>();
        private static readonly SemaphoreSlim _slim = new SemaphoreSlim(1, 1);

        private readonly Stopwatch _watcher = new Stopwatch();

        private GIFCache _cache;
        private bool _isAnimating;
        private int _current;

        private async void UpdateSource(string oldValue, string newValue)
        {
            // 重置字段
            if (_cache != null)
            {
                _cache.Count--;
                _cache = null;
                _current = 0;
                // 尝试清理之前的缓存
                TryRemoveSourceCache(oldValue);
            }
            // 更新字段
            GIFCache cache = null;
            try
            {
                await _slim.WaitAsync();

                if (!string.IsNullOrWhiteSpace(newValue) && !_caches.TryGetValue(newValue, out cache))
                {
                    var name = newValue.TrimEnd(".gif", StringComparison.OrdinalIgnoreCase);
                    // 获取资源
                    using (var stream = await Storage.GetResourceStreamAsync(name, "gif"))
                    {
                        if (stream != null)
                        {
                            // 异步解码 GIF，防止卡 UI 线程
                            await Task.Run(() =>
                            {
                                using (var codec = SKCodec.Create(stream))
                                {
                                    // 获取图片帧数并分配
                                    var count = codec.FrameCount;
                                    //codec.RepetitionCount
                                    var images = new SKBitmap[count];
                                    var accumulations = new int[count];
                                    var total = 0;
                                    // 循环图片帧
                                    for (int i = 0; i < count; i++)
                                    {
                                        // 获取每帧的时长
                                        var duration = codec.FrameInfo[i].Duration;
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
                                    cache = new GIFCache(images, accumulations, total);
                                }
                            });
                        }
                    }
                    // 加入缓存
                    _caches.Add(newValue, cache);
                }
            }
            finally
            {
                _slim.Release();
            }
            // 重绘界面
            if (_isAnimating)
            {
                if (cache == null)
                {
                    StopGIF();
                }
                else
                {
                    _cache = cache;
                    _cache.Count++;

                    InvalidateSurface();
                }
            }
            else
            {
                if (cache == null)
                    return;

                switch (State)
                {
                    case ViewState.Unloaded:
                        {
                            _cache = cache;
                            _cache.Count++;
                        }
                        break;
                    case ViewState.Loaded:
                        {
                            _cache = cache;
                            _cache.Count++;

                            PlayGIF();
                        }
                        break;
                    case ViewState.Disposed:
                        {
                            TryRemoveSourceCache(newValue);
                            break;
                        }
                }
            }
        }

        private void TryRemoveSourceCache(string source)
        {
            if (string.IsNullOrWhiteSpace(source) ||
                !_caches.TryGetValue(source, out var cache) ||
                cache.Count > 0)
                return;

            _caches.Remove(source);

            cache.Dispose();
        }

        private void UpdateStretch()
        {
            if (!_isAnimating)
                return;

            InvalidateSurface();
        }

        private void PlayGIF()
        {
            _isAnimating = true;
            _watcher.Start();
            Device.StartTimer(TimeSpan.FromMilliseconds(16), OnTimerTick);
        }

        private bool OnTimerTick()
        {
            if (_isAnimating && _cache != null)
            {
                var msec = (int)(_watcher.ElapsedMilliseconds % _cache.Total);

                // Find the frame based on the elapsed time
                int frame;
                for (frame = 0; frame < _cache.Accumulations.Length; frame++)
                {
                    if (msec < _cache.Accumulations[frame])
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

        private void StopGIF()
        {
            _watcher.Stop();
            _isAnimating = false;
        }

        protected override void OnPaintSurface(SKSurface surface, SKImageInfo info)
        {
            base.OnPaintSurface(surface, info);

            if (_cache == null)
                return;

            var canvas = surface.Canvas;
            canvas.Clear();

            var image = _cache.Images[_current];
            canvas.DrawBitmap(image, info.Rect, Stretch);
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();

            if (_isAnimating || _cache == null)
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
            if (_cache != null)
            {
                _cache.Count--;
                _cache = null;
                TryRemoveSourceCache(Source);
            }

            base.OnDispose();
        }
    }
}
