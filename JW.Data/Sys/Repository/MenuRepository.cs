using Dapper;
using JW.Core;
using JW.Core.Data.Base;
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
    /// 系统模块菜单表
    /// </summary>
    public partial class MenuRepository : BaseRepository<MenuEntity, MenuRepository>, IMenuRepository<MenuEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion

        #region Ctor

        public MenuRepository(ILogger<MenuRepository> logger,
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
        public int Add(MenuEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("pid", model.M_PARENTID);
                    parameters.Add("name", model.M_NAME);
                    parameters.Add("code", model.M_CODE);
                    parameters.Add("icon", model.M_ICON);
                    parameters.Add("link", model.M_LINK);
                    parameters.Add("disabled", model.M_DISABLED);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_add_S_Menu", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Add(MenuEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Add(MenuEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Add(MenuEntity model)发生Exception，异常信息：{0}", ex);
            }
            return -1;
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        public int Update(MenuEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id",model.M_ID);
                    parameters.Add("pid", model.M_PARENTID);
                    parameters.Add("name", model.M_NAME);
                    parameters.Add("code", model.M_CODE);
                    parameters.Add("icon", model.M_ICON);
                    parameters.Add("link", model.M_LINK);
                    parameters.Add("disabled", model.M_DISABLED);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_modify_S_Menu", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Update(MenuEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Update(MenuEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Update(MenuEntity model)发生Exception，异常信息：{0}", ex);
            }
            return -1;
        }

        /// <summary>
        /// 改变一条数据排序
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="moveType">排序类型</param>
        /// <returns></returns>
        public bool ChangeSort(int id, MoveType moveType)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", id);
                    parameters.Add("sortType", (int)moveType);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_change_sort_S_Menu", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result") > 0;
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法ChangeSort(int id, MoveType moveType)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法ChangeSort(int id, MoveType moveType)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法ChangeSort(int id, MoveType moveType)发生Exception，异常信息：{0}", ex);
            }
            return false;
        }  

        #endregion

        #region Method Async

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
                    string sql = "UPDATE [S_MENU] SET M_DISABLED=@disabled WHERE M_ID=@id";
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("@disabled", disabled);
                    parameters.Add("@id", id);
                    return await conn.ExecuteAsync(sql, parameters) > 0;
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
        /// 根据角色组编号获取菜单信息
        /// </summary>
        /// <param name="roles">角色组编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<MenuEntity>> GetListByRolesAsync(string roles)
        { 
            IEnumerable<MenuEntity> list = new List<MenuEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = $"SELECT DISTINCT a.* FROM S_MENU a JOIN S_ROLE_MENU_PURVIEWCODE b ON a.M_CODE=b.MPC_CODE JOIN dbo.fn_SplitStr('{roles}',',') c ON c.column1=b.R_ID WHERE a.M_DISABLED=0 ORDER BY M_ORDERID ASC";
                    list = await conn.QueryAsync<MenuEntity>(sql);
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListByRolesAsync(string roles)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListByRolesAsync(string roles)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListByRolesAsync(string roles)发生Exception，异常信息：{0}", ex);
            }
            
            return list;
        }

        /// <summary>
        /// 获取所有菜单模块列表
        /// </summary> 
        /// <param name="menuId">菜单编号</param>
        /// <returns>PageDataModel<MenuEntity></returns>
        public async Task<BasePagedListModel<MenuEntity>> GetAllListAsync(int menuId)
        {
            BasePagedListModel<MenuEntity> list = new BasePagedListModel<MenuEntity>();
            try
            {
                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = $"M_ID<>{menuId}";
                criteria.PageIndex = 1;
                criteria.Fields = "*";
                criteria.PageSize = 10000;
                criteria.TableName = "S_MENU";
                criteria.PrimaryKey = "M_ID";
                criteria.Sort = "M_ORDERPATH ASC";
                list = await pageListRepository.GetPageDataAsync<MenuEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetAllListAsync(int menuId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetAllListAsync(int menuId)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetAllListAsync(int menuId)发生Exception，异常信息：{0}", ex);
            }

            return list;
        }

        /// <summary>
        /// 异步获取可用菜单模块列表
        /// </summary> 
        /// <returns>PageDataModel<MenuEntity></returns>
        public async Task<BasePagedListModel<MenuEntity>> GetCanUseListAsync()
        {
            BasePagedListModel<MenuEntity> list = new BasePagedListModel<MenuEntity>();
            try
            {
                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = "M_DISABLED=0";
                criteria.PageIndex = 1;
                criteria.Fields = "*";
                criteria.PageSize = 10000;
                criteria.TableName = "S_MENU";
                criteria.PrimaryKey = "M_ID";
                criteria.Sort = "M_ORDERPATH ASC";
                list = await pageListRepository.GetPageDataAsync<MenuEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetAllListAsync(int menuId)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetAllListAsync(int menuId)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetAllListAsync(int menuId)发生Exception，异常信息：{0}", ex);
            }

            return list;
        }

        #endregion
    }
}
