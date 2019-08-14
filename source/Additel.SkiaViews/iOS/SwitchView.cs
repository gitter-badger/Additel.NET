using CoreGraphics;
using UIKit;

namespace Additel.SkiaViews
{
    partial class SwitchView
    {
        private bool _value;
        private UIColor _onColor = Colors.GreenActive.ToColor();

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

        public UIColor OnColor
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
