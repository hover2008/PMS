using JW.Web.Framework.UEditor;
using Microsoft.AspNetCore.Http;

namespace Cba.Web.Framework.UEditor.Handlers
{
    /// <summary>
    /// 处理者抽象类
    /// </summary>
    public abstract class Handler
    {
        public Handler(HttpContext context)
        {
            this.Request = context.Request;
            this.Response = context.Response;
            this.Context = context;
        }

        public abstract UploadResult Process();

        public HttpRequest Request { get; private set; }
        public HttpResponse Response { get; private set; }
        public HttpContext Context { get; private set; }
    }
}