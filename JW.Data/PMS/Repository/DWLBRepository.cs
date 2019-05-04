using Dapper;
using JW.Core.Data.Dapper;
using JW.Data.PMS.IRepository;
using JW.Data.Repository;
using JW.Domain.PMS.Entity;
using JW.Domain.PMS.ResposneEntity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace JW.Data.CMS.Repository
{
    public partial class DWLBRepository : BaseRepository<DWLBEntity, DWLBRepository>, IDWLBRepository<DWLBEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion

        #region Ctor

        public DWLBRepository(ILogger<DWLBRepository> logger,
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
        public int Save(DWLBEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", model.Id);
                    parameters.Add("code", model.Code); 
                    parameters.Add("name", model.Name); 
                    parameters.Add("pid", model.PId); 
                    parameters.Add("disabled", model.IsDisabled);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_save_P_DWLB", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Save(DWLBEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Save(DWLBEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Save(DWLBEntity model)发生Exception，异常信息：{0}", ex);
            }
            return -1;
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
                    string sql = "UPDATE [P_DWLB] SET IsDisabled=@disabled WHERE Id=@id";
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
        /// 获取所有的数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DWLBEntity>> GetAllListAsync()
        {
            IEnumerable<DWLBEntity> list = new List<DWLBEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT * FROM [P_DWLB]";
                    return await conn.QueryAsync<DWLBEntity>(sql);
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetAllListAsync()发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetAllListAsync()发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetAllListAsync()发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        /// <summary>
        /// 除ID以外的可用字典实体数据集合
        /// </summary>
        /// <param name="id">字典编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<SelectDWLBEntity>> GetSelectCanUseListAsync(int id)
        {
            IEnumerable<SelectDWLBEntity> list = new List<SelectDWLBEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = $"SELECT Id,PId,Name FROM [P_DWLB] WHERE IsDisabled=0 AND Id<>{id} ORDER BY Code ASC";
                    list = await conn.QueryAsync<SelectDWLBEntity>(sql);
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

        /// <summary>
        /// 获取可用于下拉框选择的DWLB数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<SelectDWLBEntity>> GetSelectCanUseListAsync()
        {
            IEnumerable<SelectDWLBEntity> list = new List<SelectDWLBEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT Id,Name FROM P_DWLB WHERE IsDisabled=0 ORDER BY Code ASC";
                    list = await conn.QueryAsync<SelectDWLBEntity>(sql);
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetSelectCanUseListAsync()发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetSelectCanUseListAsync()发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetSelectCanUseListAsync()发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        #endregion
    }
}
