using System.ComponentModel.DataAnnotations;

namespace JW.Core
{
    /// <summary>
    /// 移动类型
    /// </summary>
    public enum MoveType
    {
        [Display(Description = "上移")] 
        Up = 1, 
        [Display(Description = "下移")]
        Down = 2
    }
}