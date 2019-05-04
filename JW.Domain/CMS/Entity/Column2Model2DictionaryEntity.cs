using Dapper.Contrib.Extensions;

namespace JW.Domain.CMS.Entity
{
    /// <summary>
    /// 栏目-模型-字典关系视图实体
    /// </summary>
    [Table("vw_C_COLUMN2MODEL2DICTIONARY")]
    public class Column2Model2DictionaryEntity : ColumnEntity
    {
        /// <summary>
        /// 所属模型名称
        /// </summary>
        public string M_NAME { get; set; }
        /// <summary>
        /// 栏目类型名称
        /// </summary>
        public string D_NAME { get; set; }
    }
}
