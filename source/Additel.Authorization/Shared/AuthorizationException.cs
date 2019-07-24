using System;

namespace Additel.Authorization
{
    public partial class AuthorizationException : Exception
    {
        public AuthorizationState State { get; }

        public AuthorizationException(AuthorizationState state)
            : base()
        {
            State = state;
        }

        public AuthorizationException(AuthorizationState state, string message)
            : base(message)
        {
            State = state;
        }
    }
}
