using JW.Core.Data.Base;
using JW.Core.Encrypt;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.RequestParam;
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
    /// 友情链接模块控制器
    /// </summary>
    [Area(AreaNames.CMSManage)]
    public class FriendLinksController : BaseAdminController
    {
        #region Fields
         
        private readonly DESEncrypt desEncrypt;
        private readonly IFriendLinksService<FriendLinksEntity> flService; 

        #endregion

        #region Ctor

        public FriendLinksController(IWorkContext workContext,
            DESEncrypt desEncrypt,
            IFriendLinksService<FriendLinksEntity> flService)
            : base(workContext)
        { 
            this.desEncrypt = desEncrypt;
            this.flService = flService; 
        }

        #endregion

        #region Methods

        #region 列表

        [HttpGet]
        [HandlerAuthorize("BH")]
        public IActionResult Index() => View();

        [HttpPost] 
        public async Task<IActionResult> GetGridJson(FriendLinksSearchParam param)
        {
            BasePagedListModel<FriendLinksEntity> pageDataModel = await flService.GetListAsync(param); 
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        [HttpGet]
        [HandlerAuthorize("BH-add|BH-edit")]
        public async Task<IActionResult> Form(int id = 0)
        {
            FriendLinksEntity model = await flService.GetModelByIdAsync(id) ?? new FriendLinksEntity();
            if (id <= 0)
            {
                model.FL_ORDERID = flService.GetMaxOrderId();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("BH-add|BH-edit")]
        public IActionResult SubmitForm(FriendLinksEntity model) => Json(flService.Save(model));

        #endregion

        #region 删除

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("BH-del")]
        public async Task<IActionResult> BatchDeleteForm(List<int> ids) => Json(await flService.DeleteByIdsAsync(ids));

        #endregion

        #region 更新排序
        [HttpPost]
        [HandlerAuthorize("BH-order")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrder(List<int> ids, List<int> orderids) => Json(flService.UpdateOrderId(ids, orderids));

        #endregion

        #endregion

    }
}