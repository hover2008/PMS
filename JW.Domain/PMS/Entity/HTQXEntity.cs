using Dapper.Contrib.Extensions;

namespace JW.Domain.PMS.Entity
{
    [Table("P_HTQX")]
    public class HTQXEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 合同期限
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 月份数
        /// </summary>
        public decimal Months { get; set; }

        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}
