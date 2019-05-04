using System;

namespace JW.Core.Extensions
{
    /// <summary>
    /// Int类型方法扩展
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// 转换成等效的枚举对象
        /// </summary>
        /// <typeparam name="T">类型</typeparam>
        /// <param name="i">需要转换的数字</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns></returns>
        public static T ToEnum<T>(this int i, T defaultValue) where T : struct, IComparable, IFormattable
        {
            T convertedValue;

            if (!System.Enum.TryParse(i.ToString(), true, out convertedValue))
            {
                convertedValue = defaultValue;
            } 
            return convertedValue;
        }
    }
}
