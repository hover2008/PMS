using JW.Core.Data.Base;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Data.Sys.IRepository
{
    public partial interface IRoleUserRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    { 
        #region Method 


        #endregion

        #region Method Async 

        Task<IEnumerable<string>> GetRoleNameListByUserIdAsync(int userId);
        Task<bool> AddByDataTableAsync(DataTable dt);
        Task<bool> DeleteByIdsAsync(int roleId, IList<int> userIds);
        Task<BasePagedListModel<Role2UserEntity>> GetListByRoleIdAsync(RoleUserSearchParam param);

        #endregion
    }
}
