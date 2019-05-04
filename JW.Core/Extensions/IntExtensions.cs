using System;

namespace JW.Core.Extensions
{
    /// <summary>
    /// Int���ͷ�����չ
    /// </summary>
    public static class IntExtensions
    {
        /// <summary>
        /// ת���ɵ�Ч��ö�ٶ���
        /// </summary>
        /// <typeparam name="T">����</typeparam>
        /// <param name="i">��Ҫת��������</param>
        /// <param name="defaultValue">Ĭ��ֵ</param>
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
