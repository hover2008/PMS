using JW.Core.Data.Base;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.PMS.RequestParam;
using System.Threading.Tasks;

namespace JW.Data.PMS.IRepository
{
    public partial interface IFWXXRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        int Save(TEntity model); 

        #endregion

        #region Methods Async

        Task<bool> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<BasePagedListModel<TEntity>> GetListAsync(FWXXSearchParam param); 

        #endregion
    }
}
