namespace JW.Core.Data.Dapper
{
    /// <summary>
    /// 数据库连接集合
    /// </summary>
    public class ConnectionStringList
    {
        /// <summary>
        /// 默认数据库连接信息(主要业务库执行读写）
        /// </summary>
        public string DefaultConn { get; set; }
    }
}
