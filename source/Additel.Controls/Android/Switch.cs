using Android.Content;
using Android.Graphics;

namespace Additel.Controls
{
    partial class Switch
    {
        private bool _state;
        private Color _onColor = SKColors.GreenActive.ToColor();

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

        public Switch(Context context)
            : base(context)
        {

        }
    }
}
