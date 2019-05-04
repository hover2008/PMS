using Dapper;
using JW.Core.Data.Base;
using JW.Core.Data.Dapper;
using JW.Core.Extensions;
using JW.Data.CMS.IRepository;
using JW.Data.Repository;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.RequestParam;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace JW.Data.CMS.Repository
{
    /// <summary>
    /// 广告
    /// </summary>
    public partial class AdvertRepository : BaseRepository<AdvertEntity, AdvertRepository>, IAdvertRepository<AdvertEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion 

        #region Ctor

        public AdvertRepository(ILogger<AdvertRepository> logger,
            IConnectionProvider connectionProvider,
            IBasePageListRepository pageListRepository)
            :base(logger,connectionProvider)
        {
            this.pageListRepository = pageListRepository;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int Save(AdvertEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", model.A_ID);
                    parameters.Add("@typeid", model.A_TYPEID);
                    parameters.Add("@state", model.A_STATE);
                    parameters.Add("@st", model.A_STARTTIME);
                    parameters.Add("@et", model.A_ENDTIME);
                    parameters.Add("@title", model.A_TITLE);
                    parameters.Add("@url", model.A_URL);
                    parameters.Add("@picurl", model.A_PicUrl);
                    parameters.Add("@orderid", model.A_ORDERID);
                    parameters.Add("@addman", model.A_AddMan);
                    parameters.Add("@summary", model.A_SUMMARY);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_save_C_ADVERT", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Save(AdvertEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Save(AdvertEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Save(AdvertEntity model)发生Exception，异常信息：{0}", ex);
            }
            return -1;
        } 

        /// <summary>   
        /// 获取最大排序编号
        /// </summary>
        /// <returns></returns>
        public int GetMaxOrderId(int typeId)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT MAX(A_ORDERID) FROM [C_ADVERT] WHERE A_TYPEID=@typeid";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@typeid", typeId);
                    return conn.ExecuteScalar<int>(sql, parameters) + 1;
                } 
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetMaxOrderId(int typeId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetMaxOrderId(int typeId)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetMaxOrderId(int typeId)发生Exception，异常信息：{0}", ex);
            }
            return 0;
        }

        /// <summary>
        /// 更新排序值
        /// </summary>
        /// <returns></returns>
        public void UpdateOrderId(DataTable dt)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "UPDATE a SET a.A_ORDERID=b.orderid FROM [C_ADVERT] AS a JOIN @dt AS b ON b.id=a.A_ID";
                    conn.Execute(sql, new { dt = dt.AsTableValuedParameter("dbo.OrderTableType") });
                };
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法UpdateOrderId(DataTable dt)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法UpdateOrderId(DataTable dt)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法UpdateOrderId(DataTable dt)发生Exception，异常信息：{0}", ex);
            }
        } 

        #endregion

        #region Method Async

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">用英文逗号隔开字符串编号组，如："6,7,8"</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdsAsync(IList<int> ids)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "DELETE FROM [C_ADVERT] WHERE A_ID in @ids";
                    return await conn.ExecuteAsync(sql, new { ids }) > 0;
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法DeleteByIdsAsync(IList<int> ids)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法DeleteByIdsAsync(IList<int> ids)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法DeleteByIdsAsync(IList<int> ids)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        /// <summary>
        /// 设置状态
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="state">状态值</param>
        /// <returns></returns>
        public async Task<bool> UpdateStateByIdAsync(int id, byte state)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "UPDATE [C_ADVERT] SET A_STATE=@state WHERE A_ID=@id";
                    return await conn.ExecuteAsync(sql, new { state, id }) > 0;
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法UpdateStateByIdAsync(int id, byte state)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法UpdateStateByIdAsync(int id, byte state)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法UpdateStateByIdAsync(int id, byte state)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        /// <summary>
        /// 获取广告列表
        /// </summary> 
        /// <param name="param">搜索实体</param>
        /// <returns>PageDataModel<AdvertEntity></returns>
        public async Task<BasePagedListModel<AdvertEntity>> GetListAsync(AdvertSearchParam param)
        {
            BasePagedListModel<AdvertEntity> list = new BasePagedListModel<AdvertEntity>();

            try
            {
                #region 条件与排序

                StringBuilder condition = new StringBuilder("1=1");
                if (param.TypeId > 0)
                {
                    condition.AppendFormat(" AND A_TYPEID={0}", param.TypeId);
                }
                if (param.Name.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND A_TITLE like '%{0}%'", param.Name.FilterSql());
                }
                #endregion

                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = condition.ToString();
                criteria.PageIndex = param.PageIndex;
                criteria.Fields = "*";
                criteria.PageSize = param.PageSize;
                criteria.TableName = "C_ADVERT";
                criteria.PrimaryKey = "A_ID";
                if (param.SortName.IsNotNullOrEmpty() && param.SortOrder.IsNotNullOrEmpty())
                {
                    criteria.Sort = $"{param.SortName.FilterSql()} {param.SortOrder.FilterSql()}";
                }
                list = await pageListRepository.GetPageDataAsync<AdvertEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法UpdateStateByIdAsync(int id, byte state)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法UpdateStateByIdAsync(int id, byte state)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法UpdateStateByIdAsync(int id, byte state)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        #endregion
    }
}