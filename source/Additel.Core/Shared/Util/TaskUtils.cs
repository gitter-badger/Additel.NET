using System;
using System.Threading;
using System.Threading.Tasks;

namespace Additel.Core.Util
{
    /// <summary>
    /// <see cref="TaskUtils"/> 类，帮助对 <see cref="Task"/> 类进行一些便捷操作
    /// </summary>
    public static class TaskUtils
    {
        /// <summary>
        /// 使用 <see cref="TaskCompletionSource{TResult}"/> 通过方法和相关事件创建 <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TReturn">异步方法的返回值类型</typeparam>
        /// <typeparam name="TCompleteHandler">异步完成事件类型</typeparam>
        /// <param name="execute">异步方法开始时执行的方法</param>
        /// <param name="getCompleteHandler">异步完成后触发的委托方法，返回结果</param>
        /// <param name="subscribeComplete">注册完成事件的委托</param>
        /// <param name="unsubscribeComplete">注销完成事件的委托</param>
        /// <param name="timeout">超时时间，默认值为 <see cref="Timeout.Infinite"/> </param>
        /// <param name="token">取消令牌</param>
        /// <returns>创建的返回值类型为 <typeparamref name="TReturn"/> 异步 <see cref="Task"/> </returns>
        public static Task<TReturn> FromEvent<TReturn>(
            Action execute,
            Func<Action<TReturn>, EventHandler> getCompleteHandler,
            Action<EventHandler> subscribeComplete,
            Action<EventHandler> unsubscribeComplete,
            int timeout = Timeout.Infinite,
            CancellationToken token = default(CancellationToken))
            => FromEvent<TReturn, EventHandler>(
                execute,
                getCompleteHandler,
                subscribeComplete,
                unsubscribeComplete);

        /// <summary>
        /// 使用 <see cref="TaskCompletionSource{TResult}"/> 通过方法和相关事件创建 <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TReturn">异步方法的返回值类型</typeparam>
        /// <typeparam name="TCompleteHandler">异步完成事件类型</typeparam>
        /// <param name="execute">异步方法开始时执行的方法</param>
        /// <param name="getCompleteHandler">异步完成后触发的委托方法，返回结果</param>
        /// <param name="subscribeComplete">注册完成事件的委托</param>
        /// <param name="unsubscribeComplete">注销完成事件的委托</param>
        /// <param name="timeout">超时时间，默认值为 <see cref="Timeout.Infinite"/> </param>
        /// <param name="token">取消令牌</param>
        /// <returns>创建的返回值类型为 <typeparamref name="TReturn"/> 异步 <see cref="Task"/> </returns>
        public static Task<TReturn> FromEvent<TReturn, TCompleteEventArgs>(
            Action execute,
            Func<Action<TReturn>, EventHandler<TCompleteEventArgs>> getCompleteHandler,
            Action<EventHandler<TCompleteEventArgs>> subscribeComplete,
            Action<EventHandler<TCompleteEventArgs>> unsubscribeComplete,
            int timeout = Timeout.Infinite,
            CancellationToken token = default(CancellationToken))
            where TCompleteEventArgs : EventArgs
            => FromEvent<TReturn, EventHandler<TCompleteEventArgs>>(
                execute,
                getCompleteHandler,
                subscribeComplete,
                unsubscribeComplete);

