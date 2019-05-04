using Dapper.Contrib.Extensions;

namespace JW.Domain.CMS.Entity
{
    /// <summary>
    /// 内容-状态名关系视图实体
    /// </summary>
    [Table("vw_C_CONTENT")]
    public class Content2StatusNameEntity : ContentEntity
    {
        public string C_STATUSTXT { get; set; }
    }
}
