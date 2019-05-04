using JW.Web.Framework.UEditor;
using Microsoft.AspNetCore.Http;

namespace Cba.Web.Framework.UEditor.Handlers
{
    /// <summary>
    /// 无支持的处理者
    /// </summary>
    public class NotSupportedHandler : Handler
    {
        public NotSupportedHandler(HttpContext context)
            : base(context)
        {
        }

        public override UploadResult Process()
        {
            return new UploadResult
            {
                State = "action 参数为空或者 action 不被支持。"
            };
        }
    }
}