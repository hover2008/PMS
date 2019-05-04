using Dapper;
using JW.Core.Data.Base;
using JW.Core.Data.Dapper;
using JW.Core.Extensions;
using JW.Data.Repository;
using JW.Data.Sys.IRepository;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace JW.Data.Sys.Repository
{
    /// <summary>
    /// 系统日志表
    /// </summary>
    public partial class LogRepository : BaseRepository<LogEntity, LogRepository>, ILogRepository<LogEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion

        #region Ctor

        public LogRepository(ILogger<LogRepository> logger,
            IConnectionProvider connectionProvider,
            IBasePageListRepository pageListRepository) 
            : base(logger, connectionProvider)
        {
            this.pageListRepository = pageListRepository;
        }

        #endregion

        #region  Methods


        #endregion

        #region Methods Async

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
                    string sql = "DELETE [S_LOG] WHERE L_ID in @ids";
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
        /// 获取日志列表
        /// </summary> 
        /// <param name="entity">日志搜索实体</param>
        /// <returns>PageDataModel<LogEntity></returns>
        public async Task<BasePagedListModel<LogEntity>> GetListAsync(LogSearchParam param)
        {
            BasePagedListModel<LogEntity> list = new BasePagedListModel<LogEntity>();
            try
            {
                #region 条件与排序

                StringBuilder condition = new StringBuilder("1=1");
                if (param.Keyword.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND L_DATA like '%{0}%'", param.Keyword.FilterSql());
                }
                // 过滤搜索范围
                if (param.Range != "all")
                {
                    DateTime today = DateTime.Now;
                    switch (param.Range)
                    {
                        case "today":
                            condition.AppendFormat(" AND L_TIME>='{0}'", today.ToString("yyyy-MM-dd"));
                            break;
                        case "last3days":
                            condition.AppendFormat(" AND L_TIME>='{0}'", today.AddDays(-3).ToString("yyyy-MM-dd"));
                            break;
                        case "last7days":
                            condition.AppendFormat(" AND L_TIME>='{0}'", today.AddDays(-7).ToString("yyyy-MM-dd"));
                            break;
                        case "lastmonth":
                            condition.AppendFormat(" AND L_TIME>='{0}'", today.AddMonths(-1).ToString("yyyy-MM-dd"));
                            break;
                        case "lastyear":
                            condition.AppendFormat(" AND L_TIME>='{0}'", today.AddYears(-1).ToString("yyyy-MM-dd"));
                            break;
                    }
                }
                #endregion

                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = condition.ToString();
                criteria.PageIndex = param.PageIndex;
                criteria.Fields = "*";
                criteria.PageSize = param.PageSize;
                criteria.TableName = "S_LOG";
                criteria.PrimaryKey = "L_ID";
                if (param.SortName.IsNotNullOrEmpty() && param.SortOrder.IsNotNullOrEmpty())
                {
                    criteria.Sort = $"{param.SortName.FilterSql()} {param.SortOrder.FilterSql()}";
                }
                list= await pageListRepository.GetPageDataAsync<LogEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListAsync(LogSearchParam param)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListAsync(LogSearchParam param)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListAsync(LogSearchParam param)发生Exception，异常信息：{0}", ex);
            } 
            return list;
        }

        #endregion
    }
}
