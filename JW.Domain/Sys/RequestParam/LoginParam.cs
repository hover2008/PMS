using System.ComponentModel.DataAnnotations;

namespace JW.Domain.Sys.RequestParam
{
    public class LoginParam
    {
        /// <summary>
        /// 用户名
        /// </summary>  
        public string UserName { get; set; }
        /// <summary>
        /// 用户密码
        /// </summary>
        [DataType(DataType.Password)]
        public string UserPwd { get; set; }
        /// <summary>
        /// 用户验证码
        /// </summary>
        [MaxLength(4)]
        public string UserCode { get; set; }
    }
}
