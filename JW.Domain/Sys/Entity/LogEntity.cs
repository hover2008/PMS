using Dapper.Contrib.Extensions;
using System;

namespace JW.Domain.Sys.Entity
{
    /// <summary>
    /// 系统操作日志信息表实体
    /// </summary>
    [Table("S_LOG")]
    public class LogEntity : BaseEntity
    {
        public LogEntity()
        {
        }

        public LogEntity(string action, string link, string method, string data, int userId, string userName, string ip)
        {
            this.L_ACTION = action;
            this.L_LINK = link;
            this.L_METHOD = method;
            this.L_DATA = data;
            this.U_ID = userId;
            this.U_NAME = userName;
            this.L_IP = ip;
            this.L_TIME = DateTime.Now;
        }

        /// <summary>
        /// 自增编号
        /// </summary>
        [Key]
        public int L_ID { get; set; }

        /// <summary>
        /// 动作（比如：添加，编辑，删除，查看）
        /// </summary>
        public string L_ACTION { get; set; }

        /// <summary>
        /// 操作链接
        /// </summary>
        public string L_LINK { get; set; }

        /// <summary>
        /// 操作方法
        /// </summary>
        public string L_METHOD { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public string L_DATA { get; set; }

        /// <summary>
        /// 操作用户编号
        /// </summary>
        public int U_ID { get; set; }

        /// <summary>
        /// 操作用户名
        /// </summary>
        public string U_NAME { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string L_IP { get; set; }

        /// <summary>
        /// 操作时间
        /// </summary>
        public DateTime L_TIME { get; set; }

        public override string ToString() => this?.L_DATA;
    } 
}
