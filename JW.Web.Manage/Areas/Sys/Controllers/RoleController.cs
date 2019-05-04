using JW.Core.Data.Base;
using JW.Core.Extensions;
using JW.Core.ResponseResult;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using JW.Services.Sys.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.Sys.Controllers
{
    /// <summary>
    /// 系统角色模块控制器
    /// </summary>
    [Area(AreaNames.SysManage)]
    public class RoleController : BaseAdminController
    {
        #region Fields
        
        private readonly IRoleService<RoleEntity> roleService;
        private readonly IRoleMenuPurviewCodeService<RoleMenuPurviewCodeEntity> rmpcService;
        private readonly IRoleUserService<Role2UserEntity> roleUserService;
        private readonly IUserService<UserEntity> userService;
        private readonly IMenuService<MenuEntity> menuService;
        private readonly IMenuPurviewCodeService<MenuPurviewCodeEntity> mpcService;

        #endregion

        #region Ctor

        public RoleController(IWorkContext workContext,
            IRoleService<RoleEntity> roleService,
            IRoleMenuPurviewCodeService<RoleMenuPurviewCodeEntity> rmpcService,
            IRoleUserService<Role2UserEntity> roleUserService,
            IUserService<UserEntity> userService,
            IMenuService<MenuEntity> menuService,
            IMenuPurviewCodeService<MenuPurviewCodeEntity> mpcService)
            : base(workContext)
        {
            this.roleService = roleService;
            this.rmpcService = rmpcService;
            this.roleUserService = roleUserService;
            this.userService = userService;
            this.menuService = menuService;
            this.mpcService = mpcService; 
        }

        #endregion

        #region Methods

        #region 角色列表

        // GET: Role
        [HttpGet]
        [HandlerAuthorize("AC")]
        public IActionResult Index() => View();

        [HttpPost]  
        public async Task<IActionResult> GetGridJson(RoleSearchParam param)
        {
            BasePagedListModel<RoleEntity> pageDataModel = await roleService.GetListAsync(param); 
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        #endregion

        #region 新增/编辑角色

        [HttpPost]
        public IActionResult GetOrderId() => Json(roleService.GetMaxOrderId());

        [HttpGet]
        [HandlerAuthorize("AC-add|AC-edit")]
        public async Task<IActionResult> Form(int id)
        {
            var model = await roleService.GetModelByIdAsync(id) ?? new RoleEntity();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("AC-add|AC-edit")]
        public IActionResult SubmitForm(RoleEntity model) => Json(roleService.Save(model));

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("AC-del")]
        public async Task<IActionResult> BatchDeleteForm(List<int> ids) => Json(await roleService.DeleteByIdsAsync(ids));

        #endregion

        #region 权限相关

        [HttpGet]
        [HandlerAuthorize("AC-set")]
        public async Task<IActionResult> SetRuleForm(int roleId)
        {
            ViewBag.RoleId = roleId;
            StringBuilder html = new StringBuilder();
            BasePagedListModel<MenuEntity> pageDataModel = await menuService.GetCanUseListAsync();
            if (pageDataModel.Total > 0)
            {
                BasePagedListModel<MenuPurviewCodeEntity> menuPCList = await mpcService.GetCanUseListAsync(1, 100000);
                //初始化拥有的权限值
                string initPermission = "," + await roleService.GetPermissionByRoleIdAsync(roleId);
                foreach (MenuEntity item in pageDataModel.Data)
                {
                    string s = String.Empty;
                    byte level = Convert.ToByte(item.M_LEVEL);
                    for (byte i = 0; i < level; i++)
                    {
                        s += "——";
                    }
                    html.Append("<tr>");
                    html.AppendFormat("<td><label class=\"checkbox-inline i-checks\"><input type=\"checkbox\" name=\"chkMCodes\">{0}{1}</label></td>", s, item.M_NAME);
                    html.Append("<td>");
                    html.AppendFormat("<label class=\"checkbox-inline i-checks\"><input value=\"{0}\" type=\"checkbox\" name=\"chkPCodes\"{1}>浏览</label>", item.M_CODE, initPermission.Contains("," + item.M_CODE + ",") ? " checked=\"checked\"" : "");
                    if (menuPCList.Total > 0)
                    {
                        List<MenuPurviewCodeEntity> pcItems = menuPCList.Data.ToList().FindAll(x => x.M_ID == item.M_ID);
                        foreach (MenuPurviewCodeEntity pcItem in pcItems)
                        {
                            html.AppendFormat("<label class=\"checkbox-inline i-checks\"><input value=\"{0}\" type=\"checkbox\" name=\"chkPCodes\"{2}>{1}</label>", pcItem.MPC_CODE, pcItem.MPC_NAME, initPermission.Contains("," + pcItem.MPC_CODE + ",") ? " checked=\"checked\"" : "");
                        }
                    }
                    html.Append("</td>");
                    html.Append("</tr>");
                }
            }
            ViewBag.PCHtml = html.ToString();
            return View();
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("AC-set")]
        public async Task<IActionResult> SetRule(int roleId, List<string> codes)
        { 
            var userClaim = await workContext.GetCurrentUserClaim();
            Messages messages = await rmpcService.BatchSaveAsync(roleId, codes, userClaim);
            return Json(messages);
        }

        #endregion

        #region 角色用户相关

        [HttpGet]
        [HandlerAuthorize("AC-useradd")]
        public IActionResult GetUserForm(int roleId)
        {
            ViewBag.RoleId = roleId;
            return View();
        }

        [HttpPost]  
        public async Task<IActionResult> GetRoleUserList(RoleUserSearchParam param)
        {
            BasePagedListModel<Role2UserEntity> pageDataModel = await roleUserService.GetListByRoleIdAsync(param);
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("AC-userdel")]
        public async Task<IActionResult> RemoveUser(int roleId, List<int> userIds)
        {
            var userClaim = await workContext.GetCurrentUserClaim();
            Messages messages = await roleUserService.DeleteByIdsAsync(roleId, userIds, userClaim);
            return Json(messages);
        }

        [HttpGet]
        [HandlerAuthorize("AC-useradd")]
        public IActionResult AddUserForm(int roleId)
        {
            ViewBag.RoleId = roleId;
            return View();
        }

        [HttpPost]   
        public async Task<IActionResult> GetCanAddUser(RoleUserSearchParam param)
        {
            BasePagedListModel<UserEntity> pageDataModel = await userService.GetCanUserListByRoleIdAsync(param); 
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("AC-useradd")]
        public async Task<IActionResult> SubmitAddUser(int roleId, List<int> userIds)
        { 
            var userClaim = await workContext.GetCurrentUserClaim();
            Messages messages = await roleUserService.AddByUserIdsAsync(roleId, userIds, userClaim);
            return Json(messages);
        }

        #endregion

        #endregion
    }
}