namespace JW.Domain.Sys.RequestParam
{
    /// <summary>
    /// 用户搜索实体
    /// </summary>
    public class UserSearchParam : BaseSearchEntity
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 真实姓名
        /// </summary> 
        public string RealName { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        public string Mobile { get; set; }
    }
}
