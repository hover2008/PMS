
using Dapper.Contrib.Extensions;

namespace JW.Domain.Sys.Entity
{
    /// <summary>
    /// 用户与角色关系表实体
    /// </summary>
    [Table("S_ROLE_USER")]
    public class RoleUserEntity : BaseEntity
    {
        /// <summary>
        /// 系统角色编号
        /// </summary>
        public int R_ID{ get; set; }

        /// <summary>
        /// 系统用户编号
        /// </summary>
        public int U_ID { get; set; }
    }  
}
