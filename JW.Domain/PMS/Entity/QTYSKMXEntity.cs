using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.PMS.Entity
{
    [Table("P_QTYSKMX")]
    public class QTYSKMXEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
		/// 单号
		/// </summary>
		public string DH { get; set; }

        /// <summary>
        /// 序号
        /// </summary>
        public int XH { get; set; }

        /// <summary>
        /// 付款单位
        /// </summary>
        public string FKDW { get; set; }

        /// <summary>
        /// 收费款项名称
        /// </summary>
        public string SFKXMC { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime QSRQ { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime JSRQ { get; set; }

        /// <summary>
        /// 金额类型
        /// </summary>
        public string JELX { get; set; }

        /// <summary>
        /// 单价
        /// </summary>
        public decimal DJ { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public decimal SL { get; set; }

        /// <summary>
        /// 金额
        /// </summary>
        public decimal JE { get; set; }

        /// <summary>
        /// 附加金额
        /// </summary>
        public decimal FJJE { get; set; }

        /// <summary>
        /// 总金额
        /// </summary>
        public decimal ZJJ { get; set; }

        /// <summary>
        /// 缴付方式
        /// </summary>
        public string JFFS { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string SBBH { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        public string JBR { get; set; }

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
