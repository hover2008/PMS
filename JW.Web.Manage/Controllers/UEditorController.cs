using JW.Web.Framework.UEditor;
using Microsoft.AspNetCore.Mvc;

namespace JW.Web.Manage.Controllers
{
    public class UEditorController : Controller
    {
        #region Fields

        private readonly UploadService uploadService;

        #endregion

        #region Ctor

        public UEditorController(UploadService uploadService)
        {
            this.uploadService = uploadService;
        }

        #endregion

        public IActionResult Do()
        {
            var response = uploadService.UploadAndGetResponse(HttpContext);
            return Content(response.Result, response.ContentType);
        }
    }
}