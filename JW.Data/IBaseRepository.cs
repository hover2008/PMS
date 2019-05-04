using JW.Domain;
using System.Threading.Tasks;

namespace JW.Data.IRepository
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
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
