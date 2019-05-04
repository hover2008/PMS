using JW.Core.Data.Base;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.CMS.RequestParam;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Data.CMS.IRepository
{
    public partial interface IAdvertRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Method

        int Save(TEntity model);   
        int GetMaxOrderId(int typeId);
        void UpdateOrderId(DataTable dt); 

        #endregion

        #region Method Async

        Task<bool> DeleteByIdsAsync(IList<int> ids);
        Task<bool> UpdateStateByIdAsync(int id, byte state);
        Task<BasePagedListModel<TEntity>> GetListAsync(AdvertSearchParam param);

        #endregion

    }
}
