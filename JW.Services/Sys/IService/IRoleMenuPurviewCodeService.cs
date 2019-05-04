using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.Shared;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.Sys.IService
{
    public partial interface IRoleMenuPurviewCodeService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Method


        #endregion

        #region Method Async

        Task<Messages> BatchSaveAsync(int roleId, IList<string> codes, UserClaimModel userClaim);
        Task<IEnumerable<TEntity>> GetPurviewCodeListByRoleIdAsync(int roleId);

        #endregion
    }
}
