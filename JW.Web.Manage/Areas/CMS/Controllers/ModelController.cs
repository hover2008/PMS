using JW.Core.Data.Base;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.RequestParam;
using JW.Domain.CMS.ResposneEntity;
using JW.Domain.Shared;
using JW.Services.CMS.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.CMS.Controllers
{
    /// <summary>
    /// 系统模型模块控制器
    /// </summary>
    [Area(AreaNames.CMSManage)]
    public class ModController : BaseAdminController
    {
        #region Fields

        private readonly IModelService<ModelEntity> modService;

        #endregion

        #region Ctor

        public ModController(IWorkContext workContext,
            IModelService<ModelEntity> modService)
            : base(workContext)
        {
            this.modService = modService;
        }

        #endregion

        #region Methods

        #region 列表

        [HttpGet]
        [HandlerAuthorize("BF")]
        public IActionResult Index() => View();

        [HttpPost] 
        public async Task<IActionResult> GetGridJson(ModelSearchParam param)
        {
            BasePagedListModel<ModelEntity> pageDataModel = await modService.GetListAsync(param);
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        } 

        [HttpGet]
        [HandlerAuthorize("BF-add|BF-edit")]
        public async Task<IActionResult> Form(int id = 0)
        {
            var model = await modService.GetModelByIdAsync(id) ?? new ModelEntity();
            if (id==0)
            {
                model.M_ORDERID = modService.GetMaxOrderId();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("BF-add|BF-edit")]
        public IActionResult SubmitForm(ModelEntity model) => Json(modService.Save(model));

        #endregion

        #region 设置

        [HttpPost]
        [HandlerAuthorize("BF-edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Set(int id, bool action) => Json(await modService.UpdateDisabledByIdAsync(id, action));

        #endregion

        #region 更新排序

        [HttpPost]
        [HandlerAuthorize("BF-order")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrder(List<int> ids, List<int> orderids) => Json(modService.UpdateOrderId(ids, orderids));

        #endregion

        [HttpGet]
        public async Task<IActionResult> GetSelectJson()
        {
            IEnumerable<SelectModelEntity> data = await modService.GetSelectCanUseListAsync();
            var treeList = new List<SelectModel>();
            foreach (SelectModelEntity item in data)
            {
                SelectModel treeModel = new SelectModel();
                treeModel.id = item.M_ID;
                treeModel.text = item.M_NAME;
                treeList.Add(treeModel);
            }
            return Json(treeList);
        }

        #endregion

    }
}