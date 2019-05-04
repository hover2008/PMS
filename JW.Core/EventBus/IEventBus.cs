namespace JW.Core.EventBus
{
    /// <summary>
    /// 事件总线接口
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// 发布
        /// </summary>
        /// <param name="event">集成事件Model</param>
        void Publish(IntegrationEvent @event);

        /// <summary>
        /// 订阅
        /// </summary>
        /// <typeparam name="T">集成事件Model</typeparam>
        /// <typeparam name="TH">handle</typeparam>
        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        /// <summary>
        /// 订阅动态
        /// </summary>
        /// <typeparam name="TH">handle</typeparam>
        /// <param name="eventName">事件名</param>
        void SubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        /// <summary>
        /// 取消订阅动态
        /// </summary>
        /// <typeparam name="TH">handle</typeparam>
        /// <param name="eventName">事件名</param>
        void UnsubscribeDynamic<TH>(string eventName)
            where TH : IDynamicIntegrationEventHandler;

        /// <summary>
        /// 取消订阅
        /// </summary>
        /// <typeparam name="T">集成事件Model</typeparam>
        /// <typeparam name="TH">handle</typeparam>
        void Unsubscribe<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;
    }
}
