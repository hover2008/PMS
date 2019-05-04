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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JW.Data.Sys.Repository
{
    /// <summary>
    /// 系统管理用户表
    /// </summary>
    public partial class UserRepository : BaseRepository<UserEntity, UserRepository>, IUserRepository<UserEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion

        #region Ctor

        public UserRepository(ILogger<UserRepository> logger,
            IConnectionProvider connectionProvider,
            IBasePageListRepository pageListRepository)
            : base(logger, connectionProvider)
        {
            this.pageListRepository = pageListRepository;
        }

        #endregion

        #region  Methods

        /// <summary>
        /// 新增一条数据
        /// </summary>
        /// <param name="model">JW.Domain.Entity.User</param>
        /// <param name="userRoleDT">用户角色信息DataTable</param>
        public int Add(UserEntity model, DataTable userRoleDT)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("name", model.U_NAME);
                    parameters.Add("pwd", model.U_PWD);
                    parameters.Add("encrypt", model.U_ENCRYPT);
                    parameters.Add("realName", model.U_REALNAME);
                    parameters.Add("email", model.U_EMAIL);
                    parameters.Add("mobile", model.U_MOBILE);
                    parameters.Add("tel", model.U_TEL);
                    parameters.Add("disabled", model.U_DISABLED);
                    parameters.Add("photo", model.U_PHOTO);
                    parameters.Add("userRole", userRoleDT, DbType.Object);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_add_S_USER", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Add(UserEntity model, DataTable userRoleDT)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Add(UserEntity model, DataTable userRoleDT)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Add(UserEntity model, DataTable userRoleDT)生Exception，异常信息：{0}", ex);
            }
            return -1;
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        /// <param name="model">JW.Domain.Entity.User</param>
        /// <param name="userRoleDT">用户角色信息DataTable</param>
        public int Modify(UserEntity model, DataTable userRoleDT)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", model.U_ID);
                    parameters.Add("pwd", model.U_PWD);
                    parameters.Add("encrypt", model.U_ENCRYPT);
                    parameters.Add("realName", model.U_REALNAME);
                    parameters.Add("email", model.U_EMAIL);
                    parameters.Add("mobile", model.U_MOBILE);
                    parameters.Add("tel", model.U_TEL);
                    parameters.Add("disabled", model.U_DISABLED);
                    parameters.Add("photo", model.U_PHOTO);
                    parameters.Add("userRole", userRoleDT, DbType.Object);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_modify_S_USER", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }; 
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Modify(UserEntity model, DataTable userRoleDT)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Modify(UserEntity model, DataTable userRoleDT)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Modify(UserEntity model, DataTable userRoleDT)发生Exception，异常信息：{0}", ex);
            }
            return -1;
        } 

        /// <summary>
        /// 根据用户名获取用户信息
        /// </summary>
        /// <param name="userName">用户名(也可以为姓名）</param>
        /// <returns></returns>
        public UserEntity GetModelByUserName(string userName)
        {
            UserEntity model = null;
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT * FROM [S_USER] WHERE U_NAME=@userName";
                    return conn.Query<UserEntity>(sql, new { userName }).FirstOrDefault();
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetModelByUserName(string username)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetModelByUserName(string username)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetModelByUserName(string username)发生Exception，异常信息：{0}", ex);
            }
            return model;
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
                    string sql = "UPDATE [S_USER] SET U_DISABLED=@disabled WHERE U_ID=@id";
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
        /// 修改档案
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns></returns>
        public async Task<bool> ModifyInfoAsync(UserEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "UPDATE [S_USER] SET U_REALNAME=@realName,U_EMAIL=@email,U_MOBILE=@mobile,U_TEL=@tel,U_UPDATETIME=GETDATE(),U_Photo=@photo WHERE U_ID=@id";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", model.U_ID);
                    parameters.Add("@realName", model.U_REALNAME);
                    parameters.Add("@email", model.U_EMAIL);
                    parameters.Add("@mobile", model.U_MOBILE);
                    parameters.Add("@tel", model.U_TEL);
                    parameters.Add("@photo", model.U_PHOTO);
                    return await conn.ExecuteAsync(sql, parameters) > 0;
                };
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法ModifyInfoAsync(UserEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法ModifyInfoAsync(UserEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法ModifyInfoAsync(UserEntity model)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="id">用户编号</param> 
        /// <param name="pwd">新密码</param>
        /// <param name="encrypt">安全密钥</param>
        /// <returns></returns>
        public async Task<bool> ModifyPwdAsync(int id, string pwd, string encrypt)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "UPDATE [S_USER] SET U_PWD=@pwd,U_ENCRYPT=@encrypt,U_UPDATETIME=GETDATE(),U_LASTMODIFYPASSWORDTIME=GETDATE() WHERE U_ID=@id";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@id", id);
                    parameters.Add("@pwd", pwd);
                    parameters.Add("@encrypt", encrypt);
                    return await conn.ExecuteAsync(sql, parameters) > 0;
                }; 
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法ModifyPwdAsync(int id, string pwd, string encrypt)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法ModifyPwdAsync(int id, string pwd, string encrypt)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法ModifyPwdAsync(int id, string pwd, string encrypt)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        /// <summary>
        /// 根据用户编号获取权限值列表
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<UserPermissionCodeEntity>> GetPermissionByUserIdAsync(int userId)
        { 
            IEnumerable<UserPermissionCodeEntity> list = new List<UserPermissionCodeEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT DISTINCT MPC_CODE AS Code FROM [S_ROLE_MENU_PURVIEWCODE] rmp JOIN [S_ROLE_USER] ru ON ru.R_ID = rmp.R_ID AND ru.U_ID=@id";
                    list = await conn.QueryAsync<UserPermissionCodeEntity>(sql, new { id = userId });
                }; 
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetPermissionByUserIdAsync(int userId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetPermissionByUserIdAsync(int userId)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetPermissionByUserIdAsync(int userId)发生Exception，异常信息：{0}", ex);
            }
            
            return list;
        }

        /// <summary>
        /// 根据用户编号获取角色信息
        /// </summary>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public async Task<String> GetRoleByUserIdAsync(int userId)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT CONVERT(varchar,R_ID)+',' FROM [S_ROLE_USER] WHERE U_ID=@id FOR XML PATH('')";
                    return await conn.ExecuteScalarAsync<string>(sql, new { id = userId });
                };
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetRoleByUserIdAsync(int userId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetRoleByUserIdAsync(int userId)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetRoleByUserIdAsync(int userId)发生Exception，异常信息：{0}", ex);
            }
            return String.Empty;
        }

        /// <summary>
        /// 设置禁用
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="lockScreen">true-已锁屏，false-未锁屏</param>
        /// <returns></returns>
        public async Task<bool> SetLockScreenAsync(int id, bool lockScreen)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "UPDATE [S_USER] SET U_LOCKSCREEN=@lockScreen WHERE U_ID=@id";
                    return await conn.ExecuteAsync(sql, new { lockScreen, id }) > 0;
                };
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法SetLockScreenAsync(int id, bool lockScreen)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法SetLockScreenAsync(int id, bool lockScreen)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法SetLockScreenAsync(int id, bool lockScreen)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        /// <summary>
        /// 修改头像
        /// </summary>
        /// <param name="id">用户编号</param> 
        /// <param name="photo">头像路径</param> 
        /// <returns></returns>
        public async Task<bool> ModifyPhotoAsync(int id, string photo)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "UPDATE [S_USER] SET U_PHOTO=@photo,U_UPDATETIME=GETDATE() WHERE U_ID=@id";
                    return await conn.ExecuteAsync(sql, new { photo, id }) > 0;
                }; 
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法ModifyPhoto(int id, string photo)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法ModifyPhoto(int id, string photo)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法ModifyPhoto(int id, string photo)发生Exception，异常信息：{0}", ex);
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
                    string sql = "DELETE FROM [S_USER] WHERE U_ID in @ids";
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
        /// 根据登录情况更新登录用户信息
        /// </summary>
        public async Task<bool> UpdateByLoginAsync(int id, string ip, int errorTimes)
        { 
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "UPDATE [S_USER] SET U_PREVLOGINTIME=U_LASTLOGINTIME,U_PREVLOGINIP=U_LASTLOGINIP,U_LASTLOGINTIME=GETDATE(),U_LASTLOGINIP=@lastLoginIP,U_LOGINTIMES=U_LOGINTIMES+1,U_ERRORTIMES=@errorTimes,U_UPDATETIME=GETDATE(),U_LOCKSCREEN=0 WHERE U_ID=@id";
                    return await conn.ExecuteAsync(sql, new { lastLoginIP = ip, errorTimes, id }) > 0;
                };
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法UpdateByLoginAsync(int id, string ip, int errorTimes)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法UpdateByLoginAsync(int id, string ip, int errorTimes)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法UpdateByLoginAsync(int id, string ip, int errorTimes)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }

        /// <summary>
        /// 获取用户列表
        /// </summary>
        /// <param name="param">用户搜索实体</param>
        /// <returns>PageDataModel<UserEntity></returns>
        public async Task<BasePagedListModel<UserEntity>> GetListAsync(UserSearchParam param)
        {
            BasePagedListModel<UserEntity> list = new BasePagedListModel<UserEntity>();
            try
            {
                #region 条件与排序

                StringBuilder condition = new StringBuilder("1=1");
                if (param.Name.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND U_NAME='{0}'", param.Name.FilterSql());
                }
                if (param.RealName.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND U_REALNAME='{0}'", param.RealName.FilterSql());
                }
                if (param.Mobile.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND U_MOBILE='{0}'", param.Mobile.FilterSql());
                }

                #endregion

                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = condition.ToString();
                criteria.PageIndex = param.PageIndex;
                criteria.Fields = "*";
                criteria.PageSize = param.PageSize;
                criteria.TableName = "S_USER";
                criteria.PrimaryKey = "U_ID";
                if (param.SortName.IsNotNullOrEmpty() && param.SortOrder.IsNotNullOrEmpty())
                {
                    criteria.Sort = $"{param.SortName.FilterSql()} {param.SortOrder.FilterSql()}";
                }
                list = await pageListRepository.GetPageDataAsync<UserEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListAsync(UserSearchParam param)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListAsync(UserSearchParam param)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListAsync(UserSearchParam param)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        /// <summary>
        /// 根据角色编号获取可用用户列表
        /// </summary> 
        /// <param name="param">角色用户搜索实体</param>
        /// <returns>PageDataModel<LogEntity></returns>
        public async Task<BasePagedListModel<UserEntity>> GetCanUserListByRoleIdAsync(RoleUserSearchParam param)
        {
            BasePagedListModel<UserEntity> list = new BasePagedListModel<UserEntity>();
            try
            {
                #region 条件与排序
                StringBuilder condition = new StringBuilder();
                condition.Append("U_DISABLED=0");
                condition.AppendFormat(" AND (SELECT COUNT(*) AS num FROM [S_ROLE_USER] b WHERE b.U_ID=S_USER.U_ID AND R_ID={0})=0", param.RoleId);
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
                criteria.TableName = "S_USER";
                criteria.PrimaryKey = "U_ID";
                if (param.SortName.IsNotNullOrEmpty() && param.SortOrder.IsNotNullOrEmpty())
                {
                    criteria.Sort = $"{param.SortName.FilterSql()} {param.SortOrder.FilterSql()}";
                }
                list = await pageListRepository.GetPageDataAsync<UserEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListAsync(UserSearchParam param)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListAsync(UserSearchParam param)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListAsync(UserSearchParam param)发生Exception，异常信息：{0}", ex);
            }

            return list;
        }

        #endregion
    }
}
