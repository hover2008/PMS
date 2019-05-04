using JW.Core.Extensions;
using JW.Core.Helper;
using JW.Domain.PMS.Entity;
using JW.Domain.PMS.ResposneEntity;
using JW.Domain.Shared;
using JW.Services.PMS.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.PMS.Controllers
{
    /// <summary>
    /// 单位类别
    /// </summary>
    [Area(AreaNames.PMSManage)]
    public class DWLBController : BaseAdminController
    {
        #region Fields

        private readonly IDWLBService<DWLBEntity> service;

        #endregion

        #region Ctor

        public DWLBController(IWorkContext workContext,
            IDWLBService<DWLBEntity> service)
            : base(workContext)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        #region 列表

        [HttpGet]
        [HandlerAuthorize("CA")]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> GetTreeGridJson(string name)
        {
            List<DWLBEntity> list = (await service.GetAllListAsync()).ToList();
            if (name.IsNotNullOrEmpty())
            {
                list = list.TreeWhere(t => t.Name.Contains(name), "Id", "PId");
            }
            var treeList = new List<TreeGridModel>();
            foreach (DWLBEntity item in list)
            {
                TreeGridModel treeModel = new TreeGridModel();
                bool hasChildren = list.Count(t => t.PId == item.Id) == 0 ? false : true;
                treeModel.id = item.Id;
                treeModel.isLeaf = hasChildren;
                treeModel.parentId = item.PId;
                treeModel.expanded = hasChildren;
                treeModel.entityJson = item.ToJson();
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeGridJson());
        }

        [HttpGet]
        public async Task<IActionResult> GetTreeSelectJson(int id = 0, bool disabled = false)
        {
            IEnumerable<SelectDWLBEntity> data = await service.GetSelectCanUseListAsync(id);
            var treeList = new List<TreeSelectModel>();
            foreach (SelectDWLBEntity item in data)
            {
                TreeSelectModel treeModel = new TreeSelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeModel.parentId = item.PId;
                if (disabled)
                {
                    treeModel.disabled = item.PId == 0;
                }
                treeList.Add(treeModel);
            }
            return Content(treeList.TreeSelectJson());
        }

        [HttpGet]
        [HandlerAuthorize("CA-add|CA-edit")]
        public async Task<IActionResult> Form(int id = 0)
        {
            var model = await service.GetModelByIdAsync(id) ?? new DWLBEntity();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("CA-add|CA-edit")]
        public IActionResult SubmitForm(DWLBEntity model) => Json(service.Save(model));

        #endregion

        #region 设置

        [HttpPost]
        [HandlerAuthorize("CA-edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Set(int id, bool action) => Json(await service.UpdateDisabledByIdAsync(id, action));

        #endregion 

        #endregion
    }
}