using Dapper.Contrib.Extensions;

namespace JW.Domain.Sys.Entity
{
    /// <summary>
    /// 角色编号对应的用户信息视图实体
    /// </summary>
    [Table("vw_S_ROLEUSER")]
    public class Role2UserEntity : UserEntity
    {
        /// <summary>
        /// 系统角色编号
        /// </summary>
        public int R_ID { get; set; }
    }
}
