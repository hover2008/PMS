using System.Collections.Generic;

namespace JW.Core.Data.Base
{
    /// <summary>
    /// 分页模型
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BasePagedListModel<T> 
    {
        public BasePagedListModel()
        {
            this.Data = new List<T>();
        }

        /// <summary>
        /// 数据
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// 记录总行数
        /// </summary>
        public int Total { get; set; }
    }
}
