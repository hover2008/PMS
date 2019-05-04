
using Dapper.Contrib.Extensions;

namespace JW.Domain.Sys.Entity
{
    /// <summary>
    /// 系统用户角色表实体
    /// </summary>
    [Table("S_ROLE")]
    public class RoleEntity : BaseEntity
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Key]
        public int R_ID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string R_NAME { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string R_DESC { get; set; }

        /// <summary>
        /// 排序值
        /// </summary>
        public int R_ORDERID { get; set; }
    } 
}
