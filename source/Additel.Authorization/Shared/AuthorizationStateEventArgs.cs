using System;

namespace Additel.Authorization
{
    public class AuthorizationStateEventArgs : EventArgs
    {
        public AuthorizationCategory Category { get; }
        public AuthorizationState State { get; }

        public AuthorizationStateEventArgs(AuthorizationCategory category, AuthorizationState state)
        {
            Category = category;
            State = state;
        }
    }
}