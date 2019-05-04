using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.RequestParam;
using JW.Domain.Shared;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.CMS.IService
{
    public partial interface IContentService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Method

        Messages Save(TEntity model, UserClaimModel userClaim);
        int GetMaxOrderId(int catid);
        Messages UpdateOrderId(IList<int> ids, IList<int> orderids);

        #endregion

        #region Method Async

        Task UpdateHitsByIdAsync(int id);
        Task<Messages> DeleteByIdsAsync(IList<int> ids);
        Task<Messages> SetByIdsAsync(IList<int> ids, string action, int steps, int wfid);
        Task<BasePagedListModel<Content2StatusNameEntity>> GetListAsync(ContentSearchParam param);

        #endregion
    }
}
