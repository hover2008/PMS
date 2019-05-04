using Dapper;
using JW.Core.Data.Dapper;
using JW.Data.Repository;
using JW.Data.Sys.IRepository;
using JW.Domain.Sys.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace JW.Data.Sys.Repository
{
    /// <summary>
    /// 系统角色菜单权限码表
    /// </summary>
    public partial class RoleMenuPurviewCodeRepository : BaseRepository<RoleMenuPurviewCodeEntity, RoleMenuPurviewCodeRepository>, IRoleMenuPurviewCodeRepository<RoleMenuPurviewCodeEntity>
    {
        #region Fields

        

        #endregion

        #region Ctor

        public RoleMenuPurviewCodeRepository(ILogger<RoleMenuPurviewCodeRepository> logger,
            IConnectionProvider connectionProvider)
            : base(logger, connectionProvider)
        {
            
        }

        #endregion

        #region  Methods

        /// <summary>
        /// 根据DataTable批量添加(利用表变量类型方式）--SQLServer2008或以上用
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        public bool BatchSave(DataTable dt, int roleId)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("roleid", roleId);
                    parameters.Add("data", dt, DbType.Object);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_save_S_ROLE_MENU_PURVIEWCODE", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result") > 0;
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法BatchSave(DataTable dt, int roleId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法BatchSave(DataTable dt, int roleId)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法BatchSave(DataTable dt, int roleId)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        #endregion

        #region Method Async

        /// <summary>
        /// 根据角色编号获取权限值列表
        /// </summary>
        /// <param name="roleId">角色编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<RoleMenuPurviewCodeEntity>> GetPurviewCodeListByRoleIdAsync(int roleId)
        { 
            IEnumerable<RoleMenuPurviewCodeEntity> list = new List<RoleMenuPurviewCodeEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT R_ID,MPC_CODE FROM [S_ROLE_MENU_PURVIEWCODE] WHERE R_ID=@id";
                    list = await conn.QueryAsync<RoleMenuPurviewCodeEntity>(sql, new { id = roleId });
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetPurviewCodeListByRoleIdAsync(int roleId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetPurviewCodeListByRoleIdAsync(int roleId)生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetPurviewCodeListByRoleIdAsync(int roleId)发生Exception，异常信息：{0}", ex);
            }
            
            return list;
        }

        #endregion
    }
}
