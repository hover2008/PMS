using JW.Domain;
using Microsoft.AspNetCore.Mvc;

namespace JW.Web.Manage.Controllers
{
    /// <summary>
    /// 基本组件控制器
    /// </summary>
    public class CommonController : Controller
    {
        /// <summary>
        /// About
        /// </summary>
        /// <returns></returns>
        public IActionResult About() => View();

        /// <summary>
        /// Page not found
        /// </summary>
        /// <returns></returns>
        public IActionResult PageNotFound()
        {
            Response.StatusCode = 404;
            Response.ContentType = "text/html";
            return View();
        }

        /// <summary>
        /// 出错页面
        /// </summary>
        /// <returns></returns>
        public IActionResult Error() => View();

        /// <summary>
        /// 无访问权限提示页面
        /// </summary>
        /// <returns></returns>
        public IActionResult NoPermission() => View("ShowTips", new BasePromptModel("很抱歉！您的权限不足，访问被拒绝！"));
    }
}