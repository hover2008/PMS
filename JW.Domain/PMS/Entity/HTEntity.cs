using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.PMS.Entity
{
    [Table("P_HT")]
    public class HTEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
		/// 合同编号
		/// </summary>
		public string HTBH { get; set; }

        /// <summary>
        /// 付款单位
        /// </summary>
        public string FKDW { get; set; }

        /// <summary>
        /// 房屋编号
        /// </summary>
        public string FWBH { get; set; }

        /// <summary>
        /// 房屋面积
        /// </summary>
        public decimal FWMJ { get; set; }

        /// <summary>
        /// 合同期限
        /// </summary>
        public string HTQX { get; set; }

        /// <summary>
        /// 起始日期
        /// </summary>
        public DateTime QSRQ { get; set; }

        /// <summary>
        /// 结束日期
        /// </summary>
        public DateTime JSRQ { get; set; }

        /// <summary>
        /// 违约条款
        /// </summary>
        public string WYTK { get; set; }

        /// <summary>
        /// 其它补充条款
        /// </summary>
        public string QTBCTK { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 单位代表
        /// </summary>
        public string DWDB { get; set; }

        /// <summary>
        /// 签订日期
        /// </summary>
        public DateTime QDRQ { get; set; }

        /// <summary>
        /// 制单人
        /// </summary>
        public string ZDR { get; set; }

        /// <summary>
        /// 审核人
        /// </summary>
        public string SHR { get; set; }

        /// <summary>
        /// 是否完成
        /// </summary>
        public bool IsFinish { get; set; }

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
