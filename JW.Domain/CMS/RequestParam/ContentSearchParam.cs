namespace JW.Domain.CMS.RequestParam
{
    /// <summary>
    /// 内容搜索实体
    /// </summary>
    public class ContentSearchParam : BaseSearchEntity
    {
        /// <summary>
        /// 栏目类型编号
        /// </summary>
        public int CId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public int Steps { get; set; } = -1;

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 副标题
        /// </summary>
        public string SubTitle { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string Summary { get; set; }
    }
}
