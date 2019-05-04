using JW.Core.Data.Base;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.CMS.RequestParam;
using JW.Domain.CMS.ResposneEntity;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace JW.Data.CMS.IRepository
{
    public partial interface IModelRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        int Save(TEntity model); 
        int GetMaxOrderId();
        void UpdateOrderId(DataTable dt);

        #endregion

        #region Methods Async

        Task<bool> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<BasePagedListModel<TEntity>> GetListAsync(ModelSearchParam param);
        Task<IEnumerable<SelectModelEntity>> GetSelectCanUseListAsync();

        #endregion
    }
}
