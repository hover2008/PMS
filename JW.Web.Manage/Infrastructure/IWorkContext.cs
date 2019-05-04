using JW.Domain.Shared;
using JW.Domain.Sys.Entity;
using System.Threading.Tasks;

namespace JW.Web.Manage.Infrastructure
{
    public interface IWorkContext
    {
        /// <summary>
        /// 用户Key[唯一标识]
        /// </summary>
        string UserKey { get; }

        /// <summary>
        /// 获取当前登录用户编号
        /// </summary>
        /// <returns></returns>
        Task<UserClaimModel> GetCurrentUserClaim();

        /// <summary>
        /// 获取当前登录用户信息
        /// </summary>
        /// <returns></returns>
        Task<UserEntity> GetCurrentUser();
    }
}
