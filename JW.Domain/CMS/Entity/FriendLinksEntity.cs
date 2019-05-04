using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.CMS.Entity
{
    /// <summary>
    /// 友情链接表实体
    /// </summary>
    [Table("C_FRIENDLINKS")]
    public class FriendLinksEntity : BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int FL_ID{get;set;}
        /// <summary>
        /// 链接名称
        /// </summary>
        public string FL_NAME { get; set; } = String.Empty;
        /// <summary>
        /// 链接提示
        /// </summary>
        public string FL_TITLE { get; set; } = String.Empty;
        /// <summary>
        /// 链接Logo
        /// </summary>
        public string FL_LOGOURL { get; set; } = String.Empty;
        /// <summary>
        /// 链接地址
        /// </summary>
        public string FL_WEBURL { get; set; } = String.Empty;
        /// <summary>
        /// 排序号
        /// </summary>
        public int FL_ORDERID { get; set; } = 0;
        /// <summary>
        /// 添加日期
        /// </summary>
        public DateTime FL_ADDTIME { get; set; } = DateTime.Now;
        /// <summary>
        /// 链接打开目标
        /// </summary>
        public int FL_TARGET { get; set; } = 0;
    } 
}
