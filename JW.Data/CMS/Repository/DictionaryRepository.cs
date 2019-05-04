using Dapper;
using JW.Core;
using JW.Core.Data.Dapper;
using JW.Data.CMS.IRepository;
using JW.Data.Repository;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.ResposneEntity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace JW.Data.CMS.Repository
{
    /// <summary>
    /// 字典数据
    /// </summary>
    public partial class DictionaryRepository : BaseRepository<DictionaryEntity, DictionaryRepository> , IDictionaryRepository<DictionaryEntity> 
    {
        #region Fields


        #endregion

        #region Ctor

        public DictionaryRepository(ILogger<DictionaryRepository> logger,
            IConnectionProvider connectionProvider)
            : base(logger, connectionProvider)
        {

        }

        #endregion

        #region  Methods

        /// <summary>
        /// 新增一条数据
        /// </summary>
        public int Add(DictionaryEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("pid", model.D_PARENTID);
                    parameters.Add("name", model.D_NAME);
                    parameters.Add("disabled", model.D_DISABLED);
                    parameters.Add("remark", model.D_REMARK);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_add_C_DICTIONARY", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Add(DictionaryEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Add(DictionaryEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Add(DictionaryEntity model)发生Exception，异常信息：{0}", ex);
            }
            return -1;
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        public int Modify(DictionaryEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", model.D_ID);
                    parameters.Add("pid", model.D_PARENTID);
                    parameters.Add("name", model.D_NAME);
                    parameters.Add("disabled", model.D_DISABLED);
                    parameters.Add("remark", model.D_REMARK);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_modify_C_DICTIONARY", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Modify(DictionaryEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Modify(DictionaryEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Modify(DictionaryEntity model)发生Exception，异常信息：{0}", ex);
            }
            return -1;
        }  

        /// <summary>
        /// 改变一条数据排序
        /// </summary>
        /// <param name="id">编号</param>
        /// <param name="moveType">排序类型</param>
        /// <returns></returns>
        public int ChangeSort(int id, MoveType moveType)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", id);
                    parameters.Add("sortType", moveType);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_change_C_DICTIONARY_Sort", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
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
            return -1;
        }

        #endregion

        #region Methods Async

        /// <summary>
        /// 根据编号获取相关字典名称
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<String> GetNameByIdAsync(int id)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT D_NAME FROM [C_DICTIONARY] WHERE D_ID=@id";
                    return await conn.ExecuteScalarAsync<string>(sql, new { id });
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetNameByIdAsync(int id)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetNameByIdAsync(int id)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetNameByIdAsync(int id)发生Exception，异常信息：{0}", ex);
            }
            return String.Empty;
        }

        public async Task<IEnumerable<DictionaryEntity>> GetListByPIdAsync(int pid)
        {
            IEnumerable<DictionaryEntity> list = new List<DictionaryEntity>();

            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT * FROM [C_DICTIONARY] WHERE D_DISABLED=0 AND D_PARENTID=@pid ORDER BY D_ORDERPATH ASC";
                    list = await conn.QueryAsync<DictionaryEntity>(sql, new { pid });
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListByPIdAsync(int pid)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListByPIdAsync(int pid)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListByPIdAsync(int pid)发生Exception，异常信息：{0}", ex);
            }
            
            return list;
        }

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
                    string sql = "UPDATE [C_DICTIONARY] SET D_DISABLED=@disabled WHERE D_ID=@id";
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
        /// 获取所有的字典数据
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<DictionaryEntity>> GetAllListAsync()
        {
            IEnumerable<DictionaryEntity> list = new List<DictionaryEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT * FROM [C_DICTIONARY]";
                    return await conn.QueryAsync<DictionaryEntity>(sql);
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
        /// 根据编号获取字典子集集合数据
        /// </summary>
        /// <param name="id">编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<DictionaryEntity>> GetSubsetListByIdAsync(int id)
        {
            IEnumerable<DictionaryEntity> list = new List<DictionaryEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = $"SELECT * FROM [C_DICTIONARY] WHERE D_DISABLED=0 AND D_PATH like '%,{id},%' ORDER BY D_ORDERID ASC"; 
                    return await conn.QueryAsync<DictionaryEntity>(sql);
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetSubsetListByIdAsync(int id)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetSubsetListByIdAsync(int id)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetSubsetListByIdAsync(int id)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }

        /// <summary>
        /// 除ID以外的可用字典实体数据集合
        /// </summary>
        /// <param name="id">字典编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<SelectDictionaryEntity>> GetSelectCanUseListAsync(int id)
        {
            IEnumerable<SelectDictionaryEntity> list = new List<SelectDictionaryEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = $"SELECT D_ID,D_PARENTID,D_NAME,D_CHILDREN FROM [C_DICTIONARY] WHERE D_DISABLED=0 AND D_ID<>{id} ORDER BY D_ORDERID ASC";
                    list = await conn.QueryAsync<SelectDictionaryEntity>(sql);
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