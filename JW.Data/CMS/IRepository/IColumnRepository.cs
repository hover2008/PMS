using JW.Core;
using JW.Data.IRepository;
using JW.Domain;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.ResposneEntity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Data.CMS.IRepository
{
    public partial interface IColumnRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        int Add(TEntity model);
        int Modify(TEntity model); 
        int ChangeSort(int id, MoveType moveType);

        #endregion

        #region Methods Async

        Task<bool> UpdateDisabledByIdAsync(int id, bool disabled); 
        Task<IEnumerable<SelectColumnEntity>> GetSelectCanUseListAsync(int id);
        Task<IEnumerable<Column2ModelEntity>> GetAllColumn2ModelListAsync();
        Task<IEnumerable<Column2Model2DictionaryEntity>> GetAllColumn2Model2DictionaryListAsync();

        #endregion
    }
}
