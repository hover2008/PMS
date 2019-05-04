namespace JW.Core.Configuration
{
    /// <summary>
    /// RabbitMQ配置
    /// </summary>
    public class RabbitMQConfig
    {
        /// <summary>
        /// 主机名
        /// </summary>
        public string HostName { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 虚拟主机
        /// </summary>
        public string VirtualHost { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 重复次数
        /// </summary>
        public int RetryCount { get; set; }

        /// <summary>
        /// 代理名称
        /// </summary>
        public string BrokerName { get; set; }

        /// <summary>
        /// Autofac范围名称（动态订阅用到）
        /// </summary>
        public string AutofacScopeName { get; set; }

        /// <summary>
        /// 客户端订阅名称
        /// </summary>
        public string SubscriptionClientName { get; set; }
    }
}
