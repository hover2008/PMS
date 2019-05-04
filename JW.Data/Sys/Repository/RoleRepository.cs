using Dapper;
using JW.Core.Data.Base;
using JW.Core.Data.Dapper;
using JW.Core.Extensions;
using JW.Data.Repository;
using JW.Data.Sys.IRepository;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using JW.Domain.Sys.ResposneEntity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace JW.Data.Sys.Repository
{
    /// <summary>
    /// 系统角色表
    /// </summary>
    public partial class RoleRepository : BaseRepository<RoleEntity, RoleRepository>, IRoleRepository<RoleEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion

        #region Ctor

        public RoleRepository(ILogger<RoleRepository> logger,
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
        public int Save(RoleEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", model.R_ID);
                    parameters.Add("name", model.R_NAME);
                    parameters.Add("desc", model.R_DESC);
                    parameters.Add("orderid", model.R_ORDERID);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_save_S_ROLE", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Save(RoleEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Save(RoleEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Save(RoleEntity model)发生Exception，异常信息：{0}", ex);
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
                    string sql = "SELECT ISNULL(MAX(R_ORDERID),0) FROM [S_ROLE]";
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
                    string sql = "UPDATE a SET a.R_ORDERID=b.orderid FROM [S_ROLE] AS a JOIN @dt AS b ON b.id=a.R_ID";
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

        #region Methods Async

        /// <summary>
        /// 根据角色编号获取权限值信息
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        public async Task<String> GetPermissionByRoleIdAsync(int roleId)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT CONVERT(varchar(30),MPC_CODE)+',' FROM [S_ROLE_MENU_PURVIEWCODE] WHERE R_ID=@id FOR XML PATH('')";
                    return await conn.ExecuteScalarAsync<string>(sql, new { id = roleId });
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetPermissionByRoleIdAsync(int roleId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetPermissionByRoleIdAsync(int roleId)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetPermissionByRoleIdAsync(int roleId)发生Exception，异常信息：{0}", ex);
            }
            return String.Empty;
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
                    string sql = "DELETE FROM [S_ROLE] WHERE R_ID in @ids";
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
        /// 获取角色列表
        /// </summary>
        /// <param name="param">角色搜索实体</param>
        /// <returns>PageDataModel<RoleEntity></returns>
        public async Task<BasePagedListModel<RoleEntity>> GetListAsync(RoleSearchParam param)
        {
            BasePagedListModel<RoleEntity> list = new BasePagedListModel<RoleEntity>();
            try
            {
                #region 条件与排序

                StringBuilder condition = new StringBuilder("1=1");
                if (param.Name.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND R_NAME='{0}'", param.Name.FilterSql());
                }

                #endregion

                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = condition.ToString();
                criteria.PageIndex = param.PageIndex;
                criteria.Fields = "*";
                criteria.PageSize = param.PageSize;
                criteria.TableName = "S_ROLE";
                criteria.PrimaryKey = "R_ID";
                if (param.SortName.IsNotNullOrEmpty() && param.SortOrder.IsNotNullOrEmpty())
                {
                    criteria.Sort = $"{param.SortName.FilterSql()} {param.SortOrder.FilterSql()}";
                }
                list = await pageListRepository.GetPageDataAsync<RoleEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListAsync(RoleSearchParam param)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListAsync(RoleSearchParam param)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListAsync(RoleSearchParam param)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取可用于选择的角色实体数据集合
        /// </summary> 
        /// <returns></returns>
        public async Task<IEnumerable<SelectRoleEntity>> GetSelectListAsync()
        { 
            IEnumerable<SelectRoleEntity> list = new List<SelectRoleEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT R_ID,R_NAME FROM [S_ROLE] ORDER BY R_ORDERID ASC";
                    list = await conn.QueryAsync<SelectRoleEntity>(sql);
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetSelectCanUseListAsync(int id)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetSelectCanUseListAsync(int id)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetSelectCanUseListAsync(int id)发生Exception，异常信息：{0}", ex);
            }
            
            return list;
        }

        #endregion
    }
}
