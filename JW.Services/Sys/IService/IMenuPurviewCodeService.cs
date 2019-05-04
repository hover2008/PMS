using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.Sys.Entity;
using JW.Services.IService;
using System.Threading.Tasks;

namespace JW.Services.Sys.IService
{
    public partial interface IMenuPurviewCodeService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Method

        Messages Save(MenuPurviewCodeEntity model, string mcode);

        #endregion

        #region Method Async

        Task<Messages> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<BasePagedListModel<TEntity>> GetListByMenuIdAsync(int pageIndex, int pageSize, int menuId = 0);
        Task<BasePagedListModel<TEntity>> GetCanUseListAsync(int pageIndex, int pageSize);

        #endregion 
    }
}
