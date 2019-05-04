using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.CMS.RequestParam;
using JW.Domain.Shared;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.CMS.IService
{
    public partial interface IAdvertService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        Messages Save(TEntity model, UserClaimModel userClaim);  
        int GetMaxOrderId(int typeId);
        Messages UpdateOrderId(IList<int> ids, IList<int> orderids);

        #endregion

        #region Methods Async

        Task<Messages> DeleteByIdsAsync(IList<int> ids);
        Task<Messages> UpdateStateByIdAsync(int id, byte state);
        Task<BasePagedListModel<TEntity>> GetListAsync(AdvertSearchParam param);

        #endregion
    }
}
