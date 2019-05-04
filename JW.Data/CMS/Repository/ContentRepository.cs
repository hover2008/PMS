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
    /// 内容（文章、新闻）仓储
    /// </summary>
    public partial class ContentRepository : BaseRepository<ContentEntity, ContentRepository>, IContentRepository<ContentEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion

        #region Ctor

        public ContentRepository(ILogger<ContentRepository> logger,
            IConnectionProvider connectionProvider,
            IBasePageListRepository pageListRepository)
            : base(logger, connectionProvider)
        {
            this.pageListRepository = pageListRepository;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// 保存
        /// </summary>
        /// <param name="model">ContentEntity实体对象</param>
        /// <returns>int类型</returns>
        public int Save(ContentEntity model)
        {
            try
            {
                using(var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", model.C_ID);
                    parameters.Add("catid", model.CAT_ID);
                    parameters.Add("title", model.C_TITLE);
                    parameters.Add("subtitle", model.C_SUBTITLE);
                    parameters.Add("imageurl", model.C_IMAGEURL);
                    parameters.Add("content", model.C_CONTENT);
                    parameters.Add("summary", model.C_SUMMARY);
                    parameters.Add("author", model.C_AUTHOR);
                    parameters.Add("source", model.C_SOURCE);
                    parameters.Add("keywords", model.C_KEYWORDS);
                    parameters.Add("weight", model.C_WEIGHT);
                    parameters.Add("addusername", model.C_ADDUSERNAME);
                    parameters.Add("lasteditusername", model.C_LASTEDITUSERNAME);
                    parameters.Add("istop", model.C_ISTOP);
                    parameters.Add("isrec", model.C_ISREC);
                    parameters.Add("isper", model.C_ISPER);
                    parameters.Add("isslide", model.C_ISSLIDE);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_save_C_CONTENT", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Save(ContentEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Save(ContentEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Save(ContentEntity model)发生Exception，异常信息：{0}", ex);
            }
            return -1;
        }  

        /// <summary>
        /// 获取最大排序编号
        /// </summary>
        /// <returns></returns>
        public int GetMaxOrderId(int catid)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT MAX(C_WEIGHT) FROM [C_CONTENT] WHERE CAT_ID=@catid";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("catid", catid);
                    return conn.ExecuteScalar<int>(sql, parameters) + 1;
                } 
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetMaxOrderId(int catid)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetMaxOrderId(int catid)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetMaxOrderId(int catid)发生Exception，异常信息：{0}", ex);
            }
            return 0;
        }

        /// <summary>
        /// 更新权重值
        /// </summary>
        /// <returns></returns>
        public void UpdateWeight(DataTable dt)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "UPDATE a SET a.C_WEIGHT=b.orderid FROM [C_CONTENT] AS a JOIN @dt AS b ON b.id=a.C_ID";
                    conn.Execute(sql, new { dt = dt.AsTableValuedParameter("dbo.OrderTableType") });
                } 
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法UpdateWeight(DataTable dt)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法UpdateWeight(DataTable dt)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法UpdateWeight(DataTable dt)发生Exception，异常信息：{0}", ex);
            }
        }

        #endregion

        #region Method Async

        /// <summary>
        /// 更新点击数
        /// </summary>
        /// <returns></returns>
        public async Task<bool> UpdateHitsByIdAsync(int id)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "UPDATE [C_CONTENT] SET C_HITS=C_HITS+1 WHERE C_ID=@id";
                    return await conn.ExecuteAsync(sql, new { id }) > 0;
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法UpdateHitsByIdAsync(int id)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法UpdateHitsByIdAsync(int id)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法UpdateHitsByIdAsync(int id)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

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
                    string sql = "DELETE FROM [C_CONTENT] WHERE C_ID in @ids";
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
        /// 根据编号对任意字段进行设置
        /// </summary>
        /// <param name="ids">编号</param>
        /// <param name="flag">标记（0，1）</param>
        /// <param name="columnName">字段名称</param>
        /// <returns></returns>
        public async Task<bool> SetByIdsAsync(IList<int> ids, int flag, string columnName)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "UPDATE [C_CONTENT] SET " + columnName + "=@flag WHERE C_ID in @ids";
                    return await conn.ExecuteAsync(sql, new { flag, ids }) > 0;
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法SetByIdsAsync(IList<int> ids, int flag, string columnName)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法SetByIdsAsync(IList<int> ids, int flag, string columnName)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法SetByIdsAsync(IList<int> ids, int flag, string columnName)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        public async Task<BasePagedListModel<Content2StatusNameEntity>> GetListAsync(ContentSearchParam param)
        {
            BasePagedListModel<Content2StatusNameEntity> list = new BasePagedListModel<Content2StatusNameEntity>();

            try
            {
                #region 条件与排序

                StringBuilder condition = new StringBuilder("1=1");
                condition.AppendFormat(" AND CAT_ID={0}", param.CId);
                if (param.Steps > -1)
                {
                    condition.AppendFormat(" AND C_STATUS={0}", param.Steps);
                }
                if (param.Title.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND C_TITLE like '%{0}%'", param.Title.FilterSql());
                }
                if (param.SubTitle.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND C_SUBTITLE like '%{0}%'", param.SubTitle.FilterSql());
                }
                if (param.Summary.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND C_SUMMARY like '%{0}%'", param.Summary.FilterSql());
                }
                #endregion
                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = condition.ToString();
                criteria.PageIndex = param.PageIndex;
                criteria.Fields = "*";
                criteria.PageSize = param.PageSize;
                criteria.TableName = "vw_C_CONTENT";
                criteria.PrimaryKey = "C_ID";
                if (param.SortName.IsNotNullOrEmpty() && param.SortOrder.IsNotNullOrEmpty())
                {
                    criteria.Sort = $"{param.SortName.FilterSql()} {param.SortOrder.FilterSql()}";
                }
                list = await pageListRepository.GetPageDataAsync<Content2StatusNameEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListAsync(ContentSearchParam param)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListAsync(ContentSearchParam param)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListAsync(ContentSearchParam param)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        #endregion
    }
}
