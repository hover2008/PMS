using Microsoft.AspNetCore.Routing;

namespace JW.Core.Mvc.Routing
{
    /// <summary>
    /// 表示层路由发布者
    /// </summary>
    public interface IRoutePublisher
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routeBuilder">路由提供者</param>
        void RegisterRoutes(IRouteBuilder routeBuilder);
    }
}
