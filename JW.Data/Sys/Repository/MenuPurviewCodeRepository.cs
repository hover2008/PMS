using Dapper;
using JW.Core.Data.Base;
using JW.Core.Data.Dapper;
using JW.Data.Repository;
using JW.Data.Sys.IRepository;
using JW.Domain.Sys.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace JW.Data.Sys.Repository
{
    /// <summary>
    /// 系统菜单权限码表
    /// </summary>
    public partial class MenuPurviewCodeRepository : BaseRepository<MenuPurviewCodeEntity, MenuPurviewCodeRepository>, IMenuPurviewCodeRepository<MenuPurviewCodeEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion

        #region Ctor

        public MenuPurviewCodeRepository(ILogger<MenuPurviewCodeRepository> logger,
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
        public int Save(MenuPurviewCodeEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", model.MPC_ID);
                    parameters.Add("menuid", model.M_ID);
                    parameters.Add("name", model.MPC_NAME);
                    parameters.Add("code", model.MPC_CODE);
                    parameters.Add("disabled", model.MPC_DISABLED);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_save_S_MENU_PURVIEWCODE", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Save(MenuPurviewCodeEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Save(MenuPurviewCodeEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Save(MenuPurviewCodeEntity model)发生Exception，异常信息：{0}", ex);
            }
            return -1;
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
                    string sql = "UPDATE [S_MENU_PURVIEWCODE] SET MPC_DISABLED=@disabled WHERE MPC_ID=@id"; 
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
        /// 获取菜单按钮列表
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="menuId">菜单模块编号</param>
        /// <returns>PageDataModel<MenuPurviewCodeEntity></returns>
        public async Task<BasePagedListModel<MenuPurviewCodeEntity>> GetListByMenuIdAsync(int pageIndex, int pageSize, int menuId = 0)
        {
            BasePagedListModel<MenuPurviewCodeEntity> list = new BasePagedListModel<MenuPurviewCodeEntity>();
            try
            {
                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = $"M_ID={menuId}";
                criteria.PageIndex = pageIndex;
                criteria.Fields = "*";
                criteria.PageSize = pageSize;
                criteria.TableName = "S_MENU_PURVIEWCODE";
                criteria.PrimaryKey = "MPC_ID";
                criteria.Sort = "";
                list = await pageListRepository.GetPageDataAsync<MenuPurviewCodeEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListByMenuIdAsync(int pageIndex, int pageSize, int menuId = 0)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListByMenuIdAsync(int pageIndex, int pageSize, int menuId = 0)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListByMenuIdAsync(int pageIndex, int pageSize, int menuId = 0)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        /// <summary>
        /// 获取菜单按钮列表
        /// </summary>
        /// <param name="pageIndex">页索引</param>
        /// <param name="pageSize">页大小</param>
        /// <param name="menuId">菜单模块编号</param>
        /// <returns>PageDataModel<MenuPurviewCodeEntity></returns>
        public async Task<BasePagedListModel<MenuPurviewCodeEntity>> GetCanUseListAsync(int pageIndex, int pageSize)
        {
            BasePagedListModel<MenuPurviewCodeEntity> list = new BasePagedListModel<MenuPurviewCodeEntity>();
            try
            {
                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = "MPC_DISABLED=0";
                criteria.PageIndex = pageIndex;
                criteria.Fields = "*";
                criteria.PageSize = pageSize;
                criteria.TableName = "S_MENU_PURVIEWCODE";
                criteria.PrimaryKey = "MPC_ID";
                criteria.Sort = "";
                list = await pageListRepository.GetPageDataAsync<MenuPurviewCodeEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetCanUseListAsync(int pageIndex, int pageSize)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetCanUseListAsync(int pageIndex, int pageSize)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetCanUseListAsync(int pageIndex, int pageSize)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        #endregion
    }
}
