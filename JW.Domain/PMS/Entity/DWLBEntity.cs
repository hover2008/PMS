using Dapper.Contrib.Extensions;

namespace JW.Domain.PMS.Entity
{
    [Table("P_DWLB")]
    public class DWLBEntity: BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 分类代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>

        public string Name { get; set; }

        /// <summary>
        /// 父级编号
        /// </summary>

        public int PId { get; set; }

        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}
