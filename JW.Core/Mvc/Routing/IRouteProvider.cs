using Microsoft.AspNetCore.Routing;

namespace JW.Core.Mvc.Routing
{
    /// <summary>
    /// 路由提供者接口
    /// </summary>
    public interface IRouteProvider
    {
        /// <summary>
        /// 注册路由
        /// </summary>
        /// <param name="routeBuilder">路由提供者</param>
        void RegisterRoutes(IRouteBuilder routeBuilder);

        /// <summary>
        /// 获得路由提供者的优先级
        /// </summary>
        int Priority { get; }
    }
}
