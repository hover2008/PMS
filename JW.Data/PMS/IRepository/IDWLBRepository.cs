using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.PMS.ResposneEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Data.PMS.IRepository
{
    public partial interface IDWLBRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        int Save(TEntity model); 

        #endregion

        #region Methods Async

        Task<bool> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<IEnumerable<TEntity>> GetAllListAsync();
        Task<IEnumerable<SelectDWLBEntity>> GetSelectCanUseListAsync(int id); 
        Task<IEnumerable<SelectDWLBEntity>> GetSelectCanUseListAsync();

        #endregion
    }
}
