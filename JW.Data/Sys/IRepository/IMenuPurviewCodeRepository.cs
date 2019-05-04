using JW.Core.Data.Base;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.Sys.Entity;
using System.Threading.Tasks;

namespace JW.Data.Sys.IRepository
{
    public partial interface IMenuPurviewCodeRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Method

        int Save(TEntity model);

        #endregion

        #region Method Async

        Task<bool> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<BasePagedListModel<MenuPurviewCodeEntity>> GetListByMenuIdAsync(int pageIndex, int pageSize, int menuId = 0);
        Task<BasePagedListModel<MenuPurviewCodeEntity>> GetCanUseListAsync(int pageIndex, int pageSize);

        #endregion
    }
}
