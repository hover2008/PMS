using System.ComponentModel.DataAnnotations;

namespace JW.Domain.Sys.Enum
{
    /// <summary>
    /// 操作日志类型
    /// </summary>
    public enum OperatorLogEnum
    {
        [Display(Description = "其他")]
        Other = 0,
        [Display(Description = "登录")]
        Login = 1,
        [Display(Description = "退出")]
        Exit = 2,
        [Display(Description = "访问")]
        Visit = 3,
        [Display(Description = "新增")]
        Create = 4,
        [Display(Description = "删除")]
        Delete = 5,
        [Display(Description = "修改")]
        Update = 6,
    }
}
