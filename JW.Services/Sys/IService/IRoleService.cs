using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.Sys.RequestParam;
using JW.Domain.Sys.ResposneEntity;
using JW.Services.IService;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Services.Sys.IService
{
    public partial interface IRoleService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        Messages Save(TEntity model); 
        Messages GetMaxOrderId();
        void UpdateOrderId(DataTable dt);

        #endregion

        #region Methods Async

        Task<Messages> DeleteByIdsAsync(IList<int> ids);
        Task<String> GetPermissionByRoleIdAsync(int roleId);
        Task<BasePagedListModel<TEntity>> GetListAsync(RoleSearchParam param);
        Task<IEnumerable<SelectRoleEntity>> GetSelectListAsync();

        #endregion
    }
}
