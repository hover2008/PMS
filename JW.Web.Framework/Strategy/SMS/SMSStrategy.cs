using System;
using System.Net;
using System.Text;

namespace JW.Web.Framework.Strategy
{
    /// <summary>
    /// 短信策略(用于助通短信行业平台）
    /// </summary>
    public class SMSStrategy : ISMSStrategy
    {
        private string url;
        private string userName;
        private string password;

        private string encoding = "utf-8";//"gb2312"

        /// <summary>
        /// 短信服务器地址
        /// </summary>
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        /// <summary>
        /// 短信账号
        /// </summary>
        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        /// <summary>
        /// 短信密码
        /// </summary>
        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        /// <summary>
        /// 发送短信
        /// </summary>
        /// <param name="to">接收人号码</param>
        /// <param name="body">短信内容</param>
        /// <returns>是否发送成功</returns>
        public bool Send(string to, string body)
        { 
            try
            { 
                string tkey = DateTime.Now.ToString("yyyyMMddHHmmss");
                string pwd = GetMd5(GetMd5(this.Password) + tkey);
                WebClient wb = new WebClient();
                string url = String.Format("{0}?username={1}&password={2}&tkey={3}&mobile={4}&content={5}&xh=", this.Url, this.UserName, pwd, tkey, to, body);
                byte[] PageSources = wb.DownloadData(url); 
                string Sources = Encoding.GetEncoding(encoding).GetString(PageSources);
                return Sources.IndexOf("1,") > -1;
            }
            catch
            {
                return false;
            }
        }
        private string GetMd5(string str)
        {
            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            return BitConverter.ToString(md5.ComputeHash(Encoding.UTF8.GetBytes(str))).Replace("-", null).ToLower();
        }
    }
}
