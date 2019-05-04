using JW.Core.Configuration;
using JW.Core.Data.Base;
using JW.Core.Encrypt;
using JW.Core.ResponseResult;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using JW.Services.Sys.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.Sys.Controllers
{
    /// <summary>
    /// 系统用户模块控制器
    /// </summary>
    [Area(AreaNames.SysManage)]
    public class UserController : BaseAdminController
    {
        #region Fields

        private readonly SysManageSecurityConfig config;
        private readonly DESEncrypt desEncrypt;
        private readonly IUserService<UserEntity> userService;
        private readonly IRoleService<RoleEntity> roleService; 

        #endregion

        #region Ctor

        public UserController(
            IWorkContext workContext,
            SysManageSecurityConfig config,
            DESEncrypt desEncrypt,
            IUserService<UserEntity> userService,
            IRoleService<RoleEntity> roleService)
            : base(workContext)
        {
            this.config = config;
            this.desEncrypt = desEncrypt;
            this.userService = userService;
            this.roleService = roleService; 
        }

        #endregion

        #region Methods

        #region 系统用户列表

        [HttpGet]
        [HandlerAuthorize("AB")]
        public IActionResult Index() => View();

        [HttpPost] 
        public async Task<IActionResult> GetGridJson(UserSearchParam userSearchEntity)
        {
            BasePagedListModel<UserEntity> pageDataModel = await userService.GetListAsync(userSearchEntity); 
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        #endregion

        #region 新增/编辑系统用户

        [HttpGet]
        [HandlerAuthorize("AB-add|AB-edit")]
        public async Task<IActionResult> Form(int id = 0)
        {
            UserEntity model = await userService.GetModelByIdAsync(id) ?? new UserEntity();
            UserParam userModel = new UserParam()
            {
                U_ID = model.U_ID,
                U_NAME = model.U_NAME,
                //初始密码（来自系统安全配置）
                U_PWD = model.U_PWD ?? (config.InitPwd ?? ""),
                U_REALNAME = model.U_REALNAME,
                U_EMAIL = model.U_EMAIL,
                U_TEL = model.U_TEL,
                U_DISABLED = model.U_DISABLED,
                U_PHOTO = model.U_PHOTO,
                RoleSelectList = await roleService.GetSelectListAsync(),
                RoleIds = id > 0 ? "," + await userService.GetRoleByUserIdAsync(id) : ""
            };
            return View(userModel);
        }

        [HttpPost]
        [HandlerAuthorize("AB-add|AB-edit")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(UserParam model) => Json(userService.Save(model));

        #endregion

        #region 查看

        [HttpGet]
        public async Task<IActionResult> Detials(int id) => View(await userService.GetDetialsEntityAsync(id));

        #endregion

        #region 设置

        [HttpPost]
        [HandlerAuthorize("AB-edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Set(int id, bool action) => Json(await userService.UpdateDisabledByIdAsync(id, action));

        #endregion

        #region 修改密码

        [HttpGet]
        public async Task<IActionResult> ModifyPwd() => View(await workContext.GetCurrentUser());

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitModifyPwd(string opwd, string npwd)
        {
            UserEntity user = await workContext.GetCurrentUser();
            Messages messages = await userService.ChangePasswordAsync(user, opwd, npwd);
            if (messages.Success)
                await HttpContext.SignOutAsync();
            return Json(messages);
        }

        #endregion

        #region 修改头像

        [HttpGet]
        public async Task<IActionResult> ChangePhoto() => View(await workContext.GetCurrentUser());

        [HttpPost] 
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitChangePhoto(string photo)
        {
            var userClaim = await workContext.GetCurrentUserClaim();
            Messages messages = await userService.ModifyPhotoAsync(userClaim.UserId, photo);
            return Json(messages);
        }
        #endregion

        #endregion

    }
}