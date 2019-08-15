using Additel.Core;
using Android.Content;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Additel.SkiaViews
{
    partial class GIFView
    {
        private string _source;
        private SKStretch _stretch;

        public string Source
        {
            get => _source;
            set
            {
                if (_source == value)
                    return;

                var oldValue = _source;
                _source = value;
                UpdateSource(oldValue, _source);
            }
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

        public GIFView(Context context)
            : base(context)
        {

        }
    }
}
