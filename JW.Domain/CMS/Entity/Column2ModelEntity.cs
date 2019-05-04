using Dapper.Contrib.Extensions;

namespace JW.Domain.CMS.Entity
{
    /// <summary>
    /// 栏目-模型关系视图实体
    /// </summary>
    [Table("vw_C_COLUMN2MODEL")]
    public class Column2ModelEntity : BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int C_ID { get; set; }
        /// <summary>
        /// 模型编号
        /// </summary>
        public int M_ID { get; set; }
        /// <summary>
        /// 栏目类型（对应字典表，父级编号为10001 ）
        /// </summary>
        public int C_TYPEID { get; set; }
        /// <summary>
        /// 栏目父级编号
        /// </summary>
        public int C_PARENTID { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int C_ORDERID { get; set; }
        /// <summary>
        /// 层次（从0开始）
        /// </summary>
        public int C_LEVEL { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string C_NAME { get; set; }
        /// <summary>
        /// 栏目英文名称
        /// </summary>
        public string C_ENAME { get; set; }
        /// <summary>
        /// 栏目提示
        /// </summary>
        public string C_Tips { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string C_LINK { get; set; }
        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool C_DISABLED { get; set; }
        /// <summary>
        /// 孩子数量
        /// </summary>
        public int C_CHILDREN { get; set; }
        /// <summary>
        /// 后台管理URL
        /// </summary>
        public string M_MANAGEURL { get; set; }
    }
}
