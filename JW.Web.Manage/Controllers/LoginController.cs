using JW.Core;
using JW.Core.Captch;
using JW.Core.Encrypt;
using JW.Core.Extensions;
using JW.Core.Helper;
using JW.Core.ResponseResult;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using JW.Services.Sys.IService;
using JW.Web.Manage.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace JW.Web.Manage.Controllers
{
    public class LoginController : Controller
    {
        #region Fields

        private readonly IWorkContext workContext;
        private readonly IVerifyCode verifyCode;
        private readonly IWebHelper webHelper;
        private readonly IUserCacheService userCacheService;
        private readonly Messages messages;
        private readonly IUserService<UserEntity> userService; 

        #endregion

        #region Ctor

        public LoginController(IWorkContext workContext,
            IVerifyCode verifyCode,
            IWebHelper webHelper,
            IUserCacheService userCacheService,
            Messages messages,
            IUserService<UserEntity> userService)
        {
            this.workContext = workContext;
            this.verifyCode = verifyCode;
            this.webHelper = webHelper;
            this.userCacheService = userCacheService;
            this.messages = messages;
            this.userService = userService;
        }

        #endregion

        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 获取验证码
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetCaptch()
        {
            byte[] captch = verifyCode.GetCaptch(workContext.UserKey);
            return File(captch, MimeTypes.ImageGif);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CheckLogin(LoginParam loginModel)
        {
            if (loginModel != null &&
                loginModel.UserName.IsNotNullOrEmpty() &&
                loginModel.UserPwd.IsNotNullOrEmpty() &&
                loginModel.UserCode.IsNotNullOrEmpty())
            {
                if (MD5Encrypt.MD5By16(loginModel.UserCode.ToLower()) != webHelper.GetSession(workContext.UserKey))
                {
                    messages.Msg = "验证码错误，请重新输入";
                }
                else
                {
                    var result = await userService.Login(loginModel.UserName, loginModel.UserPwd);
                    if (result.Succeeded)
                    {
                        //记住登录凭证
                        var claims = new List<Claim>
                                    {
                                        //用户编号||用户名
                                        new Claim(ClaimTypes.Name, result.UserId.ToString()+"|||"+loginModel.UserName)
                                    };
                        ClaimsIdentity userIdentity = new ClaimsIdentity(claims, "login");
                        ClaimsPrincipal principal = new ClaimsPrincipal(userIdentity);

                        await HttpContext.SignInAsync(principal);
                        //缓存权限
                        await userCacheService.SetPermissionCacheAsync(result.UserId);
                    }
                    messages.Success = result.Succeeded;
                    messages.Msg = result.Msg;
                }
            }
            else
            {
                messages.Msg = "请填写完整";
            }
            return Json(messages);
        }
    }
}