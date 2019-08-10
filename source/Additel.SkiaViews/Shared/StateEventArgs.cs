using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Additel.SkiaViews
{
    public class StateEventArgs
    {
        public bool State { get; }

        public StateEventArgs(bool state)
        {
            State = state;
        }
    }
}
