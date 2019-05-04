using JW.Core.Data.Base;
using JW.Domain.PMS.Entity;
using JW.Domain.PMS.RequestParam;
using JW.Services.PMS.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.PMS.Controllers
{
    /// <summary>
    /// 房屋信息
    /// </summary>
    [Area(AreaNames.PMSManage)]
    public class FWXXController : BaseAdminController
    {
        #region Fields

        private readonly IFWXXService<FWXXEntity> service;

        #endregion

        #region Ctor

        public FWXXController(IWorkContext workContext,
            IFWXXService<FWXXEntity> service)
            : base(workContext)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        #region 列表

        [HttpGet]
        [HandlerAuthorize("CI")]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> GetGridJson(FWXXSearchParam param)
        {
            BasePagedListModel<FWXXEntity> pageDataModel = await service.GetListAsync(param);
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        [HttpGet]
        [HandlerAuthorize("CI-add|CI-edit")]
        public async Task<IActionResult> Form(int id = 0)
        {
            var model = await service.GetModelByIdAsync(id) ?? new FWXXEntity();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("CI-add|CI-edit")]
        public IActionResult SubmitForm(FWXXEntity model) => Json(service.Save(model));

        #endregion

        #region 设置

        [HttpPost]
        [HandlerAuthorize("CI-edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Set(int id, bool action) => Json(await service.UpdateDisabledByIdAsync(id, action));

        #endregion 

        #endregion
    }
}