using JW.Core.Data.Base;
using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.Sys.Enum;
using JW.Domain.Sys.RequestParam;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.Sys.IService
{
    public partial interface ILogService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Methods
          

        #endregion

        #region Methods Async

        Task<bool> AddLogAsync(string action, string data, int userId, string userName);
        Task AddLogAsync(OperatorLogEnum opType, string data, int userId, string userName);
        Task<Messages> DeleteByIdsAsync(IList<int> ids);
        Task<BasePagedListModel<TEntity>> GetListAsync(LogSearchParam param);

        #endregion
    }
}
