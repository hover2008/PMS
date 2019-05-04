using Dapper;
using JW.Core.Data.Dapper;
using JW.Domain.CMS.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace JW.Data.CMS.IRepository
{
    /// <summary>
    /// 系统配置表
    /// </summary>
    public partial class SettingRepository : ISettingRepository
    {
        #region Fields

        private readonly ILogger<SettingRepository> logger;
        private readonly IConnectionProvider connectionProvider;

        #endregion

        #region Ctor

        public SettingRepository(ILogger<SettingRepository> logger,
            IConnectionProvider connectionProvider)
        {
            this.logger = logger;
            this.connectionProvider = connectionProvider;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// 根据表添加数据
        /// </summary>
        /// <param name="dt">DataTable</param>
        /// <param name="groupId">组编号</param>
        public int Save(DataTable dt, int groupId)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("dt", dt, DbType.Object);
                    parameters.Add("groupid", groupId);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_save_C_SETTING", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法BatchSave(DataTable dt, int groupId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法BatchSave(DataTable dt, int groupId)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法BatchSave(DataTable dt, int groupId)发生Exception，异常信息：{0}", ex);
            }
            return -1;
        }
        /// <summary>
        /// 根据组编号获取配置信息
        /// </summary>
        /// <param name="groupId">类型名称,若需要获取所有的，则groupId=0</param>
        /// <returns></returns>
        public IEnumerable<SettingEntity> GetConfigByGroupId(int groupId)
        {
            IEnumerable<SettingEntity> list = new List<SettingEntity>();
            try
            {
                string sql = "SELECT * FROM [C_SETTING]";
                if (groupId > 0)
                {
                    sql = "SELECT * FROM [C_SETTING] WHERE S_GROUPID=@groupid";
                }
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    if (groupId > 0)
                    {
                        parameters.Add("groupid", groupId);
                    }
                    list = conn.Query<SettingEntity>(sql, parameters);
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetConfigByGroupId(int groupId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetConfigByGroupId(int groupId)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetConfigByGroupId(int groupId)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        /// <summary>
        /// 根据键名和组名获取键值
        /// </summary>
        /// <param name="keyName">键名</param>
        /// <param name="groupId">组名编号</param>
        /// <returns></returns>
        public string GetValuesByKeyAndGroupId(string keyName, int groupId)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT S_VALUE FROM [C_SETTING] WHERE S_GROUPID=@groupid and S_NAME=@key";
                    return conn.ExecuteScalar<string>(sql, new { groupid = groupId, key = keyName }); 
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetValuesByKeyAndGroupId(string keyName, int groupId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetValuesByKeyAndGroupId(string keyName, int groupId)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetValuesByKeyAndGroupId(string keyName, int groupId)发生Exception，异常信息：{0}", ex);
            }
            return String.Empty;
        }

        #endregion 
    }
}
