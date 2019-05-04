using System;

namespace JW.Core.EventBus
{
    /// <summary>
    /// 集成事件Model基类
    /// </summary>
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.Now;
        }

        public Guid Id { get; }
        public DateTime CreationDate { get; }
    }
}
