
using Dapper.Contrib.Extensions;

namespace JW.Domain.Sys.Entity
{
    /// <summary>
    /// 角色拥有的权限选项表实体
    /// </summary>
    [Table("S_ROLE_MENU_PURVIEWCODE")]
    public class RoleMenuPurviewCodeEntity : BaseEntity
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public int R_ID { get; set; }

        /// <summary>
        /// 权限编码
        /// </summary>
        public string MPC_CODE { get; set; }
    }
}
