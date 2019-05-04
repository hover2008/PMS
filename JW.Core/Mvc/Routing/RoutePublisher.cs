using JW.Core.Infrastructure;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;

namespace JW.Core.Mvc.Routing
{
    /// <summary>
    /// 表示层实现路由发布者类
    /// </summary>
    public class RoutePublisher : IRoutePublisher
    {
        #region Fields

        /// <summary>
        /// Type finder
        /// </summary>
        protected readonly ITypeFinder typeFinder;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="typeFinder">Type finder</param>
        public RoutePublisher(ITypeFinder typeFinder)
        {
            this.typeFinder = typeFinder;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routeBuilder">路由提供者</param>
        public virtual void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            //查找由其他程序集提供的路由提供者
            var routeProviders = typeFinder.FindClassesOfType<IRouteProvider>();

            //创建和排序路由提供者的实例
            var instances = routeProviders
                .Select(routeProvider => (IRouteProvider)Activator.CreateInstance(routeProvider))
                .OrderByDescending(routeProvider => routeProvider.Priority);

            //注册所有路由提供者
            foreach (var routeProvider in instances)
                routeProvider.RegisterRoutes(routeBuilder);
        }

        #endregion
    }
}
