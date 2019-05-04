using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JW.Core.Infrastructure
{
    /// <summary>
    /// 表示在应用程序启动时配置服务和中间件的对象
    /// </summary>
    public interface ICKStartup
    {
        /// <summary>
        /// 添加和配置任何中间件
        /// </summary>
        /// <param name="services">服务描述符的集合</param>
        /// <param name="configuration">应用程序的配置根</param>
        void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration);

        /// <summary>
        /// 配置添加中间件的使用
        /// </summary>
        /// <param name="application">用于配置应用程序请求管道的生成器</param>
        void Configure(IApplicationBuilder application);

        /// <summary>
        /// 获取此启动配置实现的顺序
        /// </summary>
        int Order { get; }
    }
}
