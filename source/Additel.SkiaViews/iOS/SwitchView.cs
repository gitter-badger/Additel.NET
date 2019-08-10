using CoreGraphics;
using UIKit;

namespace Additel.SkiaViews
{
    partial class SwitchView
    {
        private bool _state;
        private UIColor _onColor = Colors.GreenActive.ToColor();

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

        public SwitchView()
            : base()
        {

        }

        public SwitchView(CGRect frame)
            : base(frame)
        {

        }
    }
}
