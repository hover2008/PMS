﻿namespace JW.Core.Caching
{
    /// <summary>
    /// 表示与缓存相关的缺省值
    /// </summary>
    public static partial class CKCachingDefaults
    {
        /// <summary>
        /// Gets the default cache time in minutes
        /// </summary>
        public static int CacheTime => 60;

        /// <summary>
        /// Gets the key used to store the protection key list to Redis (used with the PersistDataProtectionKeysToRedis option enabled)
        /// </summary>
        public static string RedisDataProtectionKey => "JW.DataProtectionKeys";
    }
}
