
using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.CMS.Entity
{
    /// <summary>
    /// 广告表实体
    /// </summary>
    [Table("C_ADVERT")]
    public class AdvertEntity : BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int A_ID { get; set; }
        /// <summary>
        /// 点击数
        /// </summary>
        public int A_CLICKCOUNT { get; set; }
        /// <summary>
        /// 所属类型
        /// </summary>
        public int A_TYPEID { get; set; }
        /// <summary>
        /// 状态（0-不显示，1-显示）
        /// </summary>
        public byte A_STATE { get; set; } = 1;
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime A_STARTTIME { get; set; } = DateTime.Now;
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime A_ENDTIME { get; set; } = DateTime.Now.AddDays(1);
        /// <summary>
        /// 标题
        /// </summary>
        public string A_TITLE { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string A_URL { get; set; }
        /// <summary>
        /// 图片路径
        /// </summary>
        public string A_PicUrl { get; set; } = String.Empty;
        /// <summary>
        /// 排序值
        /// </summary>
        public int A_ORDERID { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime A_AddDate { get; set; } = DateTime.Now;
        /// <summary>
        /// 添加人
        /// </summary>
        public string A_AddMan { get; set; }
        /// <summary>
        /// 简介
        /// </summary>
        public string A_SUMMARY { get; set; }
    }  
}
