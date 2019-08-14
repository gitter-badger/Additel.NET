using Android.Content;
using Android.Graphics;

namespace Additel.SkiaViews
{
    partial class SwitchView
    {
        private bool _value;
        private Color _onColor = Colors.GreenActive.ToColor();

        public bool Value
        {
            get => _value;
            set
            {
                if (_value == value)
                    return;

                _value = value;
                UpdateValue();
            }
        }

        public Color OnColor
        {
            get => _onColor;
            set
            {
                if (_onColor == value)
                    return;

                _onColor = value;
                UpdateOnColor();
            }
        }

        public SwitchView(Context context)
            : base(context)
        {

        }
    }
}
