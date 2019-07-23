using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Additel.Core.WeakEvent
{
    /// <summary>
    /// <see cref="WeakEventManager"/> 类，弱事件管理器，为事件监听者提供弱事件支持
    /// </summary>
    public class WeakEventManager
    {
        private static readonly Lazy<IDictionary<string, WeakEventManager>> _lazyManagers
            = new Lazy<IDictionary<string, WeakEventManager>>(() => new ConcurrentDictionary<string, WeakEventManager>(), true);

        private static readonly MethodInfo _method
            = typeof(WeakEventManager).GetMethod(nameof(OnEvent), BindingFlags.Instance | BindingFlags.NonPublic);

        private readonly WeakReference _eventSource;
        private readonly EventInfo _event;
        private readonly Delegate _handler;
        private readonly IList<WeakDelegate> _handlers;

        private static IDictionary<string, WeakEventManager> Managers
            => _lazyManagers.Value;

        private bool IsStatic
            => _eventSource == null;

        private object EventSource
            => _eventSource?.Target;

        /// <summary>
        /// 注册弱事件
        /// </summary>
        /// <typeparam name="TEventSource">事件源类型</typeparam>
        /// <param name="eventSource">事件源</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="handler">需要注册的委托</param>
        public static void AddEventHandler<TEventSource>(TEventSource eventSource, string eventName, EventHandler handler)
            => PrivateAddEventHandler(eventSource, eventName, handler);

        /// <summary>
        /// 注册弱事件
        /// </summary>
        /// <typeparam name="TEventSource">事件源类型</typeparam>
        /// <typeparam name="TEventArgs">事件参数类型</typeparam>
        /// <param name="eventSource">事件源</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="handler">需要注册的委托</param>
        public static void AddEventHandler<TEventSource, TEventArgs>(TEventSource eventSource, string eventName, EventHandler<TEventArgs> handler)
            where TEventArgs : EventArgs
            => PrivateAddEventHandler(eventSource, eventName, handler);

        /// <summary>
        /// 注销弱事件
        /// </summary>
        /// <typeparam name="TEventSource">事件源类型</typeparam>
        /// <param name="eventSource">事件源</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="handler">需要注销的委托</param>
        public static void RemoveEventHandler<TEventSource>(TEventSource eventSource, string eventName, EventHandler handler)
            => PrivateRemoveEventHandler(eventSource, eventName, handler);

        /// <summary>
        /// 注销弱事件
        /// </summary>
        /// <typeparam name="TEventSource">事件源类型</typeparam>
        /// <typeparam name="TEventArgs">事件参数类型</typeparam>
        /// <param name="eventSource">事件源</param>
        /// <param name="eventName">事件名称</param>
        /// <param name="handler">需要注销的委托</param>
        public static void RemoveEventHandler<TEventSource, TEventArgs>(TEventSource eventSource, string eventName, EventHandler<TEventArgs> handler)
            where TEventArgs : EventArgs
            => PrivateRemoveEventHandler(eventSource, eventName, handler);

        private static void PrivateAddEventHandler<TEventSource>(TEventSource eventSource, string eventName, Delegate handler)
        {
            if (handler == null)
                return;

            var type = typeof(TEventSource);
            var key = $"{type.FullName}.{RuntimeHelpers.GetHashCode(eventSource)}.{eventName}";

            if (!Managers.TryGetValue(key, out var manager))
            {
                var @event = type.GetEvent(eventName)
                    ?? throw new ArgumentException($"`{eventName}` event not found on type `{type.FullName}`.");
                manager = new WeakEventManager(eventSource, @event);
                Managers.Add(key, manager);
            }

            manager.AddEventHandler(handler);
        }

        private static void PrivateRemoveEventHandler<TEventSource>(TEventSource eventSource, string eventName, Delegate handler)
        {
            if (handler == null)
                return;

            var type = typeof(TEventSource);
            var key = $"{type.FullName}.{RuntimeHelpers.GetHashCode(eventSource)}.{eventName}";

            if (!Managers.TryGetValue(key, out var manager))
                return;

            manager.RemoveEventHandler(handler);
        }

        private WeakEventManager(object eventSource, EventInfo @event)
        {
            _handlers = new List<WeakDelegate>();
            _eventSource = eventSource == null ? null : new WeakReference(eventSource);
            _event = @event;
            _handler = Delegate.CreateDelegate(_event.EventHandlerType, this, _method);

            StartListening();
        }

        private void AddEventHandler(Delegate handler)
        {
            // 是否需要考虑线程安全
            _handlers.Add(new WeakDelegate(handler));
        }

        private void RemoveEventHandler(Delegate handler)
        {
            // 是否需要考虑线程安全
            for (int num = _handlers.Count - 1; num >= 0; num--)
            {
                if (_handlers[num].Matches(handler))
                {
                    _handlers.RemoveAt(num);
                    break;
                }
            }
        }

        private void OnEvent(object sender, EventArgs args)
        {
            for (int num = _handlers.Count - 1; num >= 0; num--)
            {
                var handler = _handlers[num];
                var method = handler.Method;
                var target = handler.Target;
                var parameters = new[] { sender, args };

                if (handler.IsStatic)
                    method.Invoke(null, parameters);
                else if (handler.Target != null)
                    method.Invoke(target, parameters);
                else
                    _handlers.RemoveAt(num);
            }

            Purge();
        }

        private static void Purge()
        {
            var kvs = Managers.ToList();

            for (int num = kvs.Count - 1; num >= 0; num--)
            {
                var kv = kvs[num];
                if (!kv.Value.IsStatic &&
                    kv.Value.EventSource == null)
                {
                    Managers.Remove(kv.Key);
                }
            }
        }

        private void StartListening()
            => _event.AddEventHandler(EventSource, _handler);

        private void StopListening()
            => _event.RemoveEventHandler(EventSource, _handler);
    }
}
