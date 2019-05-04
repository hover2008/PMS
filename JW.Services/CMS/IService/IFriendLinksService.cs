using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.CMS.RequestParam;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.CMS.IService
{
    public partial interface IFriendLinksService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region FriendLinks

        Messages Save(TEntity model); 
        int GetMaxOrderId();
        Messages UpdateOrderId(IList<int> ids, IList<int> orderids);

        #endregion

        #region

        Task<Messages> DeleteByIdsAsync(IList<int> ids);
        Task<BasePagedListModel<TEntity>> GetListAsync(FriendLinksSearchParam param);

        #endregion
    }
}
