using System.Threading.Tasks;

namespace JW.Core.EventBus
{
    /// <summary>
    /// 动态集成事件处理程序接口
    /// </summary>
    public interface IDynamicIntegrationEventHandler
    {
        Task Handle(dynamic eventData);
    }
}
