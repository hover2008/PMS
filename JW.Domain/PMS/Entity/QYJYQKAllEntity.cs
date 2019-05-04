using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.PMS.Entity
{
    [Table("P_QYJYQKAll")]
    public class QYJYQKAllEntity : BaseEntity
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
        /// 当年总收入
        /// </summary>
        public decimal DNZSL { get; set; }

        /// <summary>
        /// 当年企业研发投入
        /// </summary>
        public decimal DNQYYFTR { get; set; }

        /// <summary>
        /// 工业总产值
        /// </summary>
        public decimal GYZCZ { get; set; }
        /// <summary>
        /// 当年利润总额
        /// </summary>
        public decimal DNLRZE { get; set; }

        /// <summary>
        /// 当年上交税金总额
        /// </summary>
        public decimal DNSJSJZE { get; set; }

        /// <summary>
        /// 当年地方财政贡献
        /// </summary>
        public decimal DNDFCZGX { get; set; }

        /// <summary>
        /// 当年累计获得财政资助额
        /// </summary>
        public decimal DNLJHDCZZZE { get; set; }

        /// <summary>
        /// 职工总数
        /// </summary>
        public int ZGZS { get; set; }

        /// <summary>
        /// 其中大专以上人数
        /// </summary>
        public int QZDZYSRS { get; set; }

        /// <summary>
        /// 其中硕士人数
        /// </summary>
        public int QZSSRS { get; set; }

        /// <summary>
        /// 其中博士人数
        /// </summary>
        public int QZBSRS { get; set; }

        /// <summary>
        /// 其中留学人数
        /// </summary>
        public int QZLXRS { get; set; }

        /// <summary>
        /// 其中千人计划人数
        /// </summary>
        public int QZQRJHRS { get; set; }

        /// <summary>
        /// 当年申请知识产权数
        /// </summary>
        public int DNSQZSCQS { get; set; }

        /// <summary>
        /// 当年批准知识产权数
        /// </summary>
        public int DNPZZSCQS { get; set; }
        /// <summary>
        /// 其中专利数
        /// </summary>
        public int QZZLS { get; set; }

        /// <summary>
        /// 其中发明专利数
        /// </summary>
        public int QZFMZLS { get; set; }

        /// <summary>
        /// 其中软件著作权数
        /// </summary>
        public int QZRJZZQS { get; set; }

        /// <summary>
        /// 其中集成电路布图数
        /// </summary>
        public int QZJCDLBTS { get; set; }

        /// <summary>
        /// 其中植物新品种数
        /// </summary>
        public int QZZWXPZS { get; set; }

        /// <summary>
        /// 购买国外技术专利数
        /// </summary>
        public int GMGWJSZLS { get; set; }

        /// <summary>
        /// 承担国家级科技项目数
        /// </summary>
        public int CDGJJKJXMS { get; set; }

        /// <summary>
        /// 获得国家级科技奖励数
        /// </summary>
        public int HDGJJKJJLS { get; set; }

        /// <summary>
        /// 主要研发生产
        /// </summary>
        public string ZYYHSC { get; set; }

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
