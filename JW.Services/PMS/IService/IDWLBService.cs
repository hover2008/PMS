using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.PMS.ResposneEntity;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.PMS.IService
{
    public partial interface IDWLBService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    { 
        #region Model

        Messages Save(TEntity model);  

        #endregion

        #region Methods Async

        Task<Messages> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<IEnumerable<TEntity>> GetAllListAsync();
        Task<IEnumerable<SelectDWLBEntity>> GetSelectCanUseListAsync(int id);
        Task<IEnumerable<SelectDWLBEntity>> GetSelectCanUseListAsync();

        #endregion
    }
}
