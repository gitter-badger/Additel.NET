using CoreGraphics;
using UIKit;

namespace Additel.Controls
{
    partial class Switch
    {
        private bool _state;
        private UIColor _onColor = SKColors.GreenActive.ToColor();

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

        public UIColor OnColor
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

        public Switch()
            : base()
        {

        }

        public Switch(CGRect frame)
            : base(frame)
        {

        }
    }
}
