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
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace JW.Data.Sys.Repository
{
    /// <summary>
    /// 系统管理角色用户表
    /// </summary>
    public partial class RoleUserRepository : BaseRepository<Role2UserEntity, RoleUserRepository>, IRoleUserRepository<Role2UserEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion

        #region Ctor

        public RoleUserRepository(ILogger<RoleUserRepository> logger,
            IConnectionProvider connectionProvider,
            IBasePageListRepository pageListRepository)
            : base(logger, connectionProvider)
        {
            this.pageListRepository = pageListRepository;
        }

        #endregion

        #region Methods


        #endregion

        #region Methods Async

        /// <summary>
        /// 根据用户编号获取拥有角色名称
        /// </summary> 
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<string>> GetRoleNameListByUserIdAsync(int userId)
        { 
            IEnumerable<string> list = new List<string>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT R_NAME FROM [S_ROLE_USER] a JOIN [S_ROLE] b ON b.R_ID=a.R_ID WHERE a.U_ID=@userid ORDER BY a.R_ID ASC";
                    list = await conn.QueryAsync<string>(sql, new { userid = userId });
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetRoleNameListByUserIdAsync(int userId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetRoleNameListByUserIdAsync(int userId)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetRoleNameListByUserIdAsync(int userId)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        /// <summary>
        /// 根据DataTable批量添加角色用户(利用表变量类型结合方式）--SQLServer2008或以上用
        /// </summary>
        /// <param name="dt">DataTable</param>
        public async Task<bool> AddByDataTableAsync(DataTable dt)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "INSERT INTO S_ROLE_USER(R_ID,U_ID) SELECT d.roleid,d.userid FROM @dt AS d";
                    return await conn.ExecuteAsync(sql, new { dt = dt.AsTableValuedParameter("RoleUserTableType") }) > 0;
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法AddByDataTableAsync(DataTable dt)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法AddByDataTableAsync(DataTable dt)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法AddByDataTableAsync(DataTable dt)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        /// <summary>
        /// 根据角色编号和用户编号组批量删除（以英文","隔开)
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <param name="userIds">用户编号组</param>
        public async Task<bool> DeleteByIdsAsync(int roleId, IList<int> userIds)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "DELETE FROM [S_ROLE_USER] WHERE R_ID=@roleId AND U_ID IN @userIds";
                    return await conn.ExecuteAsync(sql, new { roleId, userIds }) > 0;
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法DeleteByIdsAsync(int roleId, IList<int> userIds)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法DeleteByIdsAsync(int roleId, IList<int> userIds)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法DeleteByIdsAsync(int roleId, IList<int> userIds)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        /// <summary>
        /// 根据角色编号获取用户列表
        /// </summary> 
        /// <param name="param">角色用户搜索实体</param>
        /// <returns>PageDataModel<Role2UserEntity></returns>
        public async Task<BasePagedListModel<Role2UserEntity>> GetListByRoleIdAsync(RoleUserSearchParam param)
        {
            BasePagedListModel<Role2UserEntity> list = new BasePagedListModel<Role2UserEntity>();
            try
            {
                #region 条件与排序

                StringBuilder condition = new StringBuilder();
                condition.AppendFormat("R_ID={0}", param.RoleId);
                if (param.Name.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND U_NAME='{0}'", param.Name.FilterSql());
                }
                if (param.RealName.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND U_REALNAME='{0}'", param.RealName.FilterSql());
                }

                #endregion

                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = condition.ToString();
                criteria.PageIndex = param.PageIndex;
                criteria.Fields = "*";
                criteria.PageSize = param.PageSize;
                criteria.TableName = "vw_S_ROLEUSER";
                criteria.PrimaryKey = "U_ID";
                if (param.SortName.IsNotNullOrEmpty() && param.SortOrder.IsNotNullOrEmpty())
                {
                    criteria.Sort = $"{param.SortName.FilterSql()} {param.SortOrder.FilterSql()}";
                }
                list = await pageListRepository.GetPageDataAsync<Role2UserEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListByRoleIdAsync(RoleUserSearchParam param)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListByRoleIdAsync(RoleUserSearchParam param)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListByRoleIdAsync(RoleUserSearchParam param)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        #endregion
    }
}
