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
    public partial class FriendLinksRepository : BaseRepository<FriendLinksEntity, FriendLinksRepository>, IFriendLinksRepository<FriendLinksEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion

        #region Ctor

        public FriendLinksRepository(ILogger<FriendLinksRepository> logger,
            IConnectionProvider connectionProvider,
            IBasePageListRepository pageListRepository)
            : base(logger, connectionProvider)
        {
            this.pageListRepository = pageListRepository;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns></returns>
        public int Save(FriendLinksEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", model.FL_ID);
                    parameters.Add("name", model.FL_NAME);
                    parameters.Add("title", model.FL_TITLE);
                    parameters.Add("logoUrl", model.FL_LOGOURL);
                    parameters.Add("webUrl", model.FL_WEBURL);
                    parameters.Add("orderId", model.FL_ORDERID);
                    parameters.Add("target", model.FL_TARGET);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_save_C_FRIENDLINKS", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Save(FriendLinksEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Save(FriendLinksEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Save(FriendLinksEntity model)发生Exception，异常信息：{0}", ex);
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
                    string sql = "SELECT ISNULL(MAX(FL_ORDERID),0) FROM [C_FRIENDLINKS]";
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
                    string sql = "UPDATE a SET a.FL_ORDERID=b.orderid FROM [C_FRIENDLINKS] AS a JOIN @dt AS b ON b.id=a.FL_ID";
                    conn.Execute(sql, new { dt = dt.AsTableValuedParameter("dbo.OrderTableType") });
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
                    string sql = "DELETE FROM [C_FRIENDLINKS] WHERE FL_ID in @ids";
                    return await conn.ExecuteAsync(sql, new { ids }) > 0;
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法DeleteByIdsAsync(string ids)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法DeleteByIdsAsync(string ids)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法DeleteByIdsAsync(string ids)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        /// <summary>
        /// 获取友情链接列表
        /// </summary> 
        /// <param name="param">搜索实体</param>
        /// <returns>PageDataModel<FriendLinksEntity></returns>
        public async Task<BasePagedListModel<FriendLinksEntity>> GetListAsync(FriendLinksSearchParam param)
        {
            BasePagedListModel<FriendLinksEntity> list = new BasePagedListModel<FriendLinksEntity>();
            try
            {
                #region 条件与排序

                StringBuilder condition = new StringBuilder("1=1");
                if (param.Name.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND FL_NAME like '%{0}%'", param.Name.FilterSql());
                }
                #endregion

                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = condition.ToString();
                criteria.PageIndex = param.PageIndex;
                criteria.Fields = "*";
                criteria.PageSize = param.PageSize;
                criteria.TableName = "C_FRIENDLINKS";
                criteria.PrimaryKey = "FL_ID";
                if (param.SortName.IsNotNullOrEmpty() && param.SortOrder.IsNotNullOrEmpty())
                {
                    criteria.Sort = $"{param.SortName.FilterSql()} {param.SortOrder.FilterSql()}";
                }
                list = await pageListRepository.GetPageDataAsync<FriendLinksEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListAsync(FriendLinksSearchParam param)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListAsync(FriendLinksSearchParam param)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListAsync(FriendLinksSearchParam param)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        #endregion
    }
}
