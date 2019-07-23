using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace Additel.Core
{
    public static partial class Device
    {
        private const string WRONG_THREAD_ERROR = "RPC_E_WRONG_THREAD";

        private static readonly CoreDispatcher s_dispatcher = Window.Current.Dispatcher;

        public static async void BeginInvokeOnMainThread(Action action)
        {
            if (CoreApplication.Views.Count == 1)
            {
                // This is the normal scenario - one window only
                s_dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action()).WatchForError();
                return;
            }

            await TryAllDispatchers(action);
        }

        private async static Task TryAllDispatchers(Action action)
        {
            // Our best bet is Window.Current; most of the time, that's the Dispatcher we need
            var currentWindow = Window.Current;

            if (currentWindow?.Dispatcher != null)
            {
                try
                {
                    await TryDispatch(currentWindow.Dispatcher, action);
                    return;
                }
                catch (Exception ex) when (ex.Message.Contains(WRONG_THREAD_ERROR))
                {
                    // The current window is not the one we need 
                }
            }

            // Either Window.Current was the wrong Dispatcher, or Window.Current was null because we're on a 
            // non-UI thread (e.g., one from the thread pool). So now it's time to try all the available Dispatchers 

            var views = CoreApplication.Views;

            for (int n = 0; n < views.Count; n++)
            {
                var dispatcher = views[n].Dispatcher;

                if (dispatcher == null || dispatcher == currentWindow?.Dispatcher)
                {
                    // Obviously null Dispatchers are no good, and we already tried the one from currentWindow
                    continue;
                }

                // We need to ignore Deactivated/Never Activated windows, but it's possible we can't access their 
                // properties from this thread. So we'll check those using the Dispatcher
                bool activated = false;

                await TryDispatch(dispatcher, () =>
                {
                    var mode = views[n].CoreWindow.ActivationMode;
                    activated = (mode == CoreWindowActivationMode.ActivatedInForeground
                        || mode == CoreWindowActivationMode.ActivatedNotForeground);
                });

                if (!activated)
                {
                    // This is a deactivated (or not yet activated) window; move on
                    continue;
                }

                try
                {
                    await TryDispatch(dispatcher, action);
                    return;
                }
                catch (Exception ex) when (ex.Message.Contains(WRONG_THREAD_ERROR))
                {
                    // This was the incorrect dispatcher; move on to try another one
                }
            }
        }

        private async static Task<bool> TryDispatch(CoreDispatcher dispatcher, Action action)
        {
            if (dispatcher == null)
            {
                throw new ArgumentNullException(nameof(dispatcher));
            }

            var taskCompletionSource = new TaskCompletionSource<bool>();

            await dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                try
                {
                    action();
                    taskCompletionSource.SetResult(true);
                }
                catch (Exception ex)
                {
                    taskCompletionSource.SetException(ex);
                }
            });

            return await taskCompletionSource.Task;
        }

        public static bool IsInvokeRequired
        {
            get
            {
                if (CoreApplication.Views.Count == 1)
                {
                    return !s_dispatcher.HasThreadAccess;
                }

                if (Window.Current?.Dispatcher != null)
                {
                    return !Window.Current.Dispatcher.HasThreadAccess;
                }

                return true;
            }
        }
    }
}
