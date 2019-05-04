using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.PMS.Entity
{
    [Table("P_QYJYQK")]
    public class QYJYQKEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
		/// 序号
		/// </summary>
		public int XH { get; set; }
        /// <summary>
        /// 简称
        /// </summary>
        public string JC { get; set; }
        /// <summary>
        /// 报表时间
        /// </summary>
        public string BBSJ { get; set; }
        /// <summary>
        /// 总收入
        /// </summary>
        public decimal ZSR { get; set; }
        /// <summary>
        /// 工业总产值
        /// </summary>
        public decimal GYZCZ { get; set; }
        /// <summary>
        /// 净利润
        /// </summary>
        public decimal JLR { get; set; }
        /// <summary>
        /// 上缴税金
        /// </summary>
        public decimal SJSJ { get; set; }

        /// <summary>
        /// 职工总数
        /// </summary>
        public int ZGZS { get; set; }

        /// <summary>
        /// 其中大专以上
        /// </summary>
        public int QZDZYS { get; set; }

        /// <summary>
        /// 主要研发生产
        /// </summary>
        public string ZYYFSC { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string BH { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string MC { get; set; }

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
