using System;
using System.Collections.Generic;
using System.Text;

namespace JW.Domain.CMS.RequestParam
{ 
    /// <summary>
    /// 模型搜索实体
    /// </summary>
    public class ModelSearchParam : BaseSearchEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
