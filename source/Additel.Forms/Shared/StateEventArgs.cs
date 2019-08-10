using System;
using System.Collections.Generic;
using System.Text;

namespace Additel.Forms
{
    public class StateEventArgs : EventArgs
    {
        public StateEventArgs(bool isChecked)
        {
            IsChecked = isChecked;
        }

        public bool IsChecked { get; }
    }
}
