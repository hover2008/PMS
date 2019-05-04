using Dapper.Contrib.Extensions;

namespace JW.Domain.Sys.Entity
{
    /// <summary>
    /// 系统菜单表实体
    /// </summary>
    [Table("S_MENU")]
    public class MenuEntity : BaseEntity
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Key]
        public int M_ID { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>
        public int M_PARENTID { get; set; }

        /// <summary>
        /// 排序编号
        /// </summary>
        public int M_ORDERID { get; set; }

        /// <summary>
        /// 菜单名称
        /// </summary>
        public string M_NAME { get; set; }

        /// <summary>
        /// 权限码
        /// </summary>
        public string M_CODE { get; set; }

        /// <summary>
        /// 路径
        /// </summary>
        public string M_PATH { get; set; }

        /// <summary>
        /// 排序路径
        /// </summary>
        public string M_ORDERPATH { get; set; }

        /// <summary>
        /// 层次（从0开始）
        /// </summary>
        public byte M_LEVEL { get; set; }

        /// <summary>
        /// ICON路径
        /// </summary>
        public string M_ICON { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string M_LINK { get; set; }

        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool M_DISABLED { get; set; }

        /// <summary>
        /// 孩子数量
        /// </summary>
        public int M_CHILDREN { get; set; }
    }
}
