using JW.Core.Configuration;
using JW.Core.Helper.Cookies;
using JW.Core.Infrastructure;
using JW.Web.Framework.UEditor;
using JW.Web.Manage.Infrastructure.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Net.Http.Headers;
using System.Linq;

namespace JW.Web.Manage.Infrastructure
{
    /// <summary>
    /// 表示在应用程序启动时配置公共特性和中间件的对象
    /// </summary>
    public class CKCommonStartup : ICKStartup
    {
        /// <summary>
        /// 添加和配置任何中间件
        /// </summary>
        /// <param name="services">服务描述符的集合</param>
        /// <param name="configuration">应用程序的配置根</param>
        public void ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        { 
            //add response compression
            services.AddResponseCompression();

            //add options feature
            services.AddOptions();

            //add memory cache
            services.AddMemoryCache();

            //add distributed memory cache
            services.AddDistributedMemoryCache();

            //add cookie manager 
            services.AddCookieManager();

            //add HTTP sesion state feature
            services.AddHttpSession();

            //add anti-forgery
            services.AddAntiForgery();

            //add localization
            services.AddLocalization();

            //add response caching
            services.AddResponseCaching();

            //add UEditor
            services.AddUEditor();

            //add RabbitMQ 
            //TODO 需要就把注释去掉，否则保留注释
            //services.AddRabbitMQManager();
            //services.AddTransient<LogIntegrationEventHandler>();
        }

        /// <summary>
        /// 配置添加中间件的使用
        /// </summary>
        /// <param name="application">用于配置应用程序请求管道的构建器</param>
        public void Configure(IApplicationBuilder application)
        { 
            var config = EngineContext.Current.Resolve<CKConfig>();
            var fileProvider = EngineContext.Current.Resolve<ICKFileProvider>();
            //compression
            if (config.UseResponseCompression)
            {
                //gzip by default
                application.UseResponseCompression();
            }
            //static files
            application.UseStaticFiles(new StaticFileOptions
            {
                //TODO duplicated code (below)
                OnPrepareResponse = ctx =>
                {
                    if (!string.IsNullOrEmpty(config.StaticFilesCacheControl))
                        ctx.Context.Response.Headers.Append(HeaderNames.CacheControl, config.StaticFilesCacheControl);
                }
            }); 
           
            //use HTTP session
            application.UseSession();

            //use request localization
            application.UseRequestLocalization();

            //use response caching
            application.UseResponseCaching();

            //use eventbus
            //TODO 需要就把注释去掉，否则保留注释
            //application.UseEventBus();
        }

        /// <summary>
        /// 获得启动配置实现的排序
        /// </summary>
        //TODO 在错误处理程序之后，应该加载公共服务
        public int Order => 100; 
    }
}
