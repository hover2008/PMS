using JW.Domain.Sys.ResposneEntity;
using System;
using System.Collections.Generic;

namespace JW.Domain.Sys.RequestParam
{
    public class UserParam
    {
        /// <summary>
        /// 自增编号
        /// </summary> 
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
        /// 是否禁用（1-已禁用，0-未禁用）
        /// </summary>
        public bool U_DISABLED { get; set; }
        /// <summary>
        /// 头像
        /// </summary>
        public String U_PHOTO { get; set; } = String.Empty;
        /// <summary>
        /// 角色选项列表
        /// </summary>
        public IEnumerable<SelectRoleEntity> RoleSelectList { get; set; }
        /// <summary>
        /// 拥有的角色编号
        /// </summary>
        public string RoleIds { get; set; } = String.Empty;
    }
}
