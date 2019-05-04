using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.Shared;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.Sys.IService
{
    public partial interface IRoleUserService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Method  


        #endregion

        #region Method Async

        Task<IEnumerable<string>> GetRoleNameListByUserIdAsync(int userId);
        Task<Messages> AddByUserIdsAsync(int roleId, List<int> userIds, UserClaimModel userClaim); 
        Task<Messages> DeleteByIdsAsync(int roleId, IList<int> userIds, UserClaimModel userClaim);
        Task<BasePagedListModel<Role2UserEntity>> GetListByRoleIdAsync(RoleUserSearchParam param);

        #endregion
    }
}
