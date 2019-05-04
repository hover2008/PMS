using JW.Core.Data.Base;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Data.Sys.IRepository
{
    public partial interface ILogRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods 

        #endregion

        #region Methods Async

        /// <summary>
        /// 删除多个数据
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        Task<bool> DeleteByIdsAsync(IList<int> ids);

        Task<BasePagedListModel<LogEntity>> GetListAsync(LogSearchParam param);

        #endregion
    }
}
