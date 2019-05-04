using System;

namespace JW.Core.Caching
{
    /// <summary>
    /// 缓存管理器接口
    /// </summary>
    public interface ICacheManager : IDisposable
    {
        /// <summary>
        /// 获取或设置与指定键关联的值
        /// </summary>
        /// <typeparam name="T">缓存项目类型</typeparam>
        /// <param name="key">缓存项的关键字</param>
        /// <returns>与指定密钥关联的缓存值</returns>
        T Get<T>(string key);

        /// <summary>
        /// 将指定的键和对象添加到缓存中
        /// </summary>
        /// <param name="key">缓存项的关键字</param>
        /// <param name="data">缓存的价值</param>
        /// <param name="cacheTime">分钟内缓存时间</param>
        void Set(string key, object data, int cacheTime);

        /// <summary>
        /// 获取一个值，该值指示与指定密钥关联的值是否被缓存
        /// </summary>
        /// <param name="key">缓存项的关键字</param>
        /// <returns>如果项已在缓存中，则为true；否则为false</returns>
        bool IsSet(string key);

        /// <summary>
        /// 从缓存中移除具有指定键的值
        /// </summary>
        /// <param name="key">缓存项的关键字</param>
        void Remove(string key);

        /// <summary>
        /// 按键模式移除项
        /// </summary>
        /// <param name="pattern">字符串键模式</param>
        void RemoveByPattern(string pattern);

        /// <summary>
        /// 清除所有缓存数据
        /// </summary>
        void Clear();
    }
}
