using System.Threading.Tasks;

namespace JW.Core.EventBus
{
    /// <summary>
    /// 集成事件处理程序接口（逆变使你使用的类型可以比泛型参数指定的类型派生程度更小。 这样可以隐式转换实现变体接口的类以及隐式转换委托类型。 引用类型支持泛型类型参数中的协变和逆变，但值类型不支持它们。）
    /// </summary>
    /// <typeparam name="TIntegrationEvent">集成事件Model</typeparam>
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
        where TIntegrationEvent : IntegrationEvent
    {
        Task Handle(TIntegrationEvent @event);
    }

    /// <summary>
    /// 集成事件处理程序接口
    /// </summary>
    public interface IIntegrationEventHandler
    {
    }
}
