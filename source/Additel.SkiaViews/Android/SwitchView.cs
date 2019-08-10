using Android.Content;
using Android.Graphics;

namespace Additel.SkiaViews
{
    partial class SwitchView
    {
        private bool _state;
        private Color _onColor = Colors.GreenActive.ToColor();

        public bool State
        {
            get => _state;
            set
            {
                if (_state == value)
                    return;

                _state = value;

                OnStateChanged();
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

                OnOnColorChanged();
            }
        }

        public SwitchView(Context context)
            : base(context)
        {

        }
    }
}
