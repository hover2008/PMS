using JW.Core.Configuration;
using JW.Core.Data.Dapper;
using JW.Core.Infrastructure;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using System;

namespace JW.Web.Manage.Infrastructure.Extensions
{
    /// <summary>
    /// IServiceCollection的扩展方法
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 向应用程序添加服务并配置服务提供商
        /// </summary>
        /// <param name="services">服务描述符的集合</param>
        /// <param name="configuration">应用程序的配置根</param>
        /// <returns>配置服务提供商</returns>
        public static IServiceProvider ConfigureApplicationServices(this IServiceCollection services, IConfigurationRoot configuration)
        {
            //add SqlServer connection configuration parameters
services.AddDapperConnectionProvider<SqlServerConnectionProvider>(configuration.GetSection("ConnectionStrings"));
            //add CKConfig configuration parameters
            services.ConfigureStartupConfig<CKConfig>(configuration.GetSection("CK"));
            //add hosting configuration parameters
            services.ConfigureStartupConfig<HostingConfig>(configuration.GetSection("Hosting"));
            //add project configuration parameters
            services.ConfigureStartupConfig<ProjectConfig>(configuration.GetSection("Project"));
            //add rabbitMQ configuration parameters
            //services.ConfigureStartupConfig<RabbitMQConfig>(configuration.GetSection("RabbitMQ"));
            //add sysmanage configuration parameters
            services.ConfigureStartupConfig<SysManageSecurityConfig>(configuration.GetSection("SysManageSecurity"));
            //add accessor to HttpContext
            services.AddHttpContextAccessor();
            //create, initialize and configure the engine
            var engine = EngineContext.Create();
            engine.Initialize(services);
            var serviceProvider = engine.ConfigureServices(services, configuration);

            return serviceProvider;
        }

        /// <summary>
        /// 创建、绑定和注册为指定配置参数的服务
        /// </summary>
        /// <typeparam name="TConfig">配置参数</typeparam>
        /// <param name="services">服务描述符的集合</param>
        /// <param name="configuration">键/值应用程序配置属性集</param>
        /// <returns>配置参数实例</returns>
        public static TConfig ConfigureStartupConfig<TConfig>(this IServiceCollection services, IConfiguration configuration) where TConfig : class, new()
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            if (configuration == null)
                throw new ArgumentNullException(nameof(configuration));

            //创建配置实例
            var config = new TConfig();

            //将其绑定到配置的适当部分
            configuration.Bind(config);

            //并将其注册为服务
            services.AddSingleton(config);

            return config;
        }

        /// <summary>
        /// Register DbConnection Provider
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection AddDapperConnectionProvider<T>(this IServiceCollection services, IConfigurationSection configuration)
            where T : class, IConnectionProvider
        {
            services.Configure<ConnectionStringList>(configuration);
            services.AddSingleton<IConnectionProvider, T>(); //也可以改用Autofac注入
            return services;
        }

        /// <summary>
        /// Register HttpContextAccessor
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        /// <summary>
        /// Adds services required for anti-forgery support
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddAntiForgery(this IServiceCollection services)
        {
            //override cookie name
            services.AddAntiforgery(options =>
            {
                var projectConfig = EngineContext.Current.Resolve<ProjectConfig>();
                options.Cookie.Name = projectConfig.CookieNamePrefix + "Antiforgery";
            });
        }

        /// <summary>
        /// Adds services required for application session state
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddHttpSession(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                var projectConfig = EngineContext.Current.Resolve<ProjectConfig>();
                options.Cookie.Name = projectConfig.CookieNamePrefix + "Session";
                options.IdleTimeout = TimeSpan.FromMinutes(projectConfig.CookieExpire);
            });
        }

        /// <summary>
        /// Add and configure authentication for the application
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        public static void AddCKAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Login";

                    });
        }

        /// <summary>
        /// Add and configure MVC for the application
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <returns>A builder for configuring MVC services</returns>
        public static IMvcBuilder AddCKMvc(this IServiceCollection services)
        {
            //add basic MVC feature
            var mvcBuilder = services.AddMvc();

            //use session temp data provider
            mvcBuilder.AddSessionStateTempDataProvider();

            //MVC now serializes JSON with camel case names by default, use this code to avoid it
            mvcBuilder.AddJsonOptions(options =>
            {
                options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                options.SerializerSettings.DateFormatString = "yyyy-MM-dd HH:mm:ss";
            });

            return mvcBuilder;
        } 
    }
}