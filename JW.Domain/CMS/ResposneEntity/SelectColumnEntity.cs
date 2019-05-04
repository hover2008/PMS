namespace JW.Domain.CMS.ResposneEntity
{
    /// <summary>
    /// 下拉框选择栏目实体
    /// </summary>
    public class SelectColumnEntity
    {
        /// <summary>
        /// 编号
        /// </summary> 
        public int C_ID { get; set; }
        /// <summary>
        /// 栏目父级编号
        /// </summary>
        public int C_PARENTID { get; set; }
        /// <summary>
        /// 栏目名称
        /// </summary>
        public string C_NAME { get; set; }
    }
}
