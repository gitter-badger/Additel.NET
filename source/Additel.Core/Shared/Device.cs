using System;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace Additel.Core
{
    public static partial class Device
    {
        public static Task<T> InvokeOnMainThreadAsync<T>(Func<T> func)
        {
            var tcs = new TaskCompletionSource<T>();
            BeginInvokeOnMainThread(() =>
            {
                try
                {
                    var result = func();
                    tcs.SetResult(result);
                }
                catch (Exception ex)
                {
                    tcs.SetException(ex);
                }
            });
            return tcs.Task;
        }

        public static Task InvokeOnMainThreadAsync(Action action)
        {
            Func<object> dummyFunc = () => { action(); return null; };
            return InvokeOnMainThreadAsync(dummyFunc);
        }

        public static Task<T> InvokeOnMainThreadAsync<T>(Func<Task<T>> funcTask)
        {
            var tcs = new TaskCompletionSource<T>();
            BeginInvokeOnMainThread(
                async () =>
                {
                    try
                    {
                        var ret = await funcTask().ConfigureAwait(false);
                        tcs.SetResult(ret);
                    }
                    catch (Exception e)
                    {
                        tcs.SetException(e);
                    }
                }
            );

            return tcs.Task;
        }

        public static Task InvokeOnMainThreadAsync(Func<Task> funcTask)
        {
            Func<Task<object>> dummyFunc = () => { funcTask(); return null; };
            return InvokeOnMainThreadAsync(dummyFunc);
        }

        public static async Task<SynchronizationContext> GetMainThreadSynchronizationContextAsync()
        {
            SynchronizationContext ret = null;
            await InvokeOnMainThreadAsync(() =>
                ret = SynchronizationContext.Current
            ).ConfigureAwait(false);
            return ret;
        }
    }
}
