using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.PMS.Entity
{
    [Table("P_SKDMX")]
    public class SKDMXEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
		/// 单号
		/// </summary>
		public string DH { get; set; }

        /// <summary>
        /// 合同编号
        /// </summary>
        public string HTBH { get; set; }

        /// <summary>
        /// 批次
        /// </summary>
        public int PC { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int XH { get; set; }

        /// <summary>
        /// 收费款项名称
        /// </summary>
        public string SKMC { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime QSRQ { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime JSRQ { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal JR { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建用户编号
        /// </summary>
        public int CreateUserId { get; set; }

        /// <summary>
        /// 创建用户名称
        /// </summary>
        public string CreateUserName { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 最后更新用户编号
        /// </summary>
        public int LastUpdateUserId { get; set; }

        /// <summary>
        /// 最后更新用户名称
        /// </summary>
        public string LastUpdateUserName { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime { get; set; }
    }
}
