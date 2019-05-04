using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;

namespace JW.Domain.Sys.Entity
{
    /// <summary>
    /// 系统用户表实体
    /// </summary>
    [Table("S_USER")]
    public class UserEntity : BaseEntity
    {
        /// <summary>
        /// 自增编号
        /// </summary>
        [Key]
        public int U_ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string U_NAME { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string U_PWD { get; set; }

        /// <summary>
        /// 混淆字符
        /// </summary>
        public string U_ENCRYPT { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public string U_REALNAME { get; set; }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public string U_EMAIL { get; set; }

        /// <summary>
        /// 手机号码（多个以空格隔开）
        /// </summary>
        public string U_MOBILE { get; set; }

        /// <summary>
        /// 座机号码（多个以空格隔开）
        /// </summary>
        public string U_TEL { get; set; }

        /// <summary>
        /// 上一次登录时间
        /// </summary>
        public DateTime? U_PREVLOGINTIME { get; set; }

        /// <summary>
        /// 上一次登录IP
        /// </summary>
        public string U_PREVLOGINIP { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? U_LASTLOGINTIME { get; set; }

        /// <summary>
        /// 最后登录IP
        /// </summary>
        public string U_LASTLOGINIP { get; set; }

        /// <summary>
        /// 登录次数
        /// </summary>
        public int U_LOGINTIMES { get; set; }

        /// <summary>
        /// 密码登录错误次数
        /// </summary>
        public int U_ERRORTIMES { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime U_CREATETIME { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime U_UPDATETIME { get; set; } = DateTime.Now;

        /// <summary>
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool U_DISABLED { get; set; }

        /// <summary>
        /// 最后修改密码时间
        /// </summary>
        public DateTime U_LASTMODIFYPASSWORDTIME { get; set; } = DateTime.Now;

        /// <summary>
        /// 锁屏状态（False-未锁屏，True-锁屏）
        /// </summary>
        public Boolean U_LOCKSCREEN { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public String U_PHOTO { get; set; } = String.Empty;
    }
}
