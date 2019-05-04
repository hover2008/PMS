using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.CMS.Entity
{
    /// <summary>
    /// 内容模型
    /// </summary>
    [Table("C_CONTENT")]
    public class ContentEntity: BaseEntity
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Key]
        public int C_ID { get; set; }
        /// <summary>
        /// 栏目编号
        /// </summary>
        public int CAT_ID { get; set; }
        /// <summary>
        /// 点击数
        /// </summary>
        public int C_HITS { get; set; }
        /// <summary>
        /// 标题
        /// </summary>
        public string C_TITLE { get; set; }
        /// <summary>
        /// 简标题
        /// </summary>
        public string C_SUBTITLE { get; set; }
        /// <summary>
        /// 图片地址
        /// </summary>
        public string C_IMAGEURL { get; set; } = String.Empty;
        /// <summary>
        /// 内容
        /// </summary>
        public string C_CONTENT { get; set; }
        /// <summary>
        /// 摘要
        /// </summary>
        public string C_SUMMARY { get; set; }
        /// <summary>
        /// 作者
        /// </summary>
        public string C_AUTHOR { get; set; }
        /// <summary>
        /// 来源
        /// </summary>
        public string C_SOURCE { get; set; }
        /// <summary>
        /// 关键字
        /// </summary>
        public string C_KEYWORDS { get; set; }
        /// <summary>
        /// 状态等级（0-退稿，1-等待初审，2-等待二审，3-等待三审，4-等待终审，99-终审通过）
        /// </summary>
        public int C_STATUS { get; set; }
        /// <summary>
        /// 权重（越小越靠前）
        /// </summary>
        public int C_WEIGHT { get; set; }
        /// <summary>
        /// 添加者用户名
        /// </summary>
        public string C_ADDUSERNAME { get; set; }
        /// <summary>
        /// 添加时间
        /// </summary>
        public DateTime C_ADDTIME { get; set; } = DateTime.Now;
        /// <summary>
        /// 最后编辑者用户名
        /// </summary>
        public string C_LASTEDITUSERNAME { get; set; }
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime C_LASTEDITTIME { get; set; } = DateTime.Now;
        /// <summary>
        /// 是否置顶（0-否，1-是）
        /// </summary>
        public bool C_ISTOP { get; set; }
        /// <summary>
        /// 是否推荐（0-否，1-是）
        /// </summary>
        public bool C_ISREC { get; set; }
        /// <summary>
        /// 是否权限阅读（0-否，1-是）
        /// </summary>
        public bool C_ISPER { get; set; }
        /// <summary>
        /// 是否幻灯片（0-否，1-是）
        /// </summary>
        public bool C_ISSLIDE { get; set; }
    }
}
