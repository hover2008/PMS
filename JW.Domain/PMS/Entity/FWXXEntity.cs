using Dapper.Contrib.Extensions;

namespace JW.Domain.PMS.Entity
{
    [Table("P_FWXX")]
    public class FWXXEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 房屋编号
        /// </summary>
        public string FWBH { get; set; }

        /// <summary>
        /// 房屋位置
        /// </summary>
        public string FWWZ { get; set; }

        /// <summary>
        /// 房屋面积
        /// </summary>
        public string FWMJ { get; set; }

        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}