        /// <summary>
        /// 使用 <see cref="TaskCompletionSource{TResult}"/> 通过方法和相关事件创建 <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TReturn">异步方法的返回值类型</typeparam>
        /// <typeparam name="TCompleteHandler">异步完成事件类型</typeparam>
        /// <typeparam name="TRejectHandler">异步失败事件类型</typeparam>
        /// <param name="execute">异步方法开始时执行的方法</param>
        /// <param name="getCompleteHandler">异步完成后触发的委托方法</param>
        /// <param name="subscribeComplete">注册完成事件的委托</param>
        /// <param name="unsubscribeComplete">注销完成事件的委托</param>
        /// <param name="getRejectHandler">异步失败后触发的委托方法</param>
        /// <param name="subscribeReject">注册失败事件的委托</param>
        /// <param name="unsubscribeReject">注销失败事件的委托</param>
        /// <param name="timeout">超时时间，默认值为 <see cref="Timeout.Infinite"/> </param>
        /// <param name="token">取消令牌</param>
        /// <returns>创建的返回值类型为 <typeparamref name="TReturn"/> 异步 <see cref="Task"/> </returns>
        public static Task<TReturn> FromEvent<TReturn>(
            Action execute,
            Func<Action<TReturn>, EventHandler> getCompleteHandler,
            Action<EventHandler> subscribeComplete,
            Action<EventHandler> unsubscribeComplete,
            Func<Action<Exception>, EventHandler> getRejectHandler,
            Action<EventHandler> subscribeReject,
            Action<EventHandler> unsubscribeReject,
            int timeout = Timeout.Infinite,
            CancellationToken token = default(CancellationToken))
            => FromEvent<TReturn, EventHandler, EventHandler>(
                execute,
                getCompleteHandler,
                subscribeComplete,
                unsubscribeComplete,
                getRejectHandler,
                subscribeReject,
                unsubscribeReject,
                timeout,
                token);

        /// <summary>
        /// 使用 <see cref="TaskCompletionSource{TResult}"/> 通过方法和相关事件创建 <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TReturn">异步方法的返回值类型</typeparam>
        /// <typeparam name="TCompleteHandler">异步完成事件类型</typeparam>
        /// <typeparam name="TRejectHandler">异步失败事件类型</typeparam>
        /// <param name="execute">异步方法开始时执行的方法</param>
        /// <param name="getCompleteHandler">异步完成后触发的委托方法</param>
        /// <param name="subscribeComplete">注册完成事件的委托</param>
        /// <param name="unsubscribeComplete">注销完成事件的委托</param>
        /// <param name="getRejectHandler">异步失败后触发的委托方法</param>
        /// <param name="subscribeReject">注册失败事件的委托</param>
        /// <param name="unsubscribeReject">注销失败事件的委托</param>
        /// <param name="timeout">超时时间，默认值为 <see cref="Timeout.Infinite"/> </param>
        /// <param name="token">取消令牌</param>
        /// <returns>创建的返回值类型为 <typeparamref name="TReturn"/> 异步 <see cref="Task"/> </returns>
        public static Task<TReturn> FromEvent<TReturn, TCompleteEventArgs>(
            Action execute,
            Func<Action<TReturn>, EventHandler<TCompleteEventArgs>> getCompleteHandler,
            Action<EventHandler<TCompleteEventArgs>> subscribeComplete,
            Action<EventHandler<TCompleteEventArgs>> unsubscribeComplete,
            Func<Action<Exception>, EventHandler> getRejectHandler,
            Action<EventHandler> subscribeReject,
            Action<EventHandler> unsubscribeReject,
            int timeout = Timeout.Infinite,
            CancellationToken token = default(CancellationToken))
            where TCompleteEventArgs : EventArgs
            => FromEvent<TReturn, EventHandler<TCompleteEventArgs>, EventHandler>(
                execute,
                getCompleteHandler,
                subscribeComplete,
                unsubscribeComplete,
                getRejectHandler,
                subscribeReject,
                unsubscribeReject,
                timeout,
                token);

