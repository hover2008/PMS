namespace JW.Domain
{
    /// <summary>
    /// 搜索基类实体
    /// </summary>
    public class BaseSearchEntity
    {
        /// <summary>
        /// 页索引
        /// </summary>
        public int PageIndex { get; set; } = 1;

        /// <summary>
        /// 页大小
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// 排序字段名
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// 排序值（DESC、ASC）
        /// </summary>
        public string SortOrder { get; set; }
    }
}
