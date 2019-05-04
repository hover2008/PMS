using JW.Core.Extensions;
using JW.Core.Helper;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.ResposneEntity;
using JW.Domain.Shared;
using JW.Services.CMS.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.CMS.Controllers
{
    /// <summary>
    /// 字典数据模块控制器
    /// </summary>
    [Area(AreaNames.CMSManage)]
    public class DicController : BaseAdminController
    {
        #region Fields
         
        private readonly IDictionaryService<DictionaryEntity> dicService; 

        #endregion

        #region Ctor

        public DicController(IWorkContext workContext,
            IDictionaryService<DictionaryEntity> dicService)
            : base(workContext)
        { 
            this.dicService = dicService; 
        }

        #endregion

        #region Methods

        #region 列表

        [HttpGet]
        [HandlerAuthorize("BG")]
        public IActionResult Index() => View();

        [HttpPost] 
        public async Task<IActionResult> GetTreeGridJson(string keyword)
        {
            List<DictionaryEntity> list = (await dicService.GetAllListAsync()).ToList();
            if (keyword.IsNotNullOrEmpty())
            {
                list = list.TreeWhere(t => t.D_NAME.Contains(keyword), "D_ID", "D_PARENTID");
            }
            var treeList = new List<TreeGridModel>();
            foreach (DictionaryEntity item in list)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = list.Count(t => t.D_PARENTID == item.D_ID) == 0 ? false : true;
                treeModel.id = item.D_ID;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.D_PARENTID;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        } 

        [HttpGet]
        public async Task<IActionResult> GetTreeSelectJson(int id)
        {
            IEnumerable<SelectDictionaryEntity> data = await dicService.GetSelectCanUseListAsync(id); 
            var treeList = new List<TreeSelectModel>();
            foreach (SelectDictionaryEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.D_ID;
                treeModel.text = item.D_NAME;
                treeModel.parentId = item.D_PARENTID;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAuthorize("BG-add|BG-edit")]
        public async Task<IActionResult> Form(int id = 0)
        {
            var model = await dicService.GetModelByIdAsync(id) ?? new DictionaryEntity();
            return View(model);
        }

        [HttpPost]
        [HandlerAuthorize("BG-add|BG-edit")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(DictionaryEntity model) => Json(dicService.Save(model));

        #endregion

        #region 设置

        [HttpPost]
        [HandlerAuthorize("BG-down|BG-up")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Set(int id, string action) => Json(await dicService.SetAsync(id, action));

        #endregion

        #endregion
    }
}