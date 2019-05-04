using System.ComponentModel;

namespace JW.Domain.CMS.Enum
{
    /// <summary>
    /// 系统设置类型结构
    /// </summary>
    public enum SettingEnum
    {
        [Description("站点配置")]
        Site = 1,
        [Description("上传配置")]
        Attachment = 2,
        [Description("邮箱设置")]
        Email = 3,
        [Description("短信配置")]
        SMS = 4,
    }
}
