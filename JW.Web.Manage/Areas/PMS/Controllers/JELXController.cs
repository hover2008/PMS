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
    /// 金额类型
    /// </summary>
    [Area(AreaNames.PMSManage)]
    public class JELXController : BaseAdminController
    {
        #region Fields

        private readonly IJELXService<JELXEntity> service;

        #endregion

        #region Ctor

        public JELXController(IWorkContext workContext,
            IJELXService<JELXEntity> service)
            : base(workContext)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        #region 列表

        [HttpGet]
        [HandlerAuthorize("CJ")]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> GetGridJson(JELXSearchParam param)
        {
            BasePagedListModel<JELXEntity> pageDataModel = await service.GetListAsync(param);
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        [HttpGet]
        [HandlerAuthorize("CJ-add|CJ-edit")]
        public async Task<IActionResult> Form(int id = 0)
        {
            var model = await service.GetModelByIdAsync(id) ?? new JELXEntity();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("CJ-add|CJ-edit")]
        public IActionResult SubmitForm(JELXEntity model) => Json(service.Save(model));

        #endregion

        #region 设置

        [HttpPost]
        [HandlerAuthorize("CJ-edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Set(int id, bool action) => Json(await service.UpdateDisabledByIdAsync(id, action));

        #endregion 

        [HttpGet]
        public async Task<IActionResult> GetSelectJson()
        {
            IEnumerable<SelectJELXEntity> data = await service.GetSelectCanUseListAsync();
            var treeList = new List<SelectModel>();
            foreach (SelectJELXEntity item in data)
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