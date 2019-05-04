namespace JW.Domain.CMS.RequestParam
{
    /// <summary>
    /// 广告搜索实体
    /// </summary>
    public class AdvertSearchParam : BaseSearchEntity
    {
        /// <summary>
        /// 类型编号
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
    }
}
