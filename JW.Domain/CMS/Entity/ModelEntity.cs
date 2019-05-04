using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.CMS.Entity
{
    /// <summary>
    /// 模型表实体
    /// </summary>
    [Table("C_MODEL")]
    public class ModelEntity : BaseEntity
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Key]
        public int M_ID { get; set; }
        /// <summary>
        /// 模型名称
        /// </summary>
        public string M_NAME { get; set; }
        /// <summary>
        /// 模型描述
        /// </summary>
        public string M_DESCRIPTION { get; set; }
        /// <summary>
        /// 模型表名
        /// </summary>
        public string M_TABLENAME { get; set; }
        /// <summary>
        /// 后台管理URL
        /// </summary>
        public string M_MANAGEURL { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime M_CREATETIME { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int M_ORDERID { get; set; }
        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool M_DISABLED { get; set; }
    } 
}
