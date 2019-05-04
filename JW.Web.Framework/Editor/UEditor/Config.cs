using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace JW.Web.Framework.UEditor
{
    /// <summary>
    /// Config 的摘要说明
    /// </summary>
    public static class Config
    {
        #region Fields

        private static JObject items;

        #endregion 

        #region Properties

        public static bool NoCache = true;
        public static JObject Items
        {
            get
            {
                if (NoCache || items == null)
                {
                    items = BuildItems();
                }
                return items;
            }
        }
        /// <summary>
        /// 获取或设置环境的名称。这个属性由主机自动设置为“ASPNETCORE环境”环境变量的值
        /// </summary>
        public static string EnvName { get; set; }
        /// <summary>
        /// 获取或设置包含应用程序目录的绝对路径
        /// </summary>
        public static string ContentRootPath { get; set; }
        /// <summary>
        /// 获取或设置包含可web伺服应用程序内容文件的目录的绝对路径
        /// </summary>
        public static string WebRootPath { get; set; }
        /// <summary>
        /// 配置文件名
        /// </summary>
        public static string ConfigFile { set; get; } = "ueditor.json";

        #endregion

        #region Utilities

        private static JObject BuildItems()
        {
            var configExtension = Path.GetExtension(ConfigFile);
            var configFileName = ConfigFile.Substring(0, ConfigFile.Length - configExtension.Length);
            var evnConfig = $"{configFileName}.{Config.EnvName}{configExtension}";
            if (File.Exists(Path.Combine(ContentRootPath, evnConfig)))
            {
                var json = File.ReadAllText(Path.Combine(ContentRootPath, evnConfig));
                return JObject.Parse(json);
            }
            else
            {
                var configFilePath = Path.Combine(ContentRootPath, ConfigFile);
                if (!File.Exists(configFilePath))
                {
                    throw new Exception("未找到UEditor配置文件，请检查！若有问题，请参阅文档：https://github.com/baiyunchen/UEditor.Core");
                }
                var json = File.ReadAllText(configFilePath);
                return JObject.Parse(json);
            }
        }

        #endregion

        #region Methods

        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        public static String[] GetStringList(string key)
        {
            return Items[key].Select(x => x.Value<String>()).ToArray();
        }

        public static String GetString(string key)
        {
            return GetValue<String>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }

        #endregion
    }
}