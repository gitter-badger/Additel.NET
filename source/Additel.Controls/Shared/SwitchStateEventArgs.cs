using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additel.Controls
{
    public class SwitchStateEventArgs
    {
        public bool State { get; }

        public SwitchStateEventArgs(bool state)
        {
            State = state;
        }
    }
}
