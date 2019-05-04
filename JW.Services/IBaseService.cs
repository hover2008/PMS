using JW.Domain;
using System.Threading.Tasks;

namespace JW.Services.IService
{
    public interface IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Methods
 
         
        #endregion Methods

        #region Methods Async
         
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        Task<TEntity> GetModelByIdAsync(int id);

        Task<bool> AddAsync(TEntity model);

        #endregion Methods Async
    }
}
