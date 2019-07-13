using System;
using System.Collections.Generic;
using System.Text;

namespace Additel.Forms
{
    public class SwitchStateEventArgs : EventArgs
    {
        public SwitchStateEventArgs(bool isChecked)
        {
            IsChecked = isChecked;
        }

        public bool IsChecked { get; }
    }
}
