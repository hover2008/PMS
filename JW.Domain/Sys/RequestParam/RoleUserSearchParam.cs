namespace JW.Domain.Sys.RequestParam
{
    /// <summary>
    /// 角色用户搜索实体
    /// </summary>
    public class RoleUserSearchParam : BaseSearchEntity
    {
        /// <summary>
        /// 角色编号
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary> 
        public string RealName { get; set; }
    }
}
