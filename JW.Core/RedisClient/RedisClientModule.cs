using Autofac;

namespace JW.Core.RedisClient
{
    public class RedisClientModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<RedisClientConnection>()
                    .As<ILocker>()
                    .As<IRedisConnectionWrapper>()
                    .SingleInstance();
            builder.RegisterType<RedisService>().As<IRedisService>().InstancePerLifetimeScope();
        }
    }
}
