using JW.Core.Encrypt;
using JW.Core.Extensions;
using JW.Domain.Sys.Entity;
using JW.Services.CMS.IService;
using JW.Services.Sys.IService;
using JW.Web.Manage.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JW.Web.Manage.Controllers
{
    public class HomeController : BaseAdminController
    {
        #region Fields

        private readonly IUserCacheService userCacheService;
        private readonly IUserService<UserEntity> userService;
        private readonly IMenuService<MenuEntity> menuService;
        private readonly ISettingService settingService;
        private StringBuilder menuHtml = new StringBuilder();

        #endregion

        #region Ctor

        public HomeController(IWorkContext workContext,
            IUserCacheService userCacheService,
            IUserService<UserEntity> userService,
            IMenuService<MenuEntity> menuService,
            ISettingService settingService)
            : base(workContext)
        {
            this.userCacheService = userCacheService;
            this.userService = userService;
            this.menuService = menuService;
            this.settingService = settingService;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            UserEntity user = await workContext.GetCurrentUser();
            if (user != null)
            {
                string roleIds = await userService.GetRoleByUserIdAsync(user.U_ID);
                List<MenuEntity> data = (await menuService.GetListByRolesAsync(roleIds)).ToList();
                List<MenuEntity> entitys = data.FindAll(t => t.M_PARENTID == 0);
                foreach (var item in entitys)
                {
                    menuHtml.Append("<li>");
                    string link = "#", style = "";
                    if (item.M_LINK.Length > 0)
                    {
                        link = item.M_LINK;
                        style = " class=\"J_menuItem\"";
                    }
                    menuHtml.AppendFormat("<a href=\"{0}\"{1}>", link, style);
                    menuHtml.AppendFormat("<i class=\"fa {0}\"></i>", item.M_ICON);
                    menuHtml.AppendFormat("<span class=\"nav-label\">{0}</span>", item.M_NAME);
                    menuHtml.AppendFormat("<span class=\"fa{0}\"></span>", item.M_CHILDREN > 0 ? " arrow" : "");
                    menuHtml.Append("</a>");
                    if (item.M_CHILDREN > 0)
                    {
                        ResolveSubTree(data, item.M_ID, item.M_LEVEL);
                    }
                    menuHtml.Append("</li>");
                }
                ViewBag.MenuNav = menuHtml.ToString();
                return View(user);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        /// <summary>
        /// 退出登录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            userCacheService.Clear();
            return RedirectToAction("Index", "Login");
        }

        /// <summary>
        /// 清理缓存
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult ClearCache()
        {
            userCacheService.Clear();
            settingService.ClearSettingsSingleton();
            return PromptView("清理缓存成功！");
        }

        /// <summary>
        /// 锁屏
        /// </summary>
        /// <returns></returns> 
        public async Task<IActionResult> LockScreen(string userpwd)
        {
            UserEntity currentUser = await workContext.GetCurrentUser();
            if (userpwd.IsNotNullOrEmpty())
            {
                string pwd1 = MD5Encrypt.GetPass(userpwd, currentUser.U_ENCRYPT);
                if (pwd1 == currentUser.U_PWD)
                {
                    await userService.SetLockScreenAsync(currentUser.U_ID, false);
                    return RedirectToAction("Index", "Home");
                }
                ViewData["msg"] = "密码错误，请重新输入";
            }
            else
            {
                await userService.SetLockScreenAsync(currentUser.U_ID, true);
            }
            return View(currentUser);
        }

        /// <summary>
        /// 生成菜单树
        /// </summary> 
        /// <param name="source">数据元</param> 
        /// <param name="pid">父IP</param>
        /// <param name="plevel">父级层次</param>
        [NonAction]
        private void ResolveSubTree(List<MenuEntity> data, int pid, byte plevel)
        {
            List<MenuEntity> entitys = data.FindAll(t => t.M_PARENTID == pid);
            string ulStyle = " nav-second-level";
            if (plevel > 0)
            {
                ulStyle = " nav-third-level";
            }
            menuHtml.AppendFormat("<ul class=\"nav{0}\">", ulStyle);
            foreach (var item in entitys)
            {
                menuHtml.Append("<li>");
                string link = "#", style = "";
                if (item.M_LINK.Length > 0)
                {
                    link = item.M_LINK;
                    style = " class=\"J_menuItem\"";
                }
                menuHtml.AppendFormat("<a href=\"{0}\"{1}>{2}", link, style, item.M_NAME);
                if (item.M_CHILDREN > 0)
                {
                    menuHtml.Append("<span class=\"fa arrow\"></span>");
                }
                menuHtml.Append("</a>");
                if (item.M_CHILDREN > 0)
                {
                    ResolveSubTree(data, item.M_ID, item.M_LEVEL);
                }
                menuHtml.Append("</li>");
            }
            menuHtml.Append("</ul>");
        }

        #endregion

    }
}
