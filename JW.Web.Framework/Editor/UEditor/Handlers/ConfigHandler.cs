using Newtonsoft.Json.Linq;

namespace JW.Web.Framework.UEditor.Handlers
{
    /// <summary>
    /// �����ļ�������
    /// </summary>
    public class ConfigHandler  
    {
        public JObject Process()
        {
            return Config.Items;
        }
    }
}