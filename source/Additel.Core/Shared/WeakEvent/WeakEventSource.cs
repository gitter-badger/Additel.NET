using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Additel.Core.WeakEvent
{
    /// <summary>
    /// <see cref="WeakEventSource"/> 类，弱事件源，为事件源提供弱事件支持
    /// </summary>
    public class WeakEventSource
    {
        private readonly IDictionary<string, IList<WeakDelegate>> _handlerLists
            = new Dictionary<string, IList<WeakDelegate>>();

        /// <summary>
        /// 注册弱事件
        /// </summary>
        /// <param name="handler">需要注册的委托</param>
        /// <param name="eventName">事件名称</param>
        public void AddEventHandler(EventHandler handler, [CallerMemberName]string eventName = null)
            => PrivateAddEventHandler(handler, eventName);

        /// <summary>
        /// 注册弱事件
        /// </summary>
        /// <param name="handler">需要注册的委托</param>
        /// <param name="eventName">事件名称</param>
        public void AddEventHandler<TEventArgs>(EventHandler<TEventArgs> handler, [CallerMemberName]string eventName = null)
            where TEventArgs : EventArgs
            => PrivateAddEventHandler(handler, eventName);

        /// <summary>
        /// 注销弱事件
        /// </summary>
        /// <param name="handler">需要注销的委托</param>
        /// <param name="eventName">事件名称</param>
        public void RemoveEventHandler(EventHandler handler, [CallerMemberName]string eventName = null)
            => PrivateRemoveEventHandler(handler, eventName);

        /// <summary>
        /// 注销弱事件
        /// </summary>
        /// <param name="handler">需要注销的委托</param>
        /// <param name="eventName">事件名称</param>
        public void RemoveEventHandler<TEventArgs>(EventHandler<TEventArgs> handler, [CallerMemberName]string eventName = null)
            where TEventArgs : EventArgs
            => PrivateRemoveEventHandler(handler, eventName);

        /// <summary>
        /// 触发弱事件
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <param name="sender">事件源</param>
        /// <param name="args">委托参数</param>
        public void RaiseEvent<TEventArgs>(string eventName, object sender, TEventArgs args)
            where TEventArgs : EventArgs
        {
            if (_handlerLists.TryGetValue(eventName, out var handlers))
            {
                for (int num = handlers.Count - 1; num >= 0; num--)
                {
                    var handler = handlers[num];
                    var method = handler.Method;
                    var target = handler.Target;
                    var parameters = new[] { sender, args };

                    if (handler.IsStatic)
                        method.Invoke(null, parameters);
                    else if (handler.Target != null)
                        method.Invoke(target, parameters);
                    else
                        handlers.RemoveAt(num);
                }
            }
        }

        private void PrivateAddEventHandler(Delegate handler, string eventName)
        {
            if (handler == null)
                return;

            if (!_handlerLists.TryGetValue(eventName, out var handlers))
            {
                handlers = new List<WeakDelegate>();
                _handlerLists.Add(eventName, handlers);
            }
            handlers.Add(new WeakDelegate(handler));
        }

        private void PrivateRemoveEventHandler(Delegate handler, string eventName)
        {
            if (handler == null || !_handlerLists.TryGetValue(eventName, out var handlers))
                return;

            for (int num = handlers.Count - 1; num >= 0; num--)
            {
                if (handlers[num].Matches(handler))
                {
                    handlers.RemoveAt(num);
                    break;
                }
            }
        }
    }
}
