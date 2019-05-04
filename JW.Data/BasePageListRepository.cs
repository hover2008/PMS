using Dapper;
using JW.Core.Data.Base;
using JW.Core.Data.Dapper;
using System;
using System.Data;
using System.Threading.Tasks;

namespace JW.Data
{
    public class BasePageListRepository: IBasePageListRepository
    {
        #region Fields


        #endregion Fields

        #region Ctor

        public BasePageListRepository()
        {
        }

        #endregion Ctor

        #region Methods Async

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <param name="param">可空参数</param>
        /// <returns></returns>
        public async Task<BasePagedListModel<T>> GetPageDataAsync<T>(IConnectionProvider connectionProvider, PageCriteriaModel criteriaModel)
        {
            using (var conn = connectionProvider.CreateConn())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("TableName", criteriaModel.TableName);
                parameters.Add("PrimaryKey", criteriaModel.PrimaryKey);
                parameters.Add("Fields", criteriaModel.Fields);
                parameters.Add("Condition", criteriaModel.Condition);
                parameters.Add("PageIndex", criteriaModel.PageIndex);
                parameters.Add("PageSize", criteriaModel.PageSize);
                parameters.Add("Sort", criteriaModel.Sort);
                parameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);

                BasePagedListModel<T> listModel = new BasePagedListModel<T>()
                {
                    Data = await conn.QueryAsync<T>("sp_get_PageData", parameters, commandType: CommandType.StoredProcedure),
                    Total = parameters.Get<int>("RecordCount")
                };

                return listModel;
            }
        }

        /// <summary>
        /// 获取分页数据
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TReturn">dto</typeparam>
        /// <param name="map">委托函数</param>
        /// <param name="criteriaModel">查询数据Model</param>
        /// <param name="splitOn">表示查询的SQL语句中根据哪个字段进行分割</param>
        /// <returns></returns>
        public async Task<BasePagedListModel<TReturn>> GetPageDataAsync<TFirst, TSecond, TReturn>(IConnectionProvider connectionProvider, Func<TFirst, TSecond, TReturn> map, PageCriteriaModel criteriaModel, string splitOn = "Id")
        {
            using (var conn = connectionProvider.CreateConn())
            {
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("TableName", criteriaModel.TableName);
                parameters.Add("PrimaryKey", criteriaModel.PrimaryKey);
                parameters.Add("Fields", criteriaModel.Fields);
                parameters.Add("Condition", criteriaModel.Condition);
                parameters.Add("PageIndex", criteriaModel.PageIndex);
                parameters.Add("PageSize", criteriaModel.PageSize);
                parameters.Add("Sort", criteriaModel.Sort);
                parameters.Add("RecordCount", dbType: DbType.Int32, direction: ParameterDirection.Output);
                BasePagedListModel<TReturn> listModel = new BasePagedListModel<TReturn>()
                {
                    Data = await conn.QueryAsync("sp_get_PageData", map, parameters, null, true, splitOn, null, commandType: CommandType.StoredProcedure),
                    Total = parameters.Get<int>("RecordCount")
                };
                return listModel;
            }
        }

        #endregion
    }
}
