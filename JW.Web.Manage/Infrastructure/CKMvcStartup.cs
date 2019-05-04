using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JW.Core.Infrastructure;
using JW.Web.Manage.Infrastructure.Extensions;

namespace JW.Web.Manage.Infrastructure
{
    /// <summary>
    /// 表示在应用程序启动时配置MVC的对象
    /// </summary>
    public class CKMvcStartup : ICKStartup
    {
        /// <summary>
        /// 添加和配置任何中间件
        /// </summary>
        /// <param name="services">服务描述符的集合</param>
        /// <param name="configuration">应用程序的配置根</param>
        public void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {  
            //add and configure MVC feature
            services.AddCKMvc(); 
        }

        /// <summary>
        /// 配置添加中间件的使用
        /// </summary>
        /// <param name="application">用于配置应用程序请求管道的构建器</param>
        public void Configure(IApplicationBuilder application)
        { 
            //MVC routing
            application.UseCKMvc();
        }

        /// <summary>
        /// 获得启动配置实现的排序
        /// </summary>
        //TODO MVC should be loaded last
        public int Order => 1000; 
    }
}
