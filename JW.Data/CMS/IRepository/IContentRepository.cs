using JW.Core.Data.Base;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.RequestParam;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Data.CMS.IRepository
{
    public partial interface IContentRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Method

        int Save(TEntity model); 
        int GetMaxOrderId(int catid);
        void UpdateWeight(DataTable dt); 

        #endregion

        #region Method Async

        Task<bool> UpdateHitsByIdAsync(int id);
        Task<bool> DeleteByIdsAsync(IList<int> ids);
        Task<bool> SetByIdsAsync(IList<int> ids, int flag, string columnName);
        Task<BasePagedListModel<Content2StatusNameEntity>> GetListAsync(ContentSearchParam param);

        #endregion
    }
}
