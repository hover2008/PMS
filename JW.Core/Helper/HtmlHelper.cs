using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace JW.Core.Helper
{
    /// <summary>
    /// һ��HTML������
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
        /// ȷ�������ַ�ֻ�����HTML��ʽ
        /// </summary>
        /// <param name="text">�����ַ�</param>
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
        /// �ж��Ƿ�����Ч�ı�ǩ
        /// </summary>
        /// <param name="tag">��ǩ</param>
        /// <param name="tags">��ǩ��</param>
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
        /// ��ʽ���ı�
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="stripTags">ָʾ�Ƿ����ǩ��ֵ</param>
        /// <param name="convertPlainTextToHtml">ָʾ�Ƿ�����HTML��ֵ</param>
        /// <param name="allowHtml">ָʾ�Ƿ�����HTML��ֵ</param>
        /// <param name="resolveLinks">ָʾ�Ƿ�Ҫ������ӵ�ֵ</param>
        /// <param name="addNoFollowTag">ָʾ�Ƿ���ӡ�NoFollow����ǩ��ֵ</param>
        /// <returns>��ʽ������ı�</returns>
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
                    //δʵ��
                }
            }
            catch (Exception exc)
            {
                text = $"Text cannot be formatted. Error: {exc.Message}";
            }
            return text;
        }

        /// <summary>
        /// ���ű���ǩ
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>��ʽ������ı�</returns>
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
        /// �滻ê�ı����������URL��ɾ����ǩ����<a href="http://www.***.com">����</a>��ֻ����ַ��������ơ�
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>��ʽ������ı�</returns>
        public static string ReplaceAnchorTags(string text)
        {
            if (string.IsNullOrEmpty(text))
                return string.Empty;

            text = Regex.Replace(text, @"<a\b[^>]+>([^<]*(?:(?!</a)<[^<]*)*)</a>", "$1", RegexOptions.IgnoreCase);
            return text;
        }

        /// <summary>
        /// �����ı�ת��ΪHTML(\r\n \r \n \t)
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>��ʽ������ı�</returns>
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
        /// ��HTMLת��Ϊ���ı�
        /// </summary>
        /// <param name="text">Text</param>
        /// <param name="decode">ָʾ�Ƿ�����ı���ֵ</param>
        /// <param name="replaceAnchorTags">�滻ê�ı����������URL��ɾ����ǩ����<a href="http://www.***.com">����</a>��ֻ����ַ��������ơ�</param>
        /// <returns>��ʽ������ı�</returns>
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
        /// ���ı�ת��Ϊ����
        /// </summary>
        /// <param name="text">Text</param>
        /// <returns>��ʽ������ı�</returns>
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
