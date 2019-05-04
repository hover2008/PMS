using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;

namespace JW.Core.ComponentModel
{
    /// <summary>
    /// 泛型字典类型转换
    /// </summary>
    /// <typeparam name="K">Key type (simple)</typeparam>
    /// <typeparam name="V">Value type (simple)</typeparam>
    public class GenericDictionaryTypeConverter<K, V> : TypeConverter
    {
        #region Fields

        /// <summary>
        /// Type converter
        /// </summary>
        protected readonly TypeConverter typeConverterKey;
        /// <summary>
        /// Type converter
        /// </summary>
        protected readonly TypeConverter typeConverterValue;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        public GenericDictionaryTypeConverter()
        {
            typeConverterKey = TypeDescriptor.GetConverter(typeof(K));
            if (typeConverterKey == null)
                throw new InvalidOperationException("No type converter exists for type " + typeof(K).FullName);
            typeConverterValue = TypeDescriptor.GetConverter(typeof(V));
            if (typeConverterValue == null)
                throw new InvalidOperationException("No type converter exists for type " + typeof(V).FullName);
        }

        #endregion

        #region Methods

        /// <summary>
        /// 获取一个值，指示该转换器是否可以使用上下文将给定源类型中的对象转换成转换器的本机类型
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="sourceType">Source type</param>
        /// <returns>Result</returns>
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;

            return base.CanConvertFrom(context, sourceType);
        }

        /// <summary>
        /// 将给定的对象转换为转换器的本机类型
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="culture">Culture</param>
        /// <param name="value">Value</param>
        /// <returns>Result</returns>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                var input = (string)value;
                var items = string.IsNullOrEmpty(input) ? new string[0] : input.Split(';').Select(x => x.Trim()).ToArray();

                var result = new Dictionary<K,V>();
                Array.ForEach(items, s =>
                {
                    var keyValueStr = string.IsNullOrEmpty(s) ? new string[0] : s.Split(',').Select(x => x.Trim()).ToArray();
                    if (keyValueStr.Length == 2)
                    {
                        object dictionaryKey = (K)typeConverterKey.ConvertFromInvariantString(keyValueStr[0]);
                        object dictionaryValue = (V)typeConverterValue.ConvertFromInvariantString(keyValueStr[1]);
                        if (dictionaryKey != null && dictionaryValue != null)
                        {
                            if (!result.ContainsKey((K)dictionaryKey))
                                result.Add((K) dictionaryKey, (V) dictionaryValue);
                        }
                    }
                });

                return result;
            }
            return base.ConvertFrom(context, culture, value);
        }

        /// <summary>
        /// 使用指定的上下文和参数将给定值对象转换为指定的目的地类型
        /// </summary>
        /// <param name="context">Context</param>
        /// <param name="culture">Culture</param>
        /// <param name="value">Value</param>
        /// <param name="destinationType">Destination type</param>
        /// <returns>Result</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var result = string.Empty;
                if (value != null)
                {
                    //不使用string.join（）因为它不支持不变文化
                    var counter = 0;
                    var dictionary = (IDictionary<K, V>)value;
                    foreach (var keyValue in dictionary)
                    {
                        result += $"{Convert.ToString(keyValue.Key, CultureInfo.InvariantCulture)}, {Convert.ToString(keyValue.Value, CultureInfo.InvariantCulture)}";
                        //在最后一个元素之后不要添加逗号
                        if (counter != dictionary.Count - 1)
                            result += ";";
                        counter++;
                    }
                }
                return result;
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }

        #endregion
    }
}
