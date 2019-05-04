using Autofac;
using JW.Core.Configuration;

namespace JW.Core.Caching
{
    /// <summary>
    /// 缓存模块IOC
    /// </summary>
    public class CKCacheModule : Autofac.Module
    {
        public CKConfig config { get; }

        public CKCacheModule(CKConfig config)
        {
            this.config = config;

        }
        protected override void Load(ContainerBuilder builder)
        {
            //cache manager
            builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().InstancePerLifetimeScope();

            //static cache manager
            if (config.RedisCachingEnabled)
            {
                builder.RegisterType<RedisConnectionWrapper>()
                    .As<ILocker>()
                    .As<IRedisConnectionWrapper>()
                    .SingleInstance();
                builder.RegisterType<RedisCacheManager>().As<IStaticCacheManager>().InstancePerLifetimeScope();
            }
            else
            {
                builder.RegisterType<MemoryCacheManager>()
                    .As<ILocker>()
                    .As<IStaticCacheManager>()
                    .SingleInstance();
            }
        }
    }
}
