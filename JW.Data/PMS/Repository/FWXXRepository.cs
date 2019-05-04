using Dapper;
using JW.Core.Data.Base;
using JW.Core.Data.Dapper;
using JW.Core.Extensions;
using JW.Data.PMS.IRepository;
using JW.Data.Repository;
using JW.Domain.PMS.Entity;
using JW.Domain.PMS.RequestParam;
using Microsoft.Extensions.Logging;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace JW.Data.CMS.Repository
{
    public partial class FWXXRepository : BaseRepository<FWXXEntity, FWXXRepository>, IFWXXRepository<FWXXEntity>
    {
        #region Fields

        private readonly IBasePageListRepository pageListRepository;

        #endregion

        #region Ctor

        public FWXXRepository(ILogger<FWXXRepository> logger,
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
        public int Save(FWXXEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    DynamicParameters parameters = new DynamicParameters();
                    parameters.Add("id", model.Id);
                    parameters.Add("bh", model.FWBH); 
                    parameters.Add("wz", model.FWWZ);
                    parameters.Add("mj", model.FWMJ);
                    parameters.Add("disabled", model.IsDisabled);
                    parameters.Add("result", 0, DbType.Int32, ParameterDirection.Output);
                    conn.Execute("sp_save_P_FWXX", parameters, commandType: CommandType.StoredProcedure);
                    return parameters.Get<int>("result");
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法Save(FWXXEntity model)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法Save(FWXXEntity model)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法Save(FWXXEntity model)发生Exception，异常信息：{0}", ex);
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
                    string sql = "UPDATE [P_FWXX] SET IsDisabled=@disabled WHERE Id=@id";
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
        /// 获取列表
        /// </summary>
        /// <param name="param">搜索实体</param>
        public async Task<BasePagedListModel<FWXXEntity>> GetListAsync(FWXXSearchParam param)
        {
            BasePagedListModel<FWXXEntity> list = new BasePagedListModel<FWXXEntity>();
            try
            {
                #region 条件与排序

                StringBuilder condition = new StringBuilder("1=1");
                if (param.BH.IsNotNullOrEmpty())
                {
                    condition.AppendFormat(" AND FWBH like '%{0}%'", param.BH.FilterSql());
                }
                #endregion

                PageCriteriaModel criteria = new PageCriteriaModel();
                criteria.Condition = condition.ToString();
                criteria.PageIndex = param.PageIndex;
                criteria.Fields = "*";
                criteria.PageSize = param.PageSize;
                criteria.TableName = "P_FWXX";
                criteria.PrimaryKey = "Id";
                if (param.SortName.IsNotNullOrEmpty() && param.SortOrder.IsNotNullOrEmpty())
                {
                    criteria.Sort = $"{param.SortName.FilterSql()} {param.SortOrder.FilterSql()}";
                }
                list = await pageListRepository.GetPageDataAsync<FWXXEntity>(connectionProvider, criteria);
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetListAsync(FWXXSearchParam param)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetListAsync(FWXXSearchParam param)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetListAsync(FWXXSearchParam param)发生Exception，异常信息：{0}", ex);
            }
            return list;
        }
        
        #endregion
    }
}
