using JW.Core;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.ResposneEntity;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Data.CMS.IRepository
{
    public partial interface IDictionaryRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        int Add(TEntity model);
        int Modify(TEntity model);  
        int ChangeSort(int id, MoveType moveType);

        #endregion

        #region Methods Async

        Task<String> GetNameByIdAsync(int id);
        Task<IEnumerable<DictionaryEntity>> GetListByPIdAsync(int pid);
        Task<bool> UpdateDisabledByIdAsync(int id, bool disabled);
        Task<IEnumerable<DictionaryEntity>> GetAllListAsync();
        Task<IEnumerable<DictionaryEntity>> GetSubsetListByIdAsync(int id);
        Task<IEnumerable<SelectDictionaryEntity>> GetSelectCanUseListAsync(int id);

        #endregion
    }
}
