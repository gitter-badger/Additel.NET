using System;
using System.Reflection;

namespace Additel.Core.WeakEvent
{
    internal struct WeakDelegate
    {
        private readonly WeakReference _target;

        public bool IsStatic
            => _target == null;

        public object Target
            => _target?.Target;

        public MethodInfo Method { get; }

        public WeakDelegate(Delegate handler)
        {
            _target = handler.Target == null
                ? null
                : new WeakReference(handler.Target);

            Method = handler.Method;
        }

        public bool Matches(Delegate handler)
            => handler.Target == Target
            ? handler.Method == Method
            : false;
    }
}
