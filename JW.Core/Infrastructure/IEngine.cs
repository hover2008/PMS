using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace JW.Core.Infrastructure
{
    /// <summary>
    /// 实现这个接口的类可以作为组成引擎的各种服务的门户
    /// 编辑功能、模块和实现通过这个接口访问大多数功能
    /// </summary>
    public interface IEngine
    {
        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="services">服务描述符的集合</param>
        void Initialize(IServiceCollection services);

        /// <summary>
        /// 添加和配置服务
        /// </summary>
        /// <param name="services">服务描述符的集合</param>
        /// <param name="configuration">应用程序的配置根</param>
        /// <returns>服务提供者</returns>
        IServiceProvider ConfigureServices(IServiceCollection services, IConfigurationRoot configuration);

        /// <summary>
        /// 配置HTTP请求管道
        /// </summary>
        /// <param name="application">用于配置应用程序请求管道的生成器</param>
        void ConfigureRequestPipeline(IApplicationBuilder application);

        /// <summary>
        /// 解析依赖关系（泛型）
        /// </summary>
        /// <typeparam name="T">解析服务的类型</typeparam>
        /// <returns>解析服务</returns>
        T Resolve<T>() where T : class;

        /// <summary>
        /// 解析依赖关系
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <returns>解析服务</returns>
        object Resolve(Type type);

        /// <summary>
        /// 解析依赖关系
        /// </summary>
        /// <typeparam name="T">解析服务的类型</typeparam>
        /// <returns>解析服务集合</returns>
        IEnumerable<T> ResolveAll<T>();

        /// <summary>
        /// 解析未注册服务
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <returns>解析服务</returns>
        object ResolveUnregistered(Type type);
    }
}
