using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace JW.Core.Helper
{
    /// <summary>
    /// XML辅助类
    /// </summary>
    public partial class XmlHelper
    {
        #region Methods

        /// <summary>
        /// XML编码
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>Encoded string</returns>
        public static string XmlEncode(string str)
        {
            if (str == null)
                return null;
            str = Regex.Replace(str, @"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]", "", RegexOptions.Compiled);
            return XmlEncodeAsIs(str);
        }

        /// <summary>
        /// XML编码
        /// </summary>
        /// <param name="str">String</param>
        /// <returns>Encoded string</returns>
        public static string XmlEncodeAsIs(string str)
        {
            if (str == null)
                return null;
            using (var sw = new StringWriter())
            using (var xwr = new XmlTextWriter(sw))
            {
                xwr.WriteString(str);
                return sw.ToString();
            }
        }

        /// <summary>
        /// 对属性进行编码
        /// </summary>
        /// <param name="str">Attribute</param>
        /// <returns>编码后的属性</returns>
        public static string XmlEncodeAttribute(string str)
        {
            if (str == null)
                return null;
            str = Regex.Replace(str, @"[^\u0009\u000A\u000D\u0020-\uD7FF\uE000-\uFFFD]", "", RegexOptions.Compiled);
            return XmlEncodeAttributeAsIs(str);
        }

        /// <summary>
        /// 对属性进行编码
        /// </summary>
        /// <param name="str">Attribute</param>
        /// <returns>编码后的属性</returns>
        public static string XmlEncodeAttributeAsIs(string str)
        {
            return XmlEncodeAsIs(str).Replace("\"", "&quot;");
        }

        /// <summary>
        /// 解码一个属性
        /// </summary>
        /// <param name="str">Attribute</param>
        /// <returns>解码属性</returns>
        public static string XmlDecode(string str)
        {
            var sb = new StringBuilder(str);
            return sb.Replace("&quot;", "\"").Replace("&apos;", "'").Replace("&lt;", "<").Replace("&gt;", ">").Replace("&amp;", "&").ToString();
        }

        /// <summary>
        /// 序列化一个日期时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>序列化的日期时间</returns>
        public static string SerializeDateTime(DateTime dateTime)
        {
            var xmlS = new XmlSerializer(typeof(DateTime));
            var sb = new StringBuilder();
            using (var sw = new StringWriter(sb))
            {
                xmlS.Serialize(sw, dateTime);
                return sb.ToString();
            }
        }

        /// <summary>
        /// 反序列化一个日期时间
        /// </summary>
        /// <param name="dateTime">日期时间</param>
        /// <returns>反序列化的日期时间</returns>
        public static DateTime DeserializeDateTime(string dateTime)
        {
            var xmlS = new XmlSerializer(typeof(DateTime));
            using (var sr = new StringReader(dateTime))
            {
                var test = xmlS.Deserialize(sr);
                return (DateTime)test;
            }
        }

        #endregion
    }
}
