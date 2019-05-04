using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.PMS.Entity
{
    [Table("P_WLDW")]
    public class WLDWEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 编号
        /// </summary>
        public string Num { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 简称
        /// </summary>
        public string SubName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public string Contacts { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 网址
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        public string PostCode { get; set; }

        /// <summary>
        /// 开户行
        /// </summary>
        public string OpeningBank { get; set; }

        /// <summary>
        /// 帐号
        /// </summary>
        public string Accounts { get; set; }

        /// <summary>
        /// 税号
        /// </summary>
        public string Tax { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 分类代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 档案序号
        /// </summary>
        public int FileNumber { get; set; }

        /// <summary>
        /// 是否收款单位
        /// </summary>
        public bool IsSKDW { get; set; }

        /// <summary>
        /// 预约收款月份差
        /// </summary>
        public int YYSKYFC { get; set; }

        /// <summary>
        /// 是否离场
        /// </summary>
        public bool IsLeave { get; set; }

        /// <summary>
        /// 离场时间
        /// </summary>
        public DateTime? LeaveTime { get; set; }

        /// <summary>
        /// 离场备注
        /// </summary>
        public string LeaveRemark { get; set; }

        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool IsDisabled { get; set; }
    }
}
