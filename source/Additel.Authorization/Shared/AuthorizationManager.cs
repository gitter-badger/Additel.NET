using Additel.Core.Util;
using System;
using System.Threading.Tasks;

namespace Additel.Authorization
{
    public static partial class AuthorizationManager
    {
        #region 事件

        public static event EventHandler<AuthorizationStateEventArgs> AuthorizationRequested;
        #endregion

        #region 方法

        private static void RaiseAuthorizationRequested(AuthorizationCategory category, AuthorizationState state)
            => AuthorizationRequested?.Invoke(null, new AuthorizationStateEventArgs(category, state));

        public static AuthorizationState GetState(AuthorizationCategory category)
            => PlatformGetState(category);

        public static void Request(AuthorizationCategory category)
            => PlatformRequest(category);

        public static Task<AuthorizationState> RequestAsync(AuthorizationCategory category)
            => TaskUtils.FromEvent<AuthorizationState, AuthorizationStateEventArgs>(
                execute: () => Request(category),
                getCompleteHandler: complete => (s, e) =>
                {
                    if (e.Category != category)
                        return;

                    complete(e.State);
                },
                subscribeComplete: handler => AuthorizationRequested += handler,
                unsubscribeComplete: handler => AuthorizationRequested -= handler);

        public static async Task RequireAsync(AuthorizationCategory category)
        {
            var state = await RequestAsync(category);
            if (state != AuthorizationState.Authorized)
            {
                throw new AuthorizationException(state);
            }
        }
        #endregion
    }
}
