namespace JW.Domain.Sys.RequestParam
{
    /// <summary>
    /// 角色搜索实体
    /// </summary>
    public class RoleSearchParam : BaseSearchEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }
    }
}
