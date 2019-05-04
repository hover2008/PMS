using Autofac;
using JW.Core.Configuration;

namespace JW.Core.Infrastructure.DependencyManagement
{
    /// <summary>
    /// 依赖注册接口
    /// </summary>
    public interface IDependencyRegistrar
    {
        /// <summary>
        /// 注册服务和接口
        /// </summary>
        /// <param name="builder">Autofac Container builder</param>
        /// <param name="typeFinder">Type finder</param>
        /// <param name="config">CKConfig</param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder, CKConfig config);

        /// <summary> 
        /// 获取依赖注册实现排序值
        /// </summary>
        int Order { get; }
    }
}
