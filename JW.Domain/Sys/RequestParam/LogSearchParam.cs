using System;
using System.Collections.Generic;
using System.Text;

namespace JW.Domain.Sys.RequestParam
{
    /// <summary>
    /// 日志搜索实体
    /// </summary>
    public class LogSearchParam : BaseSearchEntity
    {
        /// <summary>
        /// 搜索关键字
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 搜索范围
        /// </summary>
        public string Range { get; set; } = "all";
    }
}
