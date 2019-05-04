
using Dapper.Contrib.Extensions;

namespace JW.Domain.Sys.Entity
{
    /// <summary>
    /// 系统菜单权限选项表实体
    /// </summary>
    [Table("S_MENU_PURVIEWCODE")]
    public class MenuPurviewCodeEntity : BaseEntity
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Key]
        public int MPC_ID { get; set; }

        /// <summary>
        /// 菜单编号
        /// </summary>
        public int M_ID { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string MPC_NAME { get; set; }

        /// <summary>
        /// 权限码
        /// </summary>
        public string MPC_CODE { get; set; }

        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool MPC_DISABLED { get; set; }
    }
}
