using JW.Core.Encrypt;
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
    /// 栏目模块控制器
    /// </summary>
    [Area(AreaNames.CMSManage)]
    public class ColumnController : BaseAdminController
    {
        #region Fields

        private readonly DESEncrypt desEncrypt;
        private readonly IColumnService<ColumnEntity> columnService;

        #endregion

        #region Ctor

        public ColumnController(IWorkContext workContext,
            DESEncrypt desEncrypt,
            IColumnService<ColumnEntity> columnService)
            : base(workContext)
        {
            this.desEncrypt = desEncrypt;
            this.columnService = columnService; 
        }

        #endregion

        #region 栏目

        [HttpGet]
        [HandlerAuthorize("BE")]
        public IActionResult Index() => View();

        [HttpPost] 
        public async Task<IActionResult> GetTreeGridJson(string keyword)
        {

            IEnumerable<Column2Model2DictionaryEntity> list = await columnService.GetAllColumn2Model2DictionaryListAsync();
            var data = list.ToList();
            if (keyword.IsNotNullOrEmpty())
            {
                data = data.TreeWhere(t => t.C_NAME.Contains(keyword), "C_ID", "C_PARENTID");
            }
            var treeList = new List<TreeGridModel>();
            foreach (Column2Model2DictionaryEntity item in data)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = data.Count(t => t.C_PARENTID == item.C_ID) == 0 ? false : true;
                treeModel.id = item.C_ID;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.C_PARENTID;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        [HttpGet] 
        public async Task<IActionResult> GetTreeSelectJson(int id)
        {
            IEnumerable<SelectColumnEntity> list = await columnService.GetSelectCanUseListAsync(id); 
            var treeList = new List<TreeSelectModel>();
            foreach (SelectColumnEntity item in list)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.C_ID;
                treeModel.text = item.C_NAME;
                treeModel.parentId = item.C_PARENTID;
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAuthorize("BE-add|BE-addsingle|BE-addlink|BE-edit")]
        public async Task<IActionResult> Form(int id = 0, int typeid = 10002)
        {
            ColumnEntity model = await columnService.GetModelByIdAsync(id) ?? new ColumnEntity();
            if (id <= 0)
            {
                model.C_TYPEID = typeid;
            }
            if (typeid == 10003)
            {
                return View("SingleForm", model);
            }
            else if (typeid == 10004)
            {
                return View("OutLinkForm", model);
            }
            return View(model);
        }

        [HttpPost]
        [HandlerAuthorize("BE-add|BE-addsingle|BE-addlink|BE-edit")]
        [ValidateAntiForgeryToken]
        public IActionResult SubmitForm(ColumnEntity model) => Json(columnService.Save(model));

        #endregion

        #region 设置

        [HttpPost]
        [HandlerAuthorize("BE-up|BE-down")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Set(int id, string action) => Json(await columnService.SetAsync(id, action));

        #endregion
    }
}