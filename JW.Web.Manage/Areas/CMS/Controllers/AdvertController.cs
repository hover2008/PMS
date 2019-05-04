using JW.Core.Data.Base;
using JW.Core.Encrypt;
using JW.Core.Extensions;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.RequestParam;
using JW.Services.CMS.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.CMS.Controllers
{
    /// <summary>
    /// 广告模块控制器
    /// </summary>
    [Area(AreaNames.CMSManage)]
    public class AdvertController : BaseAdminController
    {
        #region Fields

        private readonly DESEncrypt desEncrypt;
        private readonly IAdvertService<AdvertEntity> advService;
        private readonly IDictionaryService<DictionaryEntity> dicService;

        private StringBuilder navHtml = new StringBuilder();

        #endregion

        #region Ctor

        public AdvertController(IWorkContext workContext,
            DESEncrypt desEncrypt,
            IAdvertService<AdvertEntity> advService, 
            IDictionaryService<DictionaryEntity> dicService)
            : base(workContext)
        { 
            this.desEncrypt = desEncrypt;
            this.advService = advService;
            this.dicService = dicService;
        }

        #endregion

        #region Methods

        #region 列表

        [HttpGet]
        [HandlerAuthorize("BI")]
        public async Task<IActionResult> IFrame()
        {
            List<DictionaryEntity> list = (await dicService.GetSubsetListByIdAsync(10005)).ToList(); 
            List<DictionaryEntity> entitys = list.FindAll(t => t.D_PARENTID == 10005);
            int i = 0;
            foreach (var item in entitys)
            {
                if (item.D_CHILDREN > 0)
                {
                    navHtml.AppendFormat("<li{0}>", i > 0 ? " class=\"closed\"" : "");
                    navHtml.AppendFormat("<span class=\"folder\">{0}</span>", item.D_NAME);
                    ResolveSubTree(list, item.D_ID);
                }
                else
                {
                    navHtml.Append("<li>");
                    navHtml.AppendFormat("<a href=\"/cms/advert/index/?tid={0}\" target=\"myFrameName\"><span class=\"file\">{1}</span></a>", item.D_ID, item.D_NAME);
                }
                navHtml.Append("</li>");
                i++;
            }
            ViewBag.MenuNav = navHtml.ToString();
            return View();
        }

        /// <summary>
        /// 生成菜单树
        /// </summary> 
        /// <param name="source">数据元</param> 
        /// <param name="pid">父IP</param> 
        [NonAction]
        private void ResolveSubTree(List<DictionaryEntity> data, int pid)
        {
            List<DictionaryEntity> entitys = data.FindAll(t => t.D_PARENTID == pid);
            navHtml.Append("<ul>");
            int i = 0;
            foreach (var item in entitys)
            {
                if (item.D_CHILDREN > 0)
                {
                    navHtml.AppendFormat("<li{0}>", i > 0 ? " class=\"closed\"" : "");
                    navHtml.AppendFormat("<span class=\"folder\">{0}</span>", item.D_NAME);
                    ResolveSubTree(data, item.D_ID);
                }
                else
                {
                    navHtml.Append("<li>");
                    navHtml.AppendFormat("<a href=\"/cms/advert/index/?tid={0}\" target=\"myFrameName\"><span class=\"file\">{1}</span></a>", item.D_ID, item.D_NAME);
                }
                navHtml.Append("</li>");
                i++;
            }
            navHtml.Append("</ul>");
        }

        [HttpGet]
        [HandlerAuthorize("BI")]
        public IActionResult Index(int tid)
        {
            ViewBag.TypeId = tid;
            return View();
        }

        [HttpPost] 
        public async Task<IActionResult> GetGridJson(AdvertSearchParam param)
        {
            BasePagedListModel<AdvertEntity> pageDataModel = await advService.GetListAsync(param); 
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        #endregion

        #region 新增|编辑

        [HttpGet]
        [HandlerAuthorize("BI-add|BI-edit")]
        public async Task<IActionResult> Form(int id, int typeid)
        {
            AdvertEntity model = await advService.GetModelByIdAsync(id) ?? new AdvertEntity();
            if (id <= 0)
            { 
                model.A_TYPEID = typeid; 
                model.A_ORDERID = advService.GetMaxOrderId(typeid); 
            }
            return View(model);
        }

        [HttpPost] 
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("BI-add|BI-edit")]
        public async Task<IActionResult> SubmitForm(AdvertEntity model)
        { 
            var userClaim = await workContext.GetCurrentUserClaim(); 
            return Json(advService.Save(model, userClaim));
        }

        #endregion

        #region 删除

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("BI-del")]
        public async Task<IActionResult> BatchDeleteForm(List<int> ids) => Json(await advService.DeleteByIdsAsync(ids));

        #endregion

        #region 更新排序

        [HttpPost]
        [HandlerAuthorize("BI-order")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrder(List<int> ids, List<int> orderids) => Json(advService.UpdateOrderId(ids, orderids));

        #endregion

        #region 设置

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("BI-order")]
        public async Task<IActionResult> Set(int id, byte state) => Json(await advService.UpdateStateByIdAsync(id, state));

        #endregion

        #endregion

    }
}