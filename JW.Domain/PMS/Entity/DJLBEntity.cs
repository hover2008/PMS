using Dapper.Contrib.Extensions;

namespace JW.Domain.PMS.Entity
{
    [Table("P_DJLB")]
    public class DJLBEntity: BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 单据大类编号
        /// </summary>
        public int DJDLId { get; set; }

        /// <summary>
        /// 单据前缀
        /// </summary>
        public string DJQZ { get; set; }

        /// <summary>
        /// 单据名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}
