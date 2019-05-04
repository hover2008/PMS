using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Domain;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.Sys.IService
{
    public partial interface IMenuService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        Messages Save(TEntity model);  

        #endregion

        #region Methods Async

        Task<Messages> SetAsync(int id, string action);
        Task<IEnumerable<TEntity>> GetListByRolesAsync(string roles);
        Task<BasePagedListModel<TEntity>> GetAllListAsync(int menuId);
        Task<BasePagedListModel<TEntity>> GetCanUseListAsync();

        #endregion
    }
}
