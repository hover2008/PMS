using JW.Core.Data.Base;
using JW.Core.Data.Dapper;
using System;
using System.Threading.Tasks;

namespace JW.Data
{
    public interface IBasePageListRepository
    {
        #region Methods Async

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="criteriaModel">PageCriteriaModel</param>
        /// <param name="enum"></param>
        /// <returns></returns>
        Task<BasePagedListModel<T>> GetPageDataAsync<T>(IConnectionProvider connectionProvider, PageCriteriaModel criteriaModel);

        Task<BasePagedListModel<TReturn>> GetPageDataAsync<TFirst, TSecond, TReturn>(IConnectionProvider connectionProvider, Func<TFirst, TSecond, TReturn> map, PageCriteriaModel criteriaModel, string splitOn = "Id");

        #endregion
    }
}
