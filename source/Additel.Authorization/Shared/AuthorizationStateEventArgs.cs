using System;

namespace Additel.Authorization
{
    public class AuthorizationStateEventArgs : EventArgs
    {
        public AuthorizationType Category { get; }
        public AuthorizationState State { get; }

        public AuthorizationStateEventArgs(AuthorizationType category, AuthorizationState state)
        {
            Category = category;
            State = state;
        }
    }
}