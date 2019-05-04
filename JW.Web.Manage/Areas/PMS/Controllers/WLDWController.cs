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
    /// 往来单位信息
    /// </summary>
    [Area(AreaNames.PMSManage)]
    public class WLDWController : BaseAdminController
    {
        #region Fields

        private readonly IWLDWService<WLDWEntity> service;

        #endregion

        #region Ctor

        public WLDWController(IWorkContext workContext,
            IWLDWService<WLDWEntity> service)
            : base(workContext)
        {
            this.service = service;
        }

        #endregion

        #region Methods

        #region 列表

        [HttpGet]
        [HandlerAuthorize("CB")]
        public IActionResult Index() => View();

        [HttpPost]
        public async Task<IActionResult> GetGridJson(WLDWSearchParam param)
        {
            BasePagedListModel<WLDWEntity> pageDataModel = await service.GetListAsync(param);
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        [HttpGet]
        [HandlerAuthorize("CB-add|CB-edit")]
        public async Task<IActionResult> Form(int id = 0)
        {
            var model = await service.GetModelByIdAsync(id) ?? new WLDWEntity();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("CB-add|CB-edit")]
        public IActionResult SubmitForm(WLDWEntity model) => Json(service.Save(model));

        #endregion

        #endregion
    }
}