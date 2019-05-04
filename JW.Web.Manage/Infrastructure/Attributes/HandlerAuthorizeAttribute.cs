using JW.Core.Helper;
using JW.Core.Infrastructure;
using JW.Core.ResponseResult;
using JW.Services.Sys.IService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace JW.Web.Manage.Infrastructure.Attributes
{
    public class HandlerAuthorizeAttribute : ActionFilterAttribute
    {
        #region Fields

        //权限编码
        public string viewPurviewCode;
        //是否忽略权限认证
        public bool ignore;

        #endregion

        #region Ctor

        /// <summary>
        /// 权限认证过滤属性
        /// </summary>
        /// <param name="viewPurviewCode">权限编码，多个以"|"分隔</param>
        /// <param name="ignore">是否忽略权限认证</param>
        public HandlerAuthorizeAttribute(string viewPurviewCode = "", bool ignore = false)
        {
            this.viewPurviewCode = viewPurviewCode;
            this.ignore = ignore;
        }

        #endregion

        #region Methods

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        { 
            if (ignore)
                return;

            var webHelper = EngineContext.Current.Resolve<IWebHelper>();
            var workContext = EngineContext.Current.Resolve<IWorkContext>();
            var userCacheService= EngineContext.Current.Resolve<IUserCacheService>();
            var messages = EngineContext.Current.Resolve<MessagesData<String>>();
            var task = workContext.GetCurrentUserClaim();
            if (task.Result != null)
            {
                var cachedata = userCacheService.GetPermissionCacheAsync(task.Result.UserId).Result;
                bool permission = false;
                string[] code = viewPurviewCode.Split('|');
                foreach (var c in code)
                {
                    permission = cachedata.Any(item => item.Code == c);
                    if (permission)
                        break;
                }
                if (!permission)
                {
                    if (webHelper.IsAjax())
                    {
                        messages.Msg = "权限不足";
                        filterContext.Result = new JsonResult(messages);
                    }
                    else
                    {
                        filterContext.HttpContext.Response.Redirect("/page-no-permission");
                    }
                }
            }
            else
            {
                if (webHelper.IsAjax())
                {
                    messages.Msg = "请登录";
                    filterContext.Result = new JsonResult(messages);
                }
                else
                {
                    filterContext.HttpContext.Response.Redirect("/login");
                }
            }
            return;
        }

        #endregion

    }
}
