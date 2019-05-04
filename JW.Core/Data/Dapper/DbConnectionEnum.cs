using System.ComponentModel;

namespace JW.Core.Data.Dapper
{
    /// <summary>
    /// 数据库连接字符串类型枚举
    /// </summary>
    public enum DbConnectionEnum
    {
        [Description("默认数据库连接信息(主要业务库执行读写）")]
        Default =0
    }
}
