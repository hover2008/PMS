using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using JW.Core.Configuration;
using JW.Core.Helper;
using JW.Core.Infrastructure.DependencyManagement;
using JW.Core.Infrastructure.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace JW.Core.Infrastructure
{
    /// <summary>
    /// 表示层启动引擎
    /// </summary>
    public class CKEngine : IEngine
    {
        #region Properties

        /// <summary>
        /// 获取或设置服务提供程序
        /// </summary>
        private IServiceProvider _serviceProvider { get; set; }

        #endregion

        #region Utilities

        /// <summary>
        /// Get IServiceProvider
        /// </summary>
        /// <returns>IServiceProvider</returns>
        protected IServiceProvider GetServiceProvider()
        {
            var accessor = ServiceProvider.GetService<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        /// <summary>
        /// 运行启动任务
        /// </summary>
        /// <param name="typeFinder">Type finder</param>
        protected virtual void RunStartupTasks(ITypeFinder typeFinder)
        {
            //查找其他程序集提供的启动任务
            var startupTasks = typeFinder.FindClassesOfType<IStartupTask>();

            //创建和排序启动任务的实例
            var instances = startupTasks
                .Select(startupTask => (IStartupTask)Activator.CreateInstance(startupTask))
                .OrderBy(startupTask => startupTask.Order);

            //执行任务
            foreach (var task in instances)
                task.Execute();
        }

        /// <summary>
        /// 使用Autofac注册依赖关系
        /// </summary>
        /// <param name="ckConfig">启动CKConfig参数</param>
        /// <param name="services">服务描述符的集合</param>
        /// <param name="typeFinder">Type finder</param>
        protected virtual IServiceProvider RegisterDependencies(CKConfig ckConfig, IServiceCollection services, ITypeFinder typeFinder)
        {
            var containerBuilder = new ContainerBuilder();

            //注册引擎
            containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();

            //注册类型查找
            containerBuilder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            //查找由其他程序集提供的依赖性注册
            var dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();

            //创建和排序依赖注册的实例
            var instances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

            //注册所有提供的依赖项
            foreach (var dependencyRegistrar in instances)
                dependencyRegistrar.Register(containerBuilder, typeFinder, ckConfig);

            //使用一组注册的服务描述符填充AutoFac容器生成器
            containerBuilder.Populate(services);

            //创建服务提供商
            _serviceProvider = new AutofacServiceProvider(containerBuilder.Build());
            return _serviceProvider;
        }

        /// <summary>
        /// 注册和配置AutoMapper
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="typeFinder">Type finder</param>
        protected virtual void AddAutoMapper(IServiceCollection services, ITypeFinder typeFinder)
        {
            //查找其他程序集提供的映射器配置
            var mapperConfigurations = typeFinder.FindClassesOfType<IMapperProfile>();

            //创建和排序映射器配置的实例
            var instances = mapperConfigurations
                .Select(mapperConfiguration => (IMapperProfile) Activator.CreateInstance(mapperConfiguration))
                .OrderBy(mapperConfiguration => mapperConfiguration.Order);

            //创建自动配置器配置
            var config = new MapperConfiguration(cfg => {
                foreach (var instance in instances)
                {
                    cfg.AddProfile(instance.GetType());
                }
            });

            //register AutoMapper
            services.AddAutoMapper();

            //register
            AutoMapperConfiguration.Init(config);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 初始化引擎
        /// </summary>
        /// <param name="services">服务描述符的集合</param>
        public void Initialize(IServiceCollection services)
        {
            //目前大多数API提供商都需要TLS 1.2
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            //设置基本应用程序路径
            var provider = services.BuildServiceProvider();
            var hostingEnvironment = provider.GetRequiredService<IHostingEnvironment>(); 
            CommonHelper.DefaultFileProvider = new CKFileProvider(hostingEnvironment);

            //register mvc
            services.AddMvcCore(); 
        }

        /// <summary>
        /// 添加和配置服务
        /// </summary>
        /// <param name="services">服务描述符的集合</param>
        /// <param name="configuration">应用程序的配置根</param>
        /// <returns>服务提供者</returns>
        public IServiceProvider ConfigureServices(IServiceCollection services, IConfigurationRoot configuration)
        {
            //查找其他程序集提供的启动配置
            var typeFinder = new WebAppTypeFinder();
            var startupConfigurations = typeFinder.FindClassesOfType<ICKStartup>();

            //创建和排序启动配置实例
            var instances = startupConfigurations
                .Select(startup => (ICKStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            //配置服务
            foreach (var instance in instances)
                instance.ConfigureServices(services, configuration);

            //寄存器映射器配置
            AddAutoMapper(services, typeFinder);

            //寄存器依赖
            var ckConfig = services.BuildServiceProvider().GetService<CKConfig>();
            RegisterDependencies(ckConfig, services, typeFinder);

            //运行启动任务
            if (!ckConfig.IgnoreStartupTasks)
                RunStartupTasks(typeFinder);

            return _serviceProvider;
        }

        /// <summary>
        /// 配置HTTP请求管道
        /// </summary>
        /// <param name="application">用于配置应用程序请求管道的生成器</param>
        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            //查找其他程序集提供的启动配置
            var typeFinder = Resolve<ITypeFinder>();
            var startupConfigurations = typeFinder.FindClassesOfType<ICKStartup>();

            //创建和排序启动配置实例
            var instances = startupConfigurations
                .Select(startup => (ICKStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            //配置请求管道
            foreach (var instance in instances)
                instance.Configure(application);
        }

        /// <summary>
        /// 解析依赖关系（泛型）
        /// </summary>
        /// <typeparam name="T">解析服务的类型</typeparam>
        /// <returns>解析服务</returns>
        public T Resolve<T>() where T : class
        {
            return (T)GetServiceProvider().GetRequiredService(typeof(T));
        }

        /// <summary>
        /// 解析依赖关系
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <returns>解析服务</returns>
        public object Resolve(Type type)
        {
            return GetServiceProvider().GetRequiredService(type);
        }

        /// <summary>
        /// 解析依赖关系
        /// </summary>
        /// <typeparam name="T">解析服务的类型</typeparam>
        /// <returns>解析服务集合</returns>
        public IEnumerable<T> ResolveAll<T>()
        {
            return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
        }

        /// <summary>
        /// 解析未注册服务
        /// </summary>
        /// <param name="type">服务类型</param>
        /// <returns>解析服务</returns>
        public virtual object ResolveUnregistered(Type type)
        {
            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    //尝试解决构造函数参数
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null)
                            throw new CKException("Unknown dependency");
                        return service;
                    });

                    //一切都好，所以创建实例
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }
            throw new CKException("No constructor was found that had all the dependencies satisfied.", innerException);
        }

        #endregion

        #region Properties

        /// <summary>
        /// 服务提供者
        /// </summary>
        public virtual IServiceProvider ServiceProvider => _serviceProvider;

        #endregion
    }
}
