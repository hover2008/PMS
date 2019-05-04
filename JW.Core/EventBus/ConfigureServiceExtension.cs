using Autofac;
using JW.Core.Configuration;
using JW.Core.EventBus.RabbitMQ;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace JW.Core.EventBus
{
    /// <summary>
    /// 事件总线配置服务扩展方法
    /// </summary>
    public static class ConfigureServiceExtension
    {
        public static IServiceCollection AddRabbitMQManager(this IServiceCollection services)
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }
            
            services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                {
                    var config = sp.GetRequiredService<RabbitMQConfig>();
                    var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                    return new DefaultRabbitMQPersistentConnection(config, logger);
                });

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                { 
                    var config = sp.GetRequiredService<RabbitMQConfig>();
                    var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                    var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                    var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                    var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();

                    return new EventBusRabbitMQ(config, rabbitMQPersistentConnection, logger, eventBusSubcriptionsManager, iLifetimeScope, config.SubscriptionClientName);
                });

            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            return services;
        }
    }
}
