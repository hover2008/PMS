using System;
using System.Collections.Generic;
using static JW.Core.EventBus.InMemoryEventBusSubscriptionsManager;

namespace JW.Core.EventBus
{
    /// <summary>
    /// 事件总线订阅管理接口
    /// </summary>
    public interface IEventBusSubscriptionsManager
    {
        /// <summary>
        /// 是否为空
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        /// 移除事件
        /// </summary>
        event EventHandler<string> OnEventRemoved;

        /// <summary>
        /// 添加动态订阅
        /// </summary>
        /// <typeparam name="TH">handle</typeparam>
        /// <param name="eventName">事件名称</param>
        void AddDynamicSubscription<TH>(string eventName)
           where TH : IDynamicIntegrationEventHandler;

        /// <summary>
        /// 添加订阅
        /// </summary>
        /// <typeparam name="T">集成事件Model类</typeparam>
        /// <typeparam name="TH">Handle</typeparam>
        void AddSubscription<T, TH>()
           where T : IntegrationEvent
           where TH : IIntegrationEventHandler<T>;

        /// <summary>
        /// 移除订阅
        /// </summary>
        /// <typeparam name="T">集成事件Model类</typeparam>
        /// <typeparam name="TH">handle</typeparam>
        void RemoveSubscription<T, TH>()
             where TH : IIntegrationEventHandler<T>
             where T : IntegrationEvent;

        /// <summary>
        /// 移除动态订阅
        /// </summary>
        /// <typeparam name="TH">Handle</typeparam>
        /// <param name="eventName">事件名称</param>
        void RemoveDynamicSubscription<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        /// <summary>
        /// 判断是否存在订阅事件
        /// </summary>
        /// <typeparam name="T">集成事件Model类</typeparam>
        /// <returns></returns>
        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;

        /// <summary>
        /// 根据名称判断是否存在订阅事件
        /// </summary>
        /// <param name="eventName">集成事件Model类</param>
        /// <returns></returns>
        bool HasSubscriptionsForEvent(string eventName);

        /// <summary>
        /// 根据事件名称获取事件类型名称
        /// </summary>
        /// <param name="eventName">事件名称</param>
        /// <returns></returns>
        Type GetEventTypeByName(string eventName);

        /// <summary>
        /// 清理所有
        /// </summary>
        void Clear();

        /// <summary>
        /// 获取订阅信息集合
        /// </summary>
        /// <typeparam name="T">集成事件Model类</typeparam>
        /// <returns></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;

        /// <summary>
        /// 根据名称获取订阅信息集合
        /// </summary>
        /// <param name="eventName"></param>
        /// <returns></returns>
        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        /// <summary>
        /// 获取事件键
        /// </summary>
        /// <typeparam name="T">集成事件Model类</typeparam>
        /// <returns></returns>
        string GetEventKey<T>();
    }
}