using JW.Domain;
using JW.Web.Manage.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JW.Web.Manage.Controllers
{
    [Authorize]
    public class BaseAdminController : Controller
    {
        #region Fields

        protected readonly IWorkContext workContext;

        #endregion

        #region Ctor

        public BaseAdminController(IWorkContext workContext)
        {
            this.workContext = workContext;
        }

        #endregion

        /// <summary>
        /// 提示信息视图
        /// </summary> 
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected IActionResult PromptView(string message)
        {
            return View("ShowTips", new BasePromptModel(message));
        }
        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="backUrl">返回地址</param>
        /// <param name="message">提示信息</param>
        /// <returns></returns>
        protected IActionResult PromptView(string backUrl, string message)
        {
            return View("ShowTips", new BasePromptModel(backUrl, message));
        }

        /// <summary>
        /// 提示信息视图
        /// </summary>
        /// <param name="backUrl">返回地址</param>
        /// <param name="message">提示信息</param>
        /// <param name="isAutoBack">是否自动返回</param>
        /// <returns></returns>
        protected IActionResult PromptView(string backUrl, string message, bool isAutoBack)
        {
            return View("ShowTips", new BasePromptModel(backUrl, message, isAutoBack));
        } 
    }
}