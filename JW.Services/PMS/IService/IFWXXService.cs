using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.PMS.RequestParam;
using JW.Services.IService;
using System.Threading.Tasks;

namespace JW.Services.PMS.IService
{
    public partial interface IFWXXService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    { 
        #region Model

        Messages Save(TEntity model);  

        #endregion

        #region Methods Async

        Task<Messages> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<BasePagedListModel<TEntity>> GetListAsync(FWXXSearchParam param);

        #endregion
    }
}
