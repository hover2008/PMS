using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.CMS.RequestParam;
using JW.Domain.CMS.ResposneEntity;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.CMS.IService
{
    public partial interface IModelService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    { 
        #region Model

        Messages Save(TEntity model); 
        int GetMaxOrderId();
        Messages UpdateOrderId(IList<int> ids, IList<int> orderids);

        #endregion

        #region Methods Async

        Task<Messages> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<BasePagedListModel<TEntity>> GetListAsync(ModelSearchParam param);
        Task<IEnumerable<SelectModelEntity>> GetSelectCanUseListAsync();

        #endregion
    }
}
