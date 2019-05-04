
using Dapper.Contrib.Extensions;

namespace JW.Domain.CMS.Entity
{
    /// <summary>
    /// 字典数据表实体
    /// </summary>
    [Table("C_DICTIONARY")]
    public class DictionaryEntity : BaseEntity
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Key]
        public int D_ID { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        public int D_PARENTID { get; set; }
        /// <summary>
        /// 排序编号
        /// </summary>
        public int D_ORDERID { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string D_NAME { get; set; } 
        /// <summary>
        /// 路径
        /// </summary>
        public string D_PATH { get; set; }
        /// <summary>
        /// 排序路径
        /// </summary>
        public string D_ORDERPATH { get; set; }
        /// <summary>
        /// 层次（从0开始）
        /// </summary>
        public byte D_LEVEL { get; set; } 
        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool D_DISABLED { get; set; }
        /// <summary>
        /// 孩子数量
        /// </summary>
        public int D_CHILDREN { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string D_REMARK { get; set; }
    }
}
