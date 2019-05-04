using Autofac;
using JW.Core.Caching;
using JW.Core.Captch;
using JW.Core.Configuration;
using JW.Core.Encrypt;
using JW.Core.Helper;
using JW.Core.Infrastructure;
using JW.Core.Infrastructure.DependencyManagement;
using JW.Core.Mvc.Routing;
using JW.Core.ResponseResult;
using JW.Web.Framework.Strategy;
using System.Reflection;

namespace JW.Web.Manage.Infrastructure
{
    /// <summary>
    /// Dependency registrar
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        /// <summary>
        /// Register services and interfaces
        /// </summary>
        /// <param name="builder">Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">Config</param>
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder, CKConfig config)
        {
            //file provider
            builder.RegisterType<CKFileProvider>().As<ICKFileProvider>().InstancePerLifetimeScope();
            //web helper
            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();
            //verify code
            builder.RegisterType<VerifyCode>().As<IVerifyCode>().InstancePerLifetimeScope();
            //workcontext
            builder.RegisterType<WorkContext>().As<IWorkContext>().InstancePerLifetimeScope();
            //Encrypt
            builder.RegisterType<AESEncrypt>().SingleInstance();
            builder.RegisterType<DESEncrypt>().SingleInstance();
            //operation messages 
            builder.RegisterType<Messages>().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(MessagesData<>)).InstancePerLifetimeScope();
            //ck cache
            builder.RegisterModule(new CKCacheModule(config));
            //仓储和服务接口注册
            var Repository = Assembly.Load("JW.Data");
            var Services = Assembly.Load("JW.Services");

            //根据名称约定（数据访问层的接口和实现均以Repository结尾），实现数据访问接口和数据访问实现的依赖
            builder.RegisterAssemblyTypes(Repository)
              .Where(t => t.Name.EndsWith("Repository"))
              .AsImplementedInterfaces().InstancePerLifetimeScope();

            //根据名称约定（服务层的接口和实现均以Service结尾），实现服务接口和服务实现的依赖
            builder.RegisterAssemblyTypes(Services)
              .Where(t => t.Name.EndsWith("Service"))
              .AsImplementedInterfaces().InstancePerLifetimeScope();
            //route publisher
            builder.RegisterType<RoutePublisher>().As<IRoutePublisher>().SingleInstance(); 
            //上传功能策略注册
            builder.RegisterType<UploadStrategy>().As<IUploadStrategy>().InstancePerLifetimeScope();

        }

        /// <summary>
        /// Gets order of this dependency registrar implementation
        /// </summary>
        public int Order => 0; 
    }
}
