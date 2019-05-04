using System;
using System.Collections.Generic;
using System.Text;

namespace JW.Domain.PMS.RequestParam
{
    public class WLDWSearchParam : BaseSearchEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public string Num { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
