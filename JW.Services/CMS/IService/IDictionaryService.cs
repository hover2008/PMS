using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.ResposneEntity;
using JW.Services.IService;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.CMS.IService
{
    public partial interface IDictionaryService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        Messages Save(TEntity model);  

        #endregion

        #region Methods Async

        Task<Messages> SetAsync(int id, string action);
        Task<String> GetNameByIdAsync(int id);
        Task<IEnumerable<DictionaryEntity>> GetListByPIdAsync(int pid);
        Task<IEnumerable<TEntity>> GetAllListAsync(); 
        Task<IEnumerable<TEntity>> GetSubsetListByIdAsync(int id);
        Task<IEnumerable<SelectDictionaryEntity>> GetSelectCanUseListAsync(int id);

        #endregion
    }
}
