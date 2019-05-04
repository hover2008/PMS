using Dapper;
using JW.Core.Data.Base;
using JW.Core.Data.Dapper;
using JW.Core.Extensions;
using JW.Data.CMS.IRepository;
using JW.Data.Repository;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.RequestParam;
using JW.Domain.CMS.ResposneEntity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace JW.Data.CMS.Repository
{
    public partial class ModelRepository : BaseRepository<ModelEntity, ModelRepository>, IModelRepository<ModelEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion

        #region Ctor

        public ModelRepository(ILogger<ModelRepository> logger,
            IConnectionProvider connectionProvider,
            IBasePageListRepository pageListRepository)
            : base(logger, connectionProvider)
        {
            this.pageListRepository = pageListRepository;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// 保存一条数据
        /// </summary>
        public int Save(ModelEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", model.M_ID);
                    parameters.Add("name", model.M_NAME);
                    parameters.Add("desc", model.M_DESCRIPTION);
                    parameters.Add("tablename", model.M_TABLENAME);
                    parameters.Add("manageurl", model.M_MANAGEURL);
                    parameters.Add("orderid", model.M_ORDERID);
                    parameters.Add("disabled", model.M_DISABLED);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_save_C_MODEL", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Save(ModelEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Save(ModelEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Save(ModelEntity model)发生Exception，异常信息：{0}", ex);
            }
            return -1;
        }  

        /// <summary>
        /// 获取最大排序编号
        /// </summary>
        /// <returns></returns>
        public int GetMaxOrderId()
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT MAX(M_ORDERID) FROM [C_MODEL]";
                    return conn.ExecuteScalar<int>(sql) + 1;
                } 
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetMaxOrderId()发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetMaxOrderId()发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetMaxOrderId()发生Exception，异常信息：{0}", ex);
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
                    string sql = "UPDATE a SET a.M_ORDERID = b.orderid FROM [C_MODEL] AS a JOIN @dt AS b ON b.id = a.M_ID"; 
                    conn.Execute(sql, new { dt=dt.AsTableValuedParameter("dbo.OrderTableType") });
                } 
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

        #region Methods Async

        /// <summary>
        /// 设置禁用
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="disabled">true-已禁用，false-未禁用</param>
        /// <returns></returns>
        public async Task<bool> UpdateDisabledByIdAsync(int id, bool disabled)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "UPDATE [C_MODEL] SET M_DISABLED=@disabled WHERE M_ID=@id";
                    return await conn.ExecuteAsync(sql, new { disabled, id }) > 0;
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法UpdateDisabledByIdAsync(int id, bool disabled)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法UpdateDisabledByIdAsync(int id, bool disabled)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法UpdateDisabledByIdAsync(int id, bool disabled)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="param">搜索实体</param>
        public async Task<BasePagedListModel<ModelEntity>> GetListAsync(ModelSearchParam param)
        {
            BasePagedListModel<ModelEntity> list = new BasePagedListModel<ModelEntity>();
            try
            {
                #region 条件与排序

                StringBuilder condition = new StringBuilder("1=1");
                if (param.Name.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND M_NAME like '%{0}%'", param.Name.FilterSql());
                }
                #endregion

                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = condition.ToString();
                criteria.PageIndex = param.PageIndex;
                criteria.Fields = "*";
                criteria.PageSize = param.PageSize;
                criteria.TableName = "C_MODEL";
                criteria.PrimaryKey = "M_ID";
                if (param.SortName.IsNotNullOrEmpty() && param.SortOrder.IsNotNullOrEmpty())
                {
                    criteria.Sort = $"{param.SortName.FilterSql()} {param.SortOrder.FilterSql()}";
                }
                list = await pageListRepository.GetPageDataAsync<ModelEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListAsync(ModelSearchParam param)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListAsync(ModelSearchParam param)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListAsync(ModelSearchParam param)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取可用于下拉框选择的Model数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectModelEntity>> GetSelectCanUseListAsync()
        {
            IEnumerable<SelectModelEntity> list = new List<SelectModelEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT M_ID,M_NAME FROM C_MODEL WHERE M_DISABLED=0 ORDER BY M_ORDERID ASC";
                    list = await conn.QueryAsync<SelectModelEntity>(sql);
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetSelectCanUseListAsync()发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetSelectCanUseListAsync()发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetSelectCanUseListAsync()发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        #endregion
    }
}
