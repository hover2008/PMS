using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.PMS.Entity
{
    [Table("P_RFXX")]
    public class RFXXEntity : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        /// <summary>
		/// 编号
		/// </summary>
		public string BH { get; set; }

        /// <summary>
        /// 企业注册时间
        /// </summary>
        public DateTime QYZCSJ { get; set; }

        /// <summary>
        /// 注册资金
        /// </summary>
        public string ZCZJ { get; set; }

        /// <summary>
        /// 法人代码
        /// </summary>
        public string FRDM { get; set; }

        /// <summary>
        /// 登记注册类型
        /// </summary>
        public string DJZCLX { get; set; }

        /// <summary>
        /// 行业类别
        /// </summary>
        public string HYLB { get; set; }

        /// <summary>
        /// 注册地址
        /// </summary>
        public string ZCDZ { get; set; }

        /// <summary>
        /// 主要经营情况
        /// </summary>
        public string ZYJYQK { get; set; }

        /// <summary>
        /// 入孵序号
        /// </summary>
        public int RFXH { get; set; }

        /// <summary>
        /// 入驻时间
        /// </summary>
        public DateTime RZSJ { get; set; }

        /// <summary>
        /// 毕业时间
        /// </summary>
        public DateTime BYSJ { get; set; }

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
