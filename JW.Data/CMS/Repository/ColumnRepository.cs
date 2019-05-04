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
    /// 栏目
    /// </summary>
    public partial class ColumnRepository : BaseRepository<ColumnEntity, ColumnRepository>, IColumnRepository<ColumnEntity>
    {
        #region Fields



        #endregion

        #region Ctor

        public ColumnRepository(ILogger<ColumnRepository> logger,
            IConnectionProvider connectionProvider)
            : base(logger, connectionProvider)
        {

        }

        #endregion

        #region  Methods

        /// <summary>
        /// 新增一条数据
        /// </summary>
        public int Add(ColumnEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("modelid", model.M_ID);
                    parameters.Add("typeid", model.C_TYPEID);
                    parameters.Add("parentid", model.C_PARENTID);
                    parameters.Add("name", model.C_NAME);
                    parameters.Add("ename", model.C_ENAME);
                    parameters.Add("tips", model.C_Tips);
                    parameters.Add("image", model.C_IMAGE);
                    parameters.Add("summary", model.C_SUMMARY);
                    parameters.Add("ismenu", model.C_ISMENU);
                    parameters.Add("isfootermenu", model.C_ISFOOTERMENU);
                    parameters.Add("link", model.C_LINK);
                    parameters.Add("opentype", model.C_OPENTYPE);
                    parameters.Add("itemopentype", model.C_ITEMOPENTYPE);
                    parameters.Add("matetitle", model.C_MATETITLE);
                    parameters.Add("matekeywords", model.C_MATEKEYWORDS);
                    parameters.Add("matedesc", model.C_MATEDESCRIPTION);
                    parameters.Add("disabled", model.C_DISABLED);
                    parameters.Add("workflowid", model.C_WORKFLOWID);
                    parameters.Add("content", model.C_CONTENT);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_add_C_COLUMN", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Add(ColumnEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Add(ColumnEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Add(ColumnEntity model)发生Exception，异常信息：{0}", ex);
            }
            return -1;
        }

        /// <summary>
        /// 修改一条数据
        /// </summary>
        public int Modify(ColumnEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", model.C_ID);
                    parameters.Add("modelid", model.M_ID);
                    parameters.Add("typeid", model.C_TYPEID);
                    parameters.Add("parentid", model.C_PARENTID);
                    parameters.Add("name", model.C_NAME);
                    parameters.Add("ename", model.C_ENAME);
                    parameters.Add("tips", model.C_Tips);
                    parameters.Add("image", model.C_IMAGE);
                    parameters.Add("summary", model.C_SUMMARY);
                    parameters.Add("ismenu", model.C_ISMENU);
                    parameters.Add("isfootermenu", model.C_ISFOOTERMENU);
                    parameters.Add("link", model.C_LINK);
                    parameters.Add("opentype", model.C_OPENTYPE);
                    parameters.Add("itemopentype", model.C_ITEMOPENTYPE);
                    parameters.Add("matetitle", model.C_MATETITLE);
                    parameters.Add("matekeywords", model.C_MATEKEYWORDS);
                    parameters.Add("matedesc", model.C_MATEDESCRIPTION);
                    parameters.Add("disabled", model.C_DISABLED);
                    parameters.Add("workflowid", model.C_WORKFLOWID);
                    parameters.Add("content", model.C_CONTENT);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_modify_C_COLUMN", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Modify(ColumnEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Modify(ColumnEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Modify(ColumnEntity model)发生Exception，异常信息：{0}", ex);
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
                    conn.Execute("sp_change_C_COLUMN_sort", parameters, commandType: CommandType.StoredProcedure);
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
                    string sql = "UPDATE [C_COLUMN] SET C_DISABLED=@disabled WHERE C_ID=@id";
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
        /// 除ID以外的可用栏目实体数据集合
        /// </summary>
        /// <param name="id">字典编号</param>
        /// <returns></returns>
        public async Task<IEnumerable<SelectColumnEntity>> GetSelectCanUseListAsync(int id)
        {
            IEnumerable<SelectColumnEntity> list = new List<SelectColumnEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = $"SELECT C_ID,C_PARENTID,C_NAME FROM [C_COLUMN] WHERE C_DISABLED=0 AND C_ID<>{id} ORDER BY C_ORDERID ASC";
                    list = await conn.QueryAsync<SelectColumnEntity>(sql);
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

        public async Task<IEnumerable<Column2ModelEntity>> GetAllColumn2ModelListAsync()
        { 
            IEnumerable<Column2ModelEntity> list = new List<Column2ModelEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT * FROM vw_C_COLUMN2MODEL ORDER BY C_ORDERID ASC";
                    list = await conn.QueryAsync<Column2ModelEntity>(sql);
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetAllColumn2ModelListAsync()发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetAllColumn2ModelListAsync()发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetAllColumn2ModelListAsync()发生Exception，异常信息：{0}", ex);
            }
            
            return list;
        }

        public async Task<IEnumerable<Column2Model2DictionaryEntity>> GetAllColumn2Model2DictionaryListAsync()
        { 
            IEnumerable<Column2Model2DictionaryEntity> list = new List<Column2Model2DictionaryEntity>();
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    string sql = "SELECT * FROM vw_C_COLUMN2MODEL2DICTIONARY ORDER BY C_ORDERPATH ASC";
                    list = await conn.QueryAsync<Column2Model2DictionaryEntity>(sql);
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetAllColumn2Model2DictionaryListAsync()发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetAllColumn2Model2DictionaryListAsync()发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetAllColumn2Model2DictionaryListAsync()发生Exception，异常信息：{0}", ex);
            }
            
            return list;
        }

        #endregion
    }
}
