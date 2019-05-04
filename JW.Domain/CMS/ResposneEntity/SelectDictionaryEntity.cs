namespace JW.Domain.CMS.ResposneEntity
{
    /// <summary>
    /// 下拉框选择字典数据实体
    /// </summary>
    public class SelectDictionaryEntity
    {
        /// <summary>
        /// 自增编号
        /// </summary> 
        public int D_ID { get; set; }
        /// <summary>
        /// 父级编号
        /// </summary>
        public int D_PARENTID { get; set; }
        /// <summary>
        /// 字典名称
        /// </summary>
        public string D_NAME { get; set; }
        /// <summary>
        /// 孩子数量
        /// </summary>
        public int D_CHILDREN { get; set; }
    }
}
