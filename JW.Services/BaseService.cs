using JW.Data.IRepository;
using JW.Domain;
using System.Threading.Tasks;

namespace JW.Services.IService
{
    public class BaseService<TEntity, IRepository> : IBaseService<TEntity>
        where TEntity : BaseEntity
        where IRepository : IBaseRepository<TEntity>
    {
        #region Fields

        private readonly IRepository repository;

        #endregion Fields

        #region Ctor

        public BaseService(IRepository repository)
        {
            this.repository = repository;
        }

        #endregion

        #region Methods


        #endregion Methods

        #region Methods Async

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public async Task<TEntity> GetModelByIdAsync(int id)
        {
            if (id <= 0)
                return null;
            return await repository.GetModelByIdAsync(id);
        }

        public async Task<bool> AddAsync(TEntity model)
        {
            return await repository.AddAsync(model);
        }

        #endregion Methods Async

    }
}
