using JW.Core.ResponseResult;
using JW.Domain;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.ResposneEntity;
using JW.Services.IService;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Services.CMS.IService
{
    public partial interface IColumnService<TEntity> : IBaseService<TEntity> where TEntity : BaseEntity
    {
        #region Methods

        Messages Save(TEntity model);

        #endregion

        #region Methods Async

        Task<Messages> SetAsync(int id, string action);
        Task<IEnumerable<SelectColumnEntity>> GetSelectCanUseListAsync(int id);
        Task<IEnumerable<Column2ModelEntity>> GetAllColumn2ModelListAsync();
        Task<IEnumerable<Column2Model2DictionaryEntity>> GetAllColumn2Model2DictionaryListAsync();

        #endregion
    }
}
