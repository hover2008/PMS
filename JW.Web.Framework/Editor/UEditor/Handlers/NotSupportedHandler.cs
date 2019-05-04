using JW.Web.Framework.UEditor;
using Microsoft.AspNetCore.Http;

namespace Cba.Web.Framework.UEditor.Handlers
{
    /// <summary>
    /// ��֧�ֵĴ�����
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
                State = "action ����Ϊ�ջ��� action ����֧�֡�"
            };
        }
    }
}