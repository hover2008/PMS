using JW.Core.Data.Base;
using JW.Domain.PMS.Entity;
using JW.Domain.PMS.RequestParam;
using JW.Domain.PMS.ResposneEntity;
using JW.Domain.Shared;
using JW.Services.PMS.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.PMS.Controllers
{
    /// <summary>
    /// 收费款项
    /// </summary>
    [Area(AreaNames.PMSManage)]
    public class SFKXController : BaseAdminController
    {
        #region Fields

        private readonly ISFKXService<SFKXEntity> service;

        #endregion

        #region Ctor

        public SFKXController(IWorkContext workContext,
            ISFKXService<SFKXEntity> service)
            : base(workContext)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        #region 列表

        [HttpGet]
        [HandlerAuthorize("CE")]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> GetGridJson(SFKXSearchParam param)
        {
            BasePagedListModel<SFKXEntity> pageDataModel = await service.GetListAsync(param);
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        [HttpGet]
        [HandlerAuthorize("CE-add|CE-edit")]
        public async Task<IActionResult> Form(int id = 0)
        {
            var model = await service.GetModelByIdAsync(id) ?? new SFKXEntity();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("CE-add|CE-edit")]
        public IActionResult SubmitForm(SFKXEntity model) => Json(service.Save(model));

        #endregion

        #region 设置

        [HttpPost]
        [HandlerAuthorize("CE-edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Set(int id, bool action) => Json(await service.UpdateDisabledByIdAsync(id, action));

        #endregion 

        [HttpGet]
        public async Task<IActionResult> GetSelectJson()
        {
            IEnumerable<SelectSFKXEntity> data = await service.GetSelectCanUseListAsync();
            var treeList = new List<SelectModel>();
            foreach (SelectSFKXEntity item in data)
            {
                SelectModel treeModel = new SelectModel();
                treeModel.id = item.Id;
                treeModel.text = item.Name;
                treeList.Add(treeModel);
            }
            return Json(treeList);
        }

        #endregion
    }
}