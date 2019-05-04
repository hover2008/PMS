using JW.Core.Data.Base;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.Sys.RequestParam;
using JW.Domain.Sys.ResposneEntity;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Data.Sys.IRepository
{
    public partial interface IRoleRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        int Save(TEntity model);  
        int GetMaxOrderId();
        void UpdateOrderId(DataTable dt); 

        #endregion

        #region Methods Async

        Task<String> GetPermissionByRoleIdAsync(int roleId);
        Task<bool> DeleteByIdsAsync(IList<int> ids);
        Task<BasePagedListModel<TEntity>> GetListAsync(RoleSearchParam param);
        Task<IEnumerable<SelectRoleEntity>> GetSelectListAsync();

        #endregion
    }
}
