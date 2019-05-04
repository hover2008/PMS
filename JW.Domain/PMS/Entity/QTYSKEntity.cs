﻿using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.PMS.Entity
{
    [Table("P_QTYSK")]
    public class QTYSKEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
		/// 单号
		/// </summary>
		public string DH { get; set; }

        /// <summary>
        /// 单据类别
        /// </summary>
        public string DJLB { get; set; }

        /// <summary>
        /// 单据日期
        /// </summary>
        public DateTime DJRQ { get; set; }

        /// <summary>
        /// 收款单位
        /// </summary>
        public string SKDW { get; set; }

        /// <summary>
        /// 数值系数
        /// </summary>
        public decimal SZXS { get; set; }

        /// <summary>
        /// 经办人
        /// </summary>
        public string JBR { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

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
