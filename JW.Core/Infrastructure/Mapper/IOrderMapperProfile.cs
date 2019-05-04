namespace JW.Core.Infrastructure.Mapper
{
    /// <summary>
    /// 映射器注册器接口
    /// </summary>
    public interface IMapperProfile
    {
        /// <summary>
        /// 获取此配置实现的顺序
        /// </summary>
        int Order { get; }
    }
}
