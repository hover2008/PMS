using JW.Domain.Sys.ResposneEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.Sys.IService
{
    /// <summary>
    /// 后台系统用户缓存服务接口
    /// </summary>
    public interface IUserCacheService
    {
        /// <summary>
        /// 设置当前登录用户权限缓存
        /// </summary>
        /// <param name="userId">用户编号</param>
        Task SetPermissionCacheAsync(int userId);

        /// <summary>
        /// 获取当前登录用户权限缓存
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        Task<IList<UserPermissionCodeEntity>> GetPermissionCacheAsync(int userId);

        /// <summary>
        /// 清除当前登录用户缓存
        /// </summary>
        void Clear();
    }
}
