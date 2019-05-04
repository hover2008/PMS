using JW.Core;
using JW.Core.Data.Base;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.Sys.Entity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Data.Sys.IRepository
{
    public partial interface IMenuRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    { 
        #region Method

        int Add(TEntity model);
        int Update(TEntity model); 
        bool ChangeSort(int id, MoveType moveType);

        #endregion

        #region Method Async

        Task<bool> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<IEnumerable<TEntity>> GetListByRolesAsync(string roles);
        Task<BasePagedListModel<MenuEntity>> GetAllListAsync(int menuId);
        Task<BasePagedListModel<MenuEntity>> GetCanUseListAsync();

        #endregion
    }
}
