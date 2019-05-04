using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace JW.Core.Helper
{
    /// <summary>
    /// 一个HTML辅助类
    /// </summary>
    public partial class HtmlHelper
    {
        #region Fields

        private readonly static Regex paragraphStartRegex = new Regex("<p>", RegexOptions.IgnoreCase);
        private readonly static Regex paragraphEndRegex = new Regex("</p>", RegexOptions.IgnoreCase);
        //private static Regex ampRegex = new Regex("&(?!(?:#[0-9]{2,4};|[a-z0-9]+;))", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        #endregion

        #region Utilities

        /// <summary>
        /// 确保输入字符只允许的HTML格式
        /// </summary>
        /// <param name="text">输入字符</param>
        /// <returns></returns>
        private static string EnsureOnlyAllowedHtml(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            const string allowedTags = "br,hr,b,i,u,a,div,ol,ul,li,blockquote,img,span,p,em,strong,font,pre,h1,h2,h3,h4,h5,h6,address,cite";

            var m = Regex.Matches(text, "<.*?>", RegexOptions.IgnoreCase);
            for (var i = m.Count - 1; i >= 0; i--)
            {
                var tag = text.Substring(m[i].Index + 1, m[i].Length - 1).Trim().ToLower();

                if (!IsValidTag(tag, allowedTags))
                {
                    text = text.Remove(m[i].Index, m[i].Length);
                }
            }

            return text;
        }

        /// <summary>
        /// 判断是否是有效的标签
        /// </summary>
        /// <param name="tag">标签</param>
        /// <param name="tags">标签组</param>
        /// <returns></returns>
        private static bool IsValidTag(string tag, string tags)
        {
            var allowedTags = tags.Split(',');
            if (tag.IndexOf("javascript") >= 0) return false;
            if (tag.IndexOf("vbscript") >= 0) return false;
            if (tag.IndexOf("onclick") >= 0) return false;

            var endchars = new [] { ' ', '>', '/', '\t' };

            var pos = tag.IndexOfAny(endchars, 1);
            if (pos > 0) tag = tag.Substring(0, pos);
            if (tag[0] == '/') tag = tag.Substring(1);

            foreach (var aTag in allowedTags)
            {
                if (tag == aTag) return true;
            }

            return false;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 格式化文本
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="stripTags">指示是否带标签的值</param>
        /// <param name="convertPlainTextToHtml">指示是否允许HTML的值</param>
        /// <param name="allowHtml">指示是否允许HTML的值</param>
        /// <param name="resolveLinks">指示是否要解决链接的值</param>
        /// <param name="addNoFollowTag">指示是否添加“NoFollow”标签的值</param>
        /// <returns>格式化后的文本</returns>
        public static string FormatText(string text, bool stripTags,
            bool convertPlainTextToHtml, bool allowHtml, 
            bool resolveLinks, bool addNoFollowTag)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            try
            {
                if (stripTags)
                {
                    text = StripTags(text);
                }


                text = allowHtml ? EnsureOnlyAllowedHtml(text) : WebUtility.HtmlEncode(text);


                if (convertPlainTextToHtml)
                {
                    text = ConvertPlainTextToHtml(text);
                }

                if (resolveLinks)
                {
                    text = ResolveLinksHelper.FormatText(text);
                }

                if (addNoFollowTag)
                {
                    //未实现
                }
            }
            catch (Exception exc)
            {
                text = $"Text cannot be formatted. Error: {exc.Message}";
            }
            return text;
        }

        /// <summary>
        /// 带脚本标签
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>格式化后的文本</returns>
        public static string StripTags(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = Regex.Replace(text, @"(>)(\r|\n)*(<)", "><");
            text = Regex.Replace(text, "(<[^>]*>)([^<]*)", "$2");
            text = Regex.Replace(text, "(&#x?[0-9]{2,4};|&quot;|&amp;|&nbsp;|&lt;|&gt;|&euro;|&copy;|&reg;|&permil;|&Dagger;|&dagger;|&lsaquo;|&rsaquo;|&bdquo;|&rdquo;|&ldquo;|&sbquo;|&rsquo;|&lsquo;|&mdash;|&ndash;|&rlm;|&lrm;|&zwj;|&zwnj;|&thinsp;|&emsp;|&ensp;|&tilde;|&circ;|&Yuml;|&scaron;|&Scaron;)", "@");

            return text;
        }

        /// <summary>
        /// 替换锚文本（从下面的URL中删除标签）：<a href="http://www.***.com">名称</a>，只输出字符串“名称”
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>格式化后的文本</returns>
        public static string ReplaceAnchorTags(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = Regex.Replace(text, @"<a\b[^>]+>([^<]*(?:(?!</a)<[^<]*)*)</a>", "$1", RegexOptions.IgnoreCase);
            return text;
        }

        /// <summary>
        /// 将纯文本转换为HTML(\r\n \r \n \t)
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>格式化后的文本</returns>
        public static string ConvertPlainTextToHtml(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = text.Replace("\r\n", "<br />");
            text = text.Replace("\r", "<br />");
            text = text.Replace("\n", "<br />");
            text = text.Replace("\t", "&nbsp;&nbsp;");
            text = text.Replace("  ", "&nbsp;&nbsp;");

            return text;
        }

        /// <summary>
        /// 将HTML转换为纯文本
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="decode">指示是否解码文本的值</param>
        /// <param name="replaceAnchorTags">替换锚文本（从下面的URL中删除标签）：<a href="http://www.***.com">名称</a>，只输出字符串“名称”</param>
        /// <returns>格式化后的文本</returns>
        public static string ConvertHtmlToPlainText(string text,
            bool decode = false, bool replaceAnchorTags = false)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;
            
            if (decode)
                text = WebUtility.HtmlDecode(text);

            text = text.Replace("<br>", "\n");
            text = text.Replace("<br >", "\n");
            text = text.Replace("<br />", "\n");
            text = text.Replace("&nbsp;&nbsp;", "\t");
            text = text.Replace("&nbsp;&nbsp;", "  ");

            if (replaceAnchorTags)
                text = ReplaceAnchorTags(text);

            return text;
        }

        /// <summary>
        /// 将文本转换为段落
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>格式化后的文本</returns>
        public static string ConvertPlainTextToParagraph(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = paragraphStartRegex.Replace(text, string.Empty);
            text = paragraphEndRegex.Replace(text, "\n");
            text = text.Replace("\r\n", "\n").Replace("\r", "\n");
            text = text + "\n\n";
            text = text.Replace("\n\n", "\n");
            var strArray = text.Split(new [] { '\n' });
            var builder = new StringBuilder();
            foreach (var str in strArray)
            {
                if ((str != null) && (str.Trim().Length > 0))
                {
                    builder.AppendFormat("<p>{0}</p>\n", str);
                }
            }
            return builder.ToString();
        }

        #endregion
    }
}
