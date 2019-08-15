using Additel.Core;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.UI.Xaml;

namespace Additel.SkiaViews
{
    partial class GIFView
    {
        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for <see cref="Source"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty SourceProperty =
            DependencyProperty.Register(
                nameof(Source),
                typeof(string),
                typeof(GIFView),
                new PropertyMetadata(null, OnSourceChanged));

        private static void OnSourceChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (GIFView)d;
            var oldValue = (string)e.OldValue;
            var newValue = (string)e.NewValue;

            view.UpdateSource(oldValue, newValue);
        }

        public SKStretch Stretch
        {
            get { return (SKStretch)GetValue(StretchProperty); }
            set { SetValue(StretchProperty, value); }
        }

        /// <summary>
        /// Using a DependencyProperty as the backing store for <see cref="Stretch"/>.  This enables animation, styling, binding, etc...
        /// </summary>
        public static readonly DependencyProperty StretchProperty =
            DependencyProperty.Register(
                nameof(Stretch),
                typeof(SKStretch),
                typeof(GIFView),
                new PropertyMetadata(SKStretch.None, OnStretchChanged));

        private static void OnStretchChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var view = (GIFView)d;
            view.UpdateStretch();
        }
    }
}
