// TODO: 由于手势可能存在冲突(比如在 TabbedPage 中),所以去掉了边界限制,待解决

using Additel.Core;
using Additel.Core.Animation;
using SkiaSharp;
using System;

namespace Additel.SkiaViews
{
    public partial class SwitchView : CanvasView
    {
        public event EventHandler<ValueEventArgs<bool>> ValueChanged;

        private const uint TOGGLE_DURATION = 300;

        private SKColor _trackColor = Colors.GrayBackground;
        private double _trackPercentage;
        private double _thumbPercentage;
        private double _thumbExtension;
        private double _origin;
        private bool _touchValue;
        private bool _isHandled;
        //private bool _isFinished;

        private SKColor TrackColor
        {
            get => _trackColor;
            set
            {
                if (_trackColor == value)
                    return;

                _trackColor = value;

                InvalidateSurface();
            }
        }

        private double TrackPercentage
        {
            get => _trackPercentage;
            set
            {
                // 修正
                if (_trackPercentage < 0.0)
                    _trackPercentage = 0.0;
                else if (_trackPercentage > 1.0)
                    _trackPercentage = 1.0;

                if (_trackPercentage == value)
                    return;

                _trackPercentage = value;

                InvalidateSurface();
            }
        }

        private double ThumbPercentage
        {
            get => _thumbPercentage;
            set
            {
                // 修正
                if (_thumbPercentage < 0.0)
                    _thumbPercentage = 0.0;
                else if (_thumbPercentage > 1.0)
                    _thumbPercentage = 1.0;

                if (_thumbPercentage == value)
                    return;

                _thumbPercentage = value;

                InvalidateSurface();
            }
        }

        private double ThumbExtension
        {
            get => _thumbExtension;
            set
            {
                // 修正
                if (_thumbExtension < 0.0)
                    _thumbExtension = 0.0;

                var max = (CanvasSize.Width - CanvasSize.Height) / 3.0;

                if (_thumbExtension > max)
                    _thumbExtension = max;

                if (_thumbExtension == value)
                    return;

                _thumbExtension = value;

                InvalidateSurface();
            }
        }

        protected override void OnPaintSurface(SKSurface surface, SKImageInfo info)
        {
            base.OnPaintSurface(surface, info);

            var canvas = surface.Canvas;

            canvas.Clear();

            // 判断画布参数
            if (CanvasSize.Width <= 0 || CanvasSize.Height <= 0 || CanvasSize.Width <= CanvasSize.Height)
                return;

            // 边框宽度以窄边 ( 高度 ) 为参考
            var baseWidth = CanvasSize.Height / 15.0F;

            // 轨道
            //var strokeWidth = ToPixels((TRACK_HEIGHT / 2 - TRACK_STROKE_WIDTH) * TrackPercentage + TRACK_STROKE_WIDTH);
            //var trackWidth = ToPixels(TRACK_WIDTH) - strokeWidth;
            //var trackHeight = ToPixels(TRACK_HEIGHT) - strokeWidth;
            var strokeWidth = (CanvasSize.Height / 2 - baseWidth) * (float)TrackPercentage + baseWidth;
            var trackWidth = CanvasSize.Width - strokeWidth;
            var trackHeight = CanvasSize.Height - strokeWidth;
            var trackRadius = trackHeight / 2.0F;
            var trackLeft = (CanvasSize.Width - trackWidth) / 2.0F;
            var trackTop = (CanvasSize.Height - trackHeight) / 2.0F;

            // 滑块
            //var trackLength = ToPixels(TRACK_LENGTH - ThumbExtension);
            var trackLength = CanvasSize.Width - CanvasSize.Height - (float)ThumbExtension;
            var start = (CanvasSize.Width - trackLength) / 2.0F;
            var current = start + trackLength * (float)ThumbPercentage;

            //var thumbRadius = ToPixels(THUMB_RADIUS);
            var thumbRadius = CanvasSize.Height / 2.0F - baseWidth;
            var thumbHeight = thumbRadius * 2.0F;
            var thumbWidth = thumbHeight + (float)ThumbExtension;
            var thumbLeft = current - thumbWidth / 2.0F;
            var thumbTop = CanvasSize.Height / 2.0F - thumbRadius;
            var thumbSigma = CanvasSize.Height / 30.0F;
            var thumbOffset = CanvasSize.Height / 10.0F;

            // 遮罩
            var maskLeft = start - thumbRadius;
            var maskWidth = thumbHeight + trackLength * (float)ThumbPercentage;

            using (var paint = new SKPaint())
            {
                // 轨道
                paint.IsAntialias = true;
                paint.IsStroke = true;
                paint.Color = TrackColor;
                paint.StrokeWidth = strokeWidth;
                canvas.DrawRoundRect(trackLeft, trackTop, trackWidth, trackHeight, trackRadius, trackRadius, paint);

                // 遮罩
                paint.IsStroke = false;
                canvas.DrawRoundRect(maskLeft, thumbTop, maskWidth, thumbHeight, thumbRadius, thumbRadius, paint);

                // 阴影
                paint.Color = Colors.GrayShadow;
                paint.MaskFilter = SKMaskFilter.CreateBlur(SKBlurStyle.Normal, thumbSigma);
                //canvas.DrawCircle(center.X, center.Y, radius, paint);
                //canvas.DrawCircle(center.X, center.Y + offset, radius, paint);
                canvas.DrawRoundRect(thumbLeft, thumbTop, thumbWidth, thumbHeight, thumbRadius, thumbRadius, paint);
                canvas.DrawRoundRect(thumbLeft, thumbTop + thumbOffset, thumbWidth, thumbHeight, thumbRadius, thumbRadius, paint);

                // 滑块
                paint.MaskFilter.Dispose();
                paint.MaskFilter = null;
                paint.Color = SKColors.White;
                //canvas.DrawCircle(center.X, center.Y, radius, paint);
                canvas.DrawRoundRect(thumbLeft, thumbTop, thumbWidth, thumbHeight, thumbRadius, thumbRadius, paint);
            }
        }

