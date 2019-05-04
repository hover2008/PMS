using Newtonsoft.Json.Linq;

namespace JW.Web.Framework.UEditor.Handlers
{
    /// <summary>
    /// 配置文件处理者
    /// </summary>
    public class ConfigHandler  
    {
        public JObject Process()
        {
            return Config.Items;
        }
    }
}