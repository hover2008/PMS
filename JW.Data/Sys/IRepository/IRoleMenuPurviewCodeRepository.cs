using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.Sys.Entity;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Data.Sys.IRepository
{
    public partial interface IRoleMenuPurviewCodeRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Method

        bool BatchSave(DataTable dt, int roleId);

        #endregion

        #region Method Async

        Task<IEnumerable<RoleMenuPurviewCodeEntity>> GetPurviewCodeListByRoleIdAsync(int roleId);

        #endregion
    }
}
