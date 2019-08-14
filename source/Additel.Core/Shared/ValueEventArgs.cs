﻿using System;

namespace Additel.Core
{
    public class ValueEventArgs<T> : EventArgs
    {
        public T Value { get; }

        public ValueEventArgs(T value)
        {
            Value = value;
        }
    }
}
