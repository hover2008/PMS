using System;
using System.Collections.Generic;
using System.Linq;

namespace JW.Core.EventBus
{
    /// <summary>
    /// 内存事件总线订阅管理类
    /// </summary>
    public partial class InMemoryEventBusSubscriptionsManager : IEventBusSubscriptionsManager
    {
        #region Fields

        private readonly Dictionary<string, List<SubscriptionInfo>> _handlers;
        private readonly List<Type> _eventTypes;

        public event EventHandler<string> OnEventRemoved;

        #endregion

        #region 构造函数

        public InMemoryEventBusSubscriptionsManager()
        {
            _handlers = new Dictionary<string, List<SubscriptionInfo>>();
            _eventTypes = new List<Type>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 判断是否为空
        /// </summary>
        public bool IsEmpty => !_handlers.Keys.Any();

        /// <summary>
        /// 清理
        /// </summary>
        public void Clear() => _handlers.Clear();

        /// <summary>
        /// 添加动态订阅
        /// </summary>
        /// <typeparam name="TH">handle</typeparam>
        /// <param name="eventName">事件名</param>
        public void AddDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            DoAddSubscription(typeof(TH), eventName, isDynamic: true);
        }

        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <typeparam name="T">集成事件Model</typeparam>
        /// <typeparam name="TH">Handle</typeparam>
        public void AddSubscription<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            DoAddSubscription(typeof(TH), eventName, isDynamic: false);
            _eventTypes.Add(typeof(T));
        }

        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <param name="handlerType">Handler类型</param>
        /// <param name="eventName">事件名</param>
        /// <param name="isDynamic">是否动态</param>
        private void DoAddSubscription(Type handlerType, string eventName, bool isDynamic)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                _handlers.Add(eventName, new List<SubscriptionInfo>());
            }

            if (_handlers[eventName].Any(s => s.HandlerType == handlerType))
            {
                throw new ArgumentException(
                    $"Handler Type {handlerType.Name} already registered for '{eventName}'", nameof(handlerType));
            }

            if (isDynamic)
            {
                _handlers[eventName].Add(SubscriptionInfo.Dynamic(handlerType));
            }
            else
            {
                _handlers[eventName].Add(SubscriptionInfo.Typed(handlerType));
            }
        }

        /// <summary>
        /// 移除动态订阅
        /// </summary>
        /// <typeparam name="TH">handle</typeparam>
        /// <param name="eventName">事件名</param>
        public void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            var handlerToRemove = FindDynamicSubscriptionToRemove<TH>(eventName);
            DoRemoveHandler(eventName, handlerToRemove);
        }

        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <typeparam name="T">集成事件model</typeparam>
        /// <typeparam name="TH">handle</typeparam>
        public void RemoveSubscription<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent
        {
            var handlerToRemove = FindSubscriptionToRemove<T, TH>();
            var eventName = GetEventKey<T>();
            DoRemoveHandler(eventName, handlerToRemove);
        }

        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="subsToRemove">订阅信息</param>
        private void DoRemoveHandler(string eventName, SubscriptionInfo subsToRemove)
        {
            if (subsToRemove != null)
            {
                _handlers[eventName].Remove(subsToRemove);
                if (!_handlers[eventName].Any())
                {
                    _handlers.Remove(eventName);
                    var eventType = _eventTypes.SingleOrDefault(e => e.Name == eventName);
                    if (eventType != null)
                    {
                        _eventTypes.Remove(eventType);
                    }
                    RaiseOnEventRemoved(eventName);
                }

            }
        }

        /// <summary>
        /// 获取订阅信息集合
        /// </summary>
        /// <typeparam name="T">集成事件Model</typeparam>
        /// <returns></returns>
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return GetHandlersForEvent(key);
        }

        /// <summary>
        /// 获取订阅信息集合
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <returns></returns>
        public IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName) => _handlers[eventName];

        /// <summary>
        /// 根据事件名移除
        /// </summary>
        /// <param name="eventName">事件名</param>
        private void RaiseOnEventRemoved(string eventName)
        {
            var handler = OnEventRemoved;
            if (handler != null)
            {
                OnEventRemoved(this, eventName);
            }
        }

        /// <summary>
        /// 根据事件名寻找动态订阅来删除
        /// </summary>
        /// <typeparam name="TH">Handle</typeparam>
        /// <param name="eventName">事件名</param>
        /// <returns></returns>
        private SubscriptionInfo FindDynamicSubscriptionToRemove<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler
        {
            return DoFindSubscriptionToRemove(eventName, typeof(TH));
        }

        /// <summary>
        /// 寻找订阅
        /// </summary>
        /// <typeparam name="T">集成事件Model</typeparam>
        /// <typeparam name="TH">Handle</typeparam>
        /// <returns></returns>
        private SubscriptionInfo FindSubscriptionToRemove<T, TH>()
             where T : IntegrationEvent
             where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventKey<T>();
            return DoFindSubscriptionToRemove(eventName, typeof(TH));
        }

        /// <summary>
        /// 根据事件名和处理程序类型寻找订阅
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <param name="handlerType">handle type</param>
        /// <returns></returns>
        private SubscriptionInfo DoFindSubscriptionToRemove(string eventName, Type handlerType)
        {
            if (!HasSubscriptionsForEvent(eventName))
            {
                return null;
            }

            return _handlers[eventName].SingleOrDefault(s => s.HandlerType == handlerType);

        }

        /// <summary>
        /// 判断是否存在订阅事件
        /// </summary>
        /// <typeparam name="T">集成事件Model</typeparam>
        /// <returns></returns>
        public bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent
        {
            var key = GetEventKey<T>();
            return HasSubscriptionsForEvent(key);
        }

        /// <summary>
        /// 根据事件名判断是否存在订阅事件
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        public bool HasSubscriptionsForEvent(string eventName) => _handlers.ContainsKey(eventName);

        /// <summary>
        /// 根据事件名获取事件类型
        /// </summary>
        /// <param name="eventName">事件名</param>
        /// <returns></returns>
        public Type GetEventTypeByName(string eventName) => _eventTypes.SingleOrDefault(t => t.Name == eventName);

        /// <summary>
        /// 获取事件类型名
        /// </summary>
        /// <typeparam name="T">事件类型</typeparam>
        /// <returns></returns>
        public string GetEventKey<T>()
        {
            return typeof(T).Name;
        }

        #endregion
    }
}

