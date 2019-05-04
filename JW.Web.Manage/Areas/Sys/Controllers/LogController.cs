using JW.Core.Data.Base;
using JW.Domain.Sys.Entity;
using JW.Domain.Sys.RequestParam;
using JW.Services.Sys.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.Sys.Controllers
{
    /// <summary>
    /// 系统日志模块控制器
    /// </summary>
    [Area(AreaNames.SysManage)]
    public class LogController : BaseAdminController
    {
        #region Fields

        private readonly ILogService<LogEntity> logService;

        #endregion

        #region Ctor

        public LogController(IWorkContext workContext,
            ILogService<LogEntity> logService)
            : base(workContext)
        {
            this.logService = logService; 
        }

        #endregion

        #region Methods

        [HttpGet]
        [HandlerAuthorize("AD")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> GetGridJson(LogSearchParam param)
        {
            BasePagedListModel<LogEntity> pageDataModel = await logService.GetListAsync(param);
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("AD-del")]
        public async Task<IActionResult> BatchDeleteForm(List<int> ids) => Json(await logService.DeleteByIdsAsync(ids));

        #endregion

    }
}