        /// <summary>
        /// 使用 <see cref="TaskCompletionSource{TResult}"/> 通过方法和相关事件创建 <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TReturn">异步方法的返回值类型</typeparam>
        /// <typeparam name="TCompleteHandler">异步完成事件类型</typeparam>
        /// <typeparam name="TRejectHandler">异步失败事件类型</typeparam>
        /// <param name="execute">异步方法开始时执行的方法</param>
        /// <param name="getCompleteHandler">异步完成后触发的委托方法</param>
        /// <param name="subscribeComplete">注册完成事件的委托</param>
        /// <param name="unsubscribeComplete">注销完成事件的委托</param>
        /// <param name="getRejectHandler">异步失败后触发的委托方法</param>
        /// <param name="subscribeReject">注册失败事件的委托</param>
        /// <param name="unsubscribeReject">注销失败事件的委托</param>
        /// <param name="timeout">超时时间，默认值为 <see cref="Timeout.Infinite"/> </param>
        /// <param name="token">取消令牌</param>
        /// <returns>创建的返回值类型为 <typeparamref name="TReturn"/> 异步 <see cref="Task"/> </returns>
        public static Task<TReturn> FromEvent<TReturn, TRejectEventArgs>(
            Action execute,
            Func<Action<TReturn>, EventHandler> getCompleteHandler,
            Action<EventHandler> subscribeComplete,
            Action<EventHandler> unsubscribeComplete,
            Func<Action<Exception>, EventHandler<TRejectEventArgs>> getRejectHandler,
            Action<EventHandler<TRejectEventArgs>> subscribeReject,
            Action<EventHandler<TRejectEventArgs>> unsubscribeReject,
            int timeout = Timeout.Infinite,
            CancellationToken token = default(CancellationToken))
            where TRejectEventArgs : EventArgs
            => FromEvent<TReturn, EventHandler, EventHandler<TRejectEventArgs>>(
                execute,
                getCompleteHandler,
                subscribeComplete,
                unsubscribeComplete,
                getRejectHandler,
                subscribeReject,
                unsubscribeReject,
                timeout,
                token);

        /// <summary>
        /// 使用 <see cref="TaskCompletionSource{TResult}"/> 通过方法和相关事件创建 <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TReturn">异步方法的返回值类型</typeparam>
        /// <typeparam name="TCompleteHandler">异步完成事件类型</typeparam>
        /// <typeparam name="TRejectHandler">异步失败事件类型</typeparam>
        /// <param name="execute">异步方法开始时执行的方法</param>
        /// <param name="getCompleteHandler">异步完成后触发的委托方法</param>
        /// <param name="subscribeComplete">注册完成事件的委托</param>
        /// <param name="unsubscribeComplete">注销完成事件的委托</param>
        /// <param name="getRejectHandler">异步失败后触发的委托方法</param>
        /// <param name="subscribeReject">注册失败事件的委托</param>
        /// <param name="unsubscribeReject">注销失败事件的委托</param>
        /// <param name="timeout">超时时间，默认值为 <see cref="Timeout.Infinite"/> </param>
        /// <param name="token">取消令牌</param>
        /// <returns>创建的返回值类型为 <typeparamref name="TReturn"/> 异步 <see cref="Task"/> </returns>
        public static Task<TReturn> FromEvent<TReturn, TCompleteEventArgs, TRejectEventArgs>(
            Action execute,
            Func<Action<TReturn>, EventHandler<TCompleteEventArgs>> getCompleteHandler,
            Action<EventHandler<TCompleteEventArgs>> subscribeComplete,
            Action<EventHandler<TCompleteEventArgs>> unsubscribeComplete,
            Func<Action<Exception>, EventHandler<TRejectEventArgs>> getRejectHandler,
            Action<EventHandler<TRejectEventArgs>> subscribeReject,
            Action<EventHandler<TRejectEventArgs>> unsubscribeReject,
            int timeout = Timeout.Infinite,
            CancellationToken token = default(CancellationToken))
            where TCompleteEventArgs : EventArgs
            where TRejectEventArgs : EventArgs
            => FromEvent<TReturn, EventHandler<TCompleteEventArgs>, EventHandler<TRejectEventArgs>>(
                execute,
                getCompleteHandler,
                subscribeComplete,
                unsubscribeComplete,
                getRejectHandler,
                subscribeReject,
                unsubscribeReject,
                timeout,
                token);

