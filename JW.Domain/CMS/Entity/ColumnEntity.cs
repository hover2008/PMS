
using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.CMS.Entity
{
    /// <summary>
    /// 栏目表实体
    /// </summary>
    [Table("C_COLUMN")]
    public class ColumnEntity : BaseEntity
    {
        /// <summary>
        /// 编号
        /// </summary>
        [Key]
        public int C_ID { get; set; }
        /// <summary>
        /// 模型编号
        /// </summary>
        public int M_ID { get; set; }
        /// <summary>
        /// 栏目类型（对应字典表，父级编号为10001 ）
        /// </summary>
        public int C_TYPEID { get; set; }
        /// <summary>
        /// 栏目父级编号
        /// </summary>
        public int C_PARENTID { get; set; }
        /// <summary>
        /// 排序值
        /// </summary>
        public int C_ORDERID { get; set; }
        /// <summary>
        /// 层次（从0开始）
        /// </summary>
        public int C_LEVEL { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string C_NAME { get; set; }
        /// <summary>
        /// 栏目英文名称
        /// </summary>
        public string C_ENAME { get; set; }
        /// <summary>
        /// 栏目提示
        /// </summary>
        public string C_Tips { get; set; }
        /// <summary>
        /// 栏目图片路径
        /// </summary>
        public string C_IMAGE { get; set; } = String.Empty;
        /// <summary>
        /// 栏目摘要
        /// </summary>
        public string C_SUMMARY { get; set; }
        /// <summary>
        /// 是否是顶部菜单
        /// </summary>
        public bool C_ISMENU { get; set; }
        /// <summary>
        /// 是否是底部菜单
        /// </summary>
        public bool C_ISFOOTERMENU { get; set; }
        /// <summary>
        /// 链接地址
        /// </summary>
        public string C_LINK { get; set; }
        /// <summary>
        /// 打开方式（0-原窗口打开，1-新窗口打开）
        /// </summary>
        public byte C_OPENTYPE { get; set; }
        /// <summary>
        /// 此栏目下的内容打开方式(0-原窗口打开，1-新窗口打开）
        /// </summary>
        public byte C_ITEMOPENTYPE { get; set; }
        /// <summary>
        /// 路径
        /// </summary>
        public string C_PATH { get; set; }
        /// <summary>
        /// 排序路径
        /// </summary>
        public string C_ORDERPATH { get; set; }
        /// <summary>
        /// 栏目SEO标题
        /// </summary>
        public string C_MATETITLE { get; set; }
        /// <summary>
        /// 栏目SEO关键词
        /// </summary>
        public string C_MATEKEYWORDS { get; set; }
        /// <summary>
        /// 栏目SEO描述
        /// </summary>
        public string C_MATEDESCRIPTION { get; set; }
        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool C_DISABLED { get; set; }
        /// <summary>
        /// 工作流（0-不需要审核，1-一级审核，2-二级审核，3-三级审核，4-四级审核）
        /// </summary>
        public byte C_WORKFLOWID { get; set; }
        /// <summary>
        /// 单页内容
        /// </summary>
        public string C_CONTENT { get; set; }
        /// <summary>
        /// 孩子数量
        /// </summary>
        public int C_CHILDREN { get; set; }
    }
}
