using Dapper.Contrib.Extensions;

namespace JW.Domain.PMS.Entity
{
    [Table("P_SFKX")]
    public class SFKXEntity: BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 收费款项名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 默认单位
        /// </summary>
        public string DefaultUnit { get; set; }

        /// <summary>
        /// 默认计算方式
        /// </summary>
        public string DefaultMode { get; set; }

        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}
