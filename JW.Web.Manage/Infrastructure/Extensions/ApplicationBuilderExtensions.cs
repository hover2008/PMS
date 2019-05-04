using JW.Core.Configuration;
using JW.Core.EventBus;
using JW.Core.Helper;
using JW.Core.Infrastructure;
using JW.Core.Mvc.Routing;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;

namespace JW.Web.Manage.Infrastructure.Extensions
{
    /// <summary>
    /// IApplicationBuilder的扩展方法
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Configure the application HTTP request pipeline
        /// </summary>
        /// <param name="application">Builder for configuring an application's request pipeline</param>
        public static void ConfigureRequestPipeline(this IApplicationBuilder application)
        {
            EngineContext.Current.ConfigureRequestPipeline(application);
        }

        /// <summary>
        /// 添加异常处理
        /// </summary>
        /// <param name="application">用于配置应用程序请求管道的生成器</param>
        public static void UseCKExceptionHandler(this IApplicationBuilder application)
        {
            var ckConfig = EngineContext.Current.Resolve<CKConfig>();
            var hostingEnvironment = EngineContext.Current.Resolve<IHostingEnvironment>();
            var useDetailedExceptionPage = ckConfig.DisplayFullErrorStack || hostingEnvironment.IsDevelopment();
            if (useDetailedExceptionPage)
            {
                //为开发和测试目的获取详细的异常
                application.UseDeveloperExceptionPage();
            }
            else
            {
                //或使用特殊异常处理程序
                application.UseExceptionHandler("/error");
            }

            //log errors
            application.UseExceptionHandler(handler =>
            {
                handler.Run(context =>
                {
                    var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
                    if (exception == null)
                        return Task.CompletedTask;

                    try
                    {
                        //log error 
                    }
                    finally
                    {
                        //重新抛出异常以显示错误页
                        throw exception;
                    }
                });
            });
        }

        /// <summary>
        /// 添加一个特殊的处理程序，它检查没有404个体的状态代码的响应
        /// </summary>
        /// <param name="application">用于配置应用程序请求管道的生成器</param>
        public static void UsePageNotFound(this IApplicationBuilder application)
        {
            application.UseStatusCodePages(async context =>
            {
                //handle 404 Not Found
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status404NotFound)
                {
                    var webHelper = EngineContext.Current.Resolve<IWebHelper>();
                    if (!webHelper.IsStaticResource())
                    {
                        //获取原始路径和查询
                        var originalPath = context.HttpContext.Request.Path;
                        var originalQueryString = context.HttpContext.Request.QueryString;

                        //将原始路径存储在特殊的特性中，以便以后使用它
                        context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(new StatusCodeReExecuteFeature()
                        {
                            OriginalPathBase = context.HttpContext.Request.PathBase.Value,
                            OriginalPath = originalPath.Value,
                            OriginalQueryString = originalQueryString.HasValue ? originalQueryString.Value : null,
                        });

                        //get new path
                        context.HttpContext.Request.Path = "/page-not-found";
                        context.HttpContext.Request.QueryString = QueryString.Empty;

                        try
                        {
                            //用新路径重新执行请求
                            await context.Next(context.HttpContext);
                        }
                        finally
                        {
                            //返回原始路径请求
                            context.HttpContext.Request.QueryString = originalQueryString;
                            context.HttpContext.Request.Path = originalPath;
                            context.HttpContext.Features.Set<IStatusCodeReExecuteFeature>(null);
                        }
                    }
                }
            });
        }

        /// <summary>
        /// 添加一个特殊的处理程序，用400状态代码检查响应（坏请求）
        /// </summary>
        /// <param name="application">用于配置应用程序请求管道的生成器</param>
        public static void UseBadRequestResult(this IApplicationBuilder application)
        {
            application.UseStatusCodePages(context =>
            {
                //handle 400 (Bad request)
                if (context.HttpContext.Response.StatusCode == StatusCodes.Status400BadRequest)
                {
                    //log error
                }

                return Task.CompletedTask;
            });
        }

        /// <summary>
        /// 配置MVC路由
        /// </summary>
        /// <param name="application">用于配置应用程序请求管道的生成器</param>
        public static void UseCKMvc(this IApplicationBuilder application)
        {
            application.UseMvc(routeBuilder =>
            {
                //注册所有路由
                EngineContext.Current.Resolve<IRoutePublisher>().RegisterRoutes(routeBuilder);
            });
        }

        /// <summary>
        /// 使用事件总线
        /// </summary>
        /// <param name="application"></param>
        public static void UseEventBus(this IApplicationBuilder application)
        {
            var eventBus = application.ApplicationServices.GetRequiredService<IEventBus>();
        }
    }
}
