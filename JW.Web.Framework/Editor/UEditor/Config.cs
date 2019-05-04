using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Linq;

namespace JW.Web.Framework.UEditor
{
    /// <summary>
    /// Config ��ժҪ˵��
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
        /// ��ȡ�����û��������ơ���������������Զ�����Ϊ��ASPNETCORE����������������ֵ
        /// </summary>
        public static string EnvName { get; set; }
        /// <summary>
        /// ��ȡ�����ð���Ӧ�ó���Ŀ¼�ľ���·��
        /// </summary>
        public static string ContentRootPath { get; set; }
        /// <summary>
        /// ��ȡ�����ð�����web�ŷ�Ӧ�ó��������ļ���Ŀ¼�ľ���·��
        /// </summary>
        public static string WebRootPath { get; set; }
        /// <summary>
        /// �����ļ���
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
                    throw new Exception("δ�ҵ�UEditor�����ļ������飡�������⣬������ĵ���https://github.com/baiyunchen/UEditor.Core");
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