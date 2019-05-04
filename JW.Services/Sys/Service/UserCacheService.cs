using JW.Core.Caching;
using JW.Core.Configuration;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.ResposneEntity;
using JW.Services.Sys.IService;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JW.Services.Sys.Service
{
    /// <summary>
    /// 后台系统用户缓存服务
    /// </summary>
    public class UserCacheService : IUserCacheService
    {
        #region Fields

        private readonly ProjectConfig projectConfig;
        private readonly IStaticCacheManager cacheManager;
        private readonly IUserService<UserEntity> userService;

        #endregion

        #region Ctor

        public UserCacheService(ProjectConfig projectConfig,
            IStaticCacheManager cacheManager,
            IUserService<UserEntity> userService)
        {
            this.projectConfig = projectConfig;
            this.cacheManager = cacheManager;
            this.userService = userService;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 用户权限缓存名称
        /// </summary>
        public string UserPermissionCacheName => projectConfig.CacheNamePrefix + "Permission:UserId_";

        #endregion

        #region Methods

        /// <summary>
        /// 设置当前登录用户权限缓存
        /// </summary>
        /// <param name="userId">用户编号</param>
        public async Task SetPermissionCacheAsync(int userId)
        {
            //TODO 缓存权限值，默认120分钟（若要Redis作为缓存，直接更改appsettings.json中节点CK的RedisCachingEnabled为True 
            cacheManager.Set(UserPermissionCacheName + userId, await userService.GetPermissionByUserIdAsync(userId), projectConfig.CacheExpire);
        }

        /// <summary>
        /// 获取当前登录用户权限缓存
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public async Task<IList<UserPermissionCodeEntity>> GetPermissionCacheAsync(int userId)
        {
            var cachedata = cacheManager.Get<IList<UserPermissionCodeEntity>>(UserPermissionCacheName + userId);
            if (cachedata == null)
            {
                var authorizedata = await userService.GetPermissionByUserIdAsync(userId);
                cacheManager.Set(UserPermissionCacheName + userId, authorizedata, projectConfig.CacheExpire);
                cachedata = authorizedata.ToList();
            }
            return cachedata;
        }

        /// <summary>
        /// 清除当前登录用户缓存
        /// </summary>
        public void Clear()
        {
            //清空用户权限缓存
            cacheManager.RemoveByPattern(UserPermissionCacheName);
        }

        #endregion
    }
}
