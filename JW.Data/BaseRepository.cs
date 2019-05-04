using Dapper.Contrib.Extensions;
using JW.Core.Data.Dapper;
using JW.Data.IRepository;
using JW.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace JW.Data.Repository
{
    public class BaseRepository<TEntity, Repository> :IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        #region Fields

        protected readonly ILogger<Repository> logger;
        protected readonly IConnectionProvider connectionProvider;

        #endregion Fields

        #region Ctor

        public BaseRepository(ILogger<Repository> logger,
            IConnectionProvider connectionProvider) 
        {
            this.logger = logger;
            this.connectionProvider = connectionProvider;
        }

        #endregion Ctor

        #region Methods
         

        #endregion Methods

        #region Methods Async

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public async Task<TEntity> GetModelByIdAsync(int id)
        {
            TEntity model = null;
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    model = await conn.GetAsync<TEntity>(id);
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError("调用方法GetModelByIdAsync(int id)发生ArgumentNullException，异常信息：{0}", ex);
            }
            catch (SqlException ex)
            {
                logger.LogError("调用方法GetModelByIdAsync(int id)发生SqlException，异常信息：{0}", ex);
            }
            catch (Exception ex)
            {
                logger.LogError("调用方法GetModelByIdAsync(int id)发生Exception，异常信息：{0}", ex);
            }
            return model;
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="model">Model</param>
        /// <returns></returns>
        public async Task<bool> AddAsync(TEntity model)
        {
            try
            {
                using (var conn = connectionProvider.CreateConn())
                {
                    return await conn.InsertAsync(new List<TEntity> { model }) > 0;
                }
            }
            catch (ArgumentNullException ex)
            {
                logger.LogError($"调用方法AddAsync(TEntity model)发生ArgumentNullException，异常信息：{ex}");
            }
            catch (SqlException ex)
            {
                logger.LogError($"调用方法AddAsync(TEntity model)发生SqlException，异常信息：{ex}");
            }
            catch (Exception ex)
            {
                logger.LogError($"调用方法AddAsync(TEntity model)发生Exception，异常信息：{ex}");
            }
            return false;
        }

        #endregion Methods Async
    }
}