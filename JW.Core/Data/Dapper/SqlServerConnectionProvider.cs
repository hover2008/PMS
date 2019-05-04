using Microsoft.Extensions.Options;
using System;
using System.Data;
using System.Data.SqlClient;

namespace JW.Core.Data.Dapper
{
    /// <summary>
    /// SqlServer数据库连接驱动
    /// </summary>
    public class SqlServerConnectionProvider : IConnectionProvider
    {
        private readonly IOptions<ConnectionStringList> options;

        public SqlServerConnectionProvider(IOptions<ConnectionStringList> options)
        {
            this.options = options; 
        }

        /// <summary>
        /// 创建数据库连接
        /// </summary>
        /// <param name="enum">数据库连接枚举</param>
        /// <returns></returns>
        public IDbConnection CreateConn(DbConnectionEnum @enum = DbConnectionEnum.Default)
        {
            if (options.Value == null)
                throw new ArgumentNullException("There's no database connection configuration section registered. Please, register the section in appsettings.json or user secrets.");

            switch(@enum)
            { 
                default:

                    if (string.IsNullOrEmpty(options.Value?.DefaultConn))
                        throw new ArgumentNullException("请配置默认连接数据库信息(主要业务库执行读写)");

                    return new SqlConnection(options.Value.DefaultConn);
            }
        }
    }
}