        protected override bool OnTouch(long id, SkTouchAction action, SKPoint location)
        {
            switch (action)
            {
                case SkTouchAction.Pressed:
                    {
                        _origin = location.X;
                        _touchValue = Value;
                        //_isHandled = _isFinished = false;
                        _isHandled = false;

                        if (!_touchValue)
                            FillTrack();

                        ExtendMaskThumb();
                        break;
                    }
                case SkTouchAction.Moved:
                    {
                        //if (_isFinished)
                        //    break;

                        var current = location.X;
                        var distance = current - _origin;
                        var max = (CanvasSize.Width - CanvasSize.Height) / 2.0F;
                        if (!_touchValue && distance > max)
                        {
                            _touchValue = true;

                            TrackColor = OnColor.ToSKColor();
                            ForwardThumb();

                            _origin = current;

                            if (!_isHandled)
                                _isHandled = true;
                        }
                        else if (_touchValue && -distance > max)
                        {
                            _touchValue = false;

                            TrackColor = Colors.GrayBackground;
                            BackwardThumb();

                            _origin = current;

                            if (!_isHandled)
                                _isHandled = true;
                        }

                        //var bounds = new SKRect(0.0F, 0.0F, CanvasSize.Width, CanvasSize.Height);
                        //bounds.Inflate(CanvasSize.Height, CanvasSize.Height);
                        //if (!bounds.Contains(location))
                        //{
                        //    _isFinished = true;

                        //    if (Value != _touchValue)
                        //        Value = _touchValue;

                        //    if (!Value)
                        //        EmptyTrack();

                        //    ShortenMaskThumb();
                        //}
                        break;
                    }
                case SkTouchAction.Released:
                    {
                        //if (_isFinished)
                        //    break;

                        if (Value != _touchValue)
                            Value = _touchValue;
                        else if (!_isHandled)
                            Value = !Value;

                        if (!Value)
                            EmptyTrack();

                        ShortenMaskThumb();
                        break;
                    }
                case SkTouchAction.Cancelled:
                    {
                        break;
                    }
                default:
                    {
                        break;
                    }
            }

            // 返回 true, 以获取后续事件
            return true;
        }

