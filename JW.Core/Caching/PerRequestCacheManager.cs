using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace JW.Core.Caching
{
    /// <summary>
    /// 表示在HTTP请求（短期缓存）期间缓存的管理器
    /// </summary>
    public partial class PerRequestCacheManager : ICacheManager
    {
        #region Fields

        private readonly IHttpContextAccessor _httpContextAccessor;

        #endregion

        #region Ctor

        /// <summary>
        /// Gets a key/value collection that can be used to share data within the scope of this request 
        /// </summary>
        public PerRequestCacheManager(IHttpContextAccessor httpContextAccessor)
        {
            this._httpContextAccessor = httpContextAccessor;
        }

        #endregion

        #region Utilities

        /// <summary>
        /// Gets a key/value collection that can be used to share data within the scope of this request 
        /// </summary>
        protected virtual IDictionary<object, object> GetItems()
        {
            return _httpContextAccessor.HttpContext?.Items;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Key of cached item</param>
        /// <returns>The cached value associated with the specified key</returns>
        public virtual T Get<T>(string key)
        {
            var items = GetItems();
            if (items == null)
                return default(T);

            return (T)items[key];
        }

        /// <summary>
        /// Adds the specified key and object to the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <param name="data">Value for caching</param>
        /// <param name="cacheTime">Cache time in minutes</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            var items = GetItems();
            if (items == null)
                return;

            if (data != null)
                items[key] = data;
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">Key of cached item</param>
        /// <returns>True if item already is in cache; otherwise false</returns>
        public virtual bool IsSet(string key)
        {
            var items = GetItems();

            return items?[key] != null;
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">Key of cached item</param>
        public virtual void Remove(string key)
        {
            var items = GetItems();

            items?.Remove(key);
        }

        /// <summary>
        /// Removes items by key pattern
        /// </summary>
        /// <param name="pattern">String key pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
            var items = GetItems();
            if (items == null)
                return;

            //get cache keys that matches pattern
            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var matchesKeys = items.Keys.Select(p => p.ToString()).Where(key => regex.IsMatch(key)).ToList();

            //remove matching values
            foreach (var key in matchesKeys)
            {
                items.Remove(key);
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            var items = GetItems();

            items?.Clear();
        }

        /// <summary>
        /// Dispose cache manager
        /// </summary>
        public virtual void Dispose()
        {
            //nothing special
        }

        #endregion
    }
}