        /// <summary>
        /// 使用 <see cref="TaskCompletionSource{TResult}"/> 通过方法和相关事件创建 <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TReturn">异步方法的返回值类型</typeparam>
        /// <typeparam name="TCompleteHandler">异步完成事件类型</typeparam>
        /// <param name="execute">异步方法开始时执行的方法</param>
        /// <param name="getCompleteHandler">异步完成后触发的委托方法，返回结果</param>
        /// <param name="subscribeComplete">注册完成事件的委托</param>
        /// <param name="unsubscribeComplete">注销完成事件的委托</param>
        /// <param name="timeout">超时时间，默认值为 <see cref="Timeout.Infinite"/> </param>
        /// <param name="token">取消令牌</param>
        /// <returns>创建的返回值类型为 <typeparamref name="TReturn"/> 异步 <see cref="Task"/> </returns>
        public static Task<TReturn> FromEvent<TReturn, TCompleteHandler>(
            Action execute,
            Func<Action<TReturn>, TCompleteHandler> getCompleteHandler,
            Action<TCompleteHandler> subscribeComplete,
            Action<TCompleteHandler> unsubscribeComplete,
            int timeout = Timeout.Infinite,
            CancellationToken token = default(CancellationToken))
            => FromEvent<TReturn, TCompleteHandler, object>(
                execute,
                getCompleteHandler,
                subscribeComplete,
                unsubscribeComplete,
                getRejectHandler: reject => null,
                subscribeReject: handler => { },
                unsubscribeReject: handler => { },
                timeout: timeout,
                token: token);

        /// <summary>
        /// 使用 <see cref="TaskCompletionSource{TResult}"/> 通过方法和相关事件创建 <see cref="Task"/>
        /// </summary>
        /// <typeparam name="TReturn">异步方法的返回值类型</typeparam>
        /// <typeparam name="TCompleteHandler">异步完成事件类型</typeparam>
        /// <typeparam name="TRejectHandler">异步失败事件类型</typeparam>
        /// <param name="execute">异步方法开始时执行的方法</param>
        /// <param name="getCompleteHandler">异步完成后触发的委托方法</param>
        /// <param name="subscribeComplete">注册完成事件的委托</param>
        /// <param name="unsubscribeComplete">注销完成事件的委托</param>
        /// <param name="getRejectHandler">异步失败后触发的委托方法</param>
        /// <param name="subscribeReject">注册失败事件的委托</param>
        /// <param name="unsubscribeReject">注销失败事件的委托</param>
        /// <param name="timeout">超时时间，默认值为 <see cref="Timeout.Infinite"/> </param>
        /// <param name="token">取消令牌</param>
        /// <returns>创建的返回值类型为 <typeparamref name="TReturn"/> 异步 <see cref="Task"/> </returns>
        public static async Task<TReturn> FromEvent<TReturn, TCompleteHandler, TRejectHandler>(
            Action execute,
            Func<Action<TReturn>, TCompleteHandler> getCompleteHandler,
            Action<TCompleteHandler> subscribeComplete,
            Action<TCompleteHandler> unsubscribeComplete,
            Func<Action<Exception>, TRejectHandler> getRejectHandler,
            Action<TRejectHandler> subscribeReject,
            Action<TRejectHandler> unsubscribeReject,
            int timeout = Timeout.Infinite,
            CancellationToken token = default(CancellationToken))
        {
            var tcs = new TaskCompletionSource<TReturn>();
            void complete(TReturn args) => tcs.TrySetResult(args);
            void reject(Exception ex) => tcs.TrySetException(ex);

            var completeHandler = getCompleteHandler(complete);
            var rejectHandler = getRejectHandler(reject);

            try
            {
                subscribeComplete(completeHandler);
                subscribeReject(rejectHandler);
                using (var cts = new CancellationTokenSource(timeout))
                using (cts.Token.Register(() => tcs.TrySetException(new TimeoutException())))
                using (token.Register(() => tcs.TrySetCanceled()))
                {
                    execute();
                    return await tcs.Task;
                }
            }
            finally
            {
                unsubscribeReject(rejectHandler);
                unsubscribeComplete(completeHandler);
            }
        }
    }
}