        private void UpdateValue()
        {
            if (Value)
            {
                TrackColor = OnColor.ToSKColor();
                FillTrack();
                ForwardThumb();
            }
            else
            {
                TrackColor = Colors.GrayBackground;
                EmptyTrack();
                BackwardThumb();
            }

            ValueChanged?.Invoke(this, new ValueEventArgs<bool>(Value));
        }

        private void UpdateOnColor()
        {
            if (!Value)
                return;

            TrackColor = OnColor.ToSKColor();
        }

        private void FillTrack()
        {
            if (this.AnimationIsRunning("EMPTY_TRACK"))
                this.AbortAnimation("EMPTY_TRACK");

            var start = TrackPercentage;
            var end = 1.0;
            var length = TOGGLE_DURATION - (uint)(TOGGLE_DURATION * TrackPercentage / 1.0);

            if (TrackPercentage == end || this.AnimationIsRunning("FILL_TRACK"))
                return;

            this.Animate("FILL_TRACK", value => TrackPercentage = value, start, end, length: length, easing: Easing.SinOut);
        }

        private void EmptyTrack()
        {
            if (this.AnimationIsRunning("FILL_TRACK"))
                this.AbortAnimation("FILL_TRACK");

            var start = TrackPercentage;
            var end = 0.0;
            var length = (uint)(TOGGLE_DURATION * TrackPercentage / 1.0);

            if (TrackPercentage == end || this.AnimationIsRunning("EMPTY_TRACK"))
                return;

            this.Animate("EMPTY_TRACK", value => TrackPercentage = value, start, end, length: length, easing: Easing.SinOut);
        }

        private void ExtendMaskThumb()
        {
            if (this.AnimationIsRunning("SHORTEN_MASK_THUMB"))
                this.AbortAnimation("SHORTEN_MASK_THUMB");

            var max = (CanvasSize.Width - CanvasSize.Height) / 3.0;
            var start = ThumbExtension;
            var end = max;
            var length = TOGGLE_DURATION - (uint)(TOGGLE_DURATION * ThumbExtension / max);

            if (ThumbExtension == end || this.AnimationIsRunning("EXTEND_MASK_THUMB"))
                return;

            this.Animate("EXTEND_MASK_THUMB", value => ThumbExtension = value, start, end, length: length);
        }

        private void ShortenMaskThumb()
        {
            if (this.AnimationIsRunning("EXTEND_MASK_THUMB"))
                this.AbortAnimation("EXTEND_MASK_THUMB");

            var max = (CanvasSize.Width - CanvasSize.Height) / 3.0;
            var start = ThumbExtension;
            var end = 0.0;
            var length = (uint)(TOGGLE_DURATION * ThumbExtension / max);

            if (ThumbExtension == end || this.AnimationIsRunning("SHORTEN_MASK_THUMB"))
                return;

            this.Animate("SHORTEN_MASK_THUMB", value => ThumbExtension = value, start, end, length: length);
        }

        private void ForwardThumb()
        {
            if (this.AnimationIsRunning("BACKWARD_MASK_THUMB"))
                this.AbortAnimation("BACKWARD_MASK_THUMB");

            var start = ThumbPercentage;
            var end = 1.0;
            var length = TOGGLE_DURATION - (uint)(TOGGLE_DURATION * ThumbPercentage / 1.0);

            if (ThumbPercentage == end || this.AnimationIsRunning("FORWARD_MASK_THUMB"))
                return;

            this.Animate("FORWARD_MASK_THUMB", value => ThumbPercentage = value, start, end, length: length, easing: Easing.SpringOut);
        }

        private void BackwardThumb()
        {
            if (this.AnimationIsRunning("FORWARD_MASK_THUMB"))
                this.AbortAnimation("FORWARD_MASK_THUMB");

            var start = ThumbPercentage;
            var end = 0.0;
            var length = (uint)(TOGGLE_DURATION * ThumbPercentage / 1.0);

            if (ThumbPercentage == end || this.AnimationIsRunning("BACKWARD_MASK_THUMB"))
                return;

            this.Animate("BACKWARD_MASK_THUMB", value => ThumbPercentage = value, start, end, length: length, easing: Easing.SpringOut);
        }
    }
}
