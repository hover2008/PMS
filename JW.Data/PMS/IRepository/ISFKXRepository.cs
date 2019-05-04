using JW.Core.Data.Base;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.PMS.RequestParam;
using JW.Domain.PMS.ResposneEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Data.PMS.IRepository
{
    public partial interface ISFKXRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        int Save(TEntity model); 

        #endregion

        #region Methods Async

        Task<bool> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<BasePagedListModel<TEntity>> GetListAsync(SFKXSearchParam param);
        Task<IEnumerable<SelectSFKXEntity>> GetSelectCanUseListAsync();

        #endregion
    }
}
