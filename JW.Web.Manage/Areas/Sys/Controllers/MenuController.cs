using JW.Core.Data.Base;
using JW.Core.Extensions;
using JW.Core.Helper;
using JW.Domain.Shared;
using JW.Domain.Sys.Entity;
using JW.Services.Sys.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.Sys.Controllers
{
    /// <summary>
    /// 系统菜单模块控制器
    /// </summary>
    [Area(AreaNames.SysManage)]
    public class MenuController : BaseAdminController
    {
        #region Fields
         
        private readonly IUserCacheService userCacheService;
        private readonly IMenuService<MenuEntity> menuService;
        private readonly IMenuPurviewCodeService<MenuPurviewCodeEntity> menuPurviewCodeService;
        private readonly IUserService<UserEntity> userService; 

        #endregion

        #region Ctor

        public MenuController(IWorkContext workContext,
            IUserCacheService userCacheService,
            IMenuService<MenuEntity> menuService,
            IMenuPurviewCodeService<MenuPurviewCodeEntity> menuPurviewCodeService,
            IUserService<UserEntity> userService)
            : base(workContext)
        { 
            this.userCacheService = userCacheService;
            this.menuService = menuService;
            this.menuPurviewCodeService = menuPurviewCodeService;
            this.userService = userService; 
        }

        #endregion

        #region Methods

        #region 菜单列表

        [HttpGet]
        [HandlerAuthorize("AA")]
        public IActionResult Index() => View();

        [HttpPost]  
        public async Task<IActionResult> GetTreeGridJson(string keyword)
        {
            BasePagedListModel<MenuEntity> pageDataModel = await menuService.GetAllListAsync(0);
            var data = pageDataModel.Data.ToList();
            if (keyword.IsNotNullOrEmpty())
            {
                data = data.TreeWhere(t => t.M_NAME.Contains(keyword));
            }
            var treeList = new List<TreeGridModel>();
            foreach (MenuEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.M_PARENTID == item.M_ID) == 0 ? false : true;
                treeModel.id = item.M_ID;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.M_PARENTID;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        [HttpGet] 
        public async Task<IActionResult> GetTreeSelectJson(int id)
        {
            BasePagedListModel<MenuEntity> pageDataModel = await menuService.GetAllListAsync(id);
            var treeList = new List<TreeSelectModel>();
            foreach (MenuEntity item in pageDataModel.Data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.M_ID;
                treeModel.text = item.M_NAME;
                treeModel.parentId = item.M_PARENTID;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        #endregion

        #region 新增/编辑菜单

        [HttpGet]
        [HandlerAuthorize("AA-add|AA-edit")]
        public async Task<IActionResult> Form(int id=0)
        {
            var model = await menuService.GetModelByIdAsync(id) ?? new MenuEntity();
            return View(model);
        }

        [HttpPost]
        [HandlerAuthorize("AA-add|AA-edit")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(MenuEntity model) => Json(menuService.Save(model));

        #endregion

        #region 菜单权限项

        [HttpGet]
        [HandlerAuthorize("AA-set")]
        public IActionResult PCForm(int id, string code)
        {
            ViewBag.M_ID = id;
            ViewBag.M_CODE = code;
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> GetPCList(int mid, int pageIndex = 1, int pageSize = 10)
        {
            BasePagedListModel<MenuPurviewCodeEntity> pageDataModel = await menuPurviewCodeService.GetListByMenuIdAsync(pageIndex, pageSize, mid);
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        [HttpPost]
        [HandlerAuthorize("AA-set")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitPCForm(MenuPurviewCodeEntity model, string mcode) => Json(menuPurviewCodeService.Save(model, mcode));

        #endregion

        #region 菜单设置

        [HttpPost]
        [HandlerAuthorize("AA-up|AA-down")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Set(int id, string action) => Json(await menuService.SetAsync(id, action));

        #endregion

        #region 获取权限按钮

        [HttpPost] 
        public async Task<IActionResult> GetAuthorizeButton(string code)
        {
            var userClaim = await workContext.GetCurrentUserClaim();
            var cachedata = await userCacheService.GetPermissionCacheAsync(userClaim.UserId);
            var pattern = new Regex(@"^" + code + "-+");
            var query = from data in cachedata
                        let matches = pattern.Matches(data.Code)
                        where matches.Count > 0
                        select data; 
            return Json(query);
        }

        #endregion

        #endregion
    }
}