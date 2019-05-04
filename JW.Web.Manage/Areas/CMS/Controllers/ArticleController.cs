using JW.Core.Data.Base;
using JW.Core.Encrypt;
using JW.Core.ResponseResult;
using JW.Domain.CMS.Entity;
using JW.Domain.CMS.RequestParam;
using JW.Services.CMS.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.CMS.Controllers
{
    /// <summary>
    /// 文章模块控制器
    /// </summary>
    [Area(AreaNames.CMSManage)]
    public class ArticleController : BaseAdminController
    {
        #region Fields

        private readonly DESEncrypt desEncrypt;
        private readonly IColumnService<ColumnEntity> columnService;
        private readonly IContentService<ContentEntity> contentService;

        #endregion

        #region Ctor

        public ArticleController(IWorkContext workContext,
            DESEncrypt desEncrypt,
            IColumnService<ColumnEntity> columnService,
            IContentService<ContentEntity> contentService)
            : base(workContext)
        {
            this.desEncrypt = desEncrypt;
            this.columnService = columnService;
            this.contentService = contentService;
        }

        #endregion

        #region Methods

        #region 列表

        [HttpGet]
        [HandlerAuthorize("BH")]
        public async Task<IActionResult> Index(int cid = 0, int steps = -1)
        {
            StringBuilder html = new StringBuilder();
            ColumnEntity model = await columnService.GetModelByIdAsync(cid) ?? new ColumnEntity();
            html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}\">全部</a>", steps == -1 ? " btn-primary" : "", model.C_ID);
            switch (model.C_WORKFLOWID)
            {
                case 1:
                    html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=1\">一审</a>", steps == 1 ? " btn-primary" : "", model.C_ID);
                    break;
                case 2:
                    html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=1\">一审</a>", steps == 1 ? " btn-primary" : "", model.C_ID);
                    html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=2\">二审</a>", steps == 2 ? " btn-primary" : "", model.C_ID);
                    break;
                case 3:
                    html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=1\">一审</a>", steps == 1 ? " btn-primary" : "", model.C_ID);
                    html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=2\">二审</a>", steps == 2 ? " btn-primary" : "", model.C_ID);
                    html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=3\">三审</a>", steps == 3 ? " btn-primary" : "", model.C_ID);
                    break;
                case 4:
                    html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=1\">一审</a>", steps == 1 ? " btn-primary" : "", model.C_ID);
                    html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=2\">二审</a>", steps == 2 ? " btn-primary" : "", model.C_ID);
                    html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=3\">三审</a>", steps == 3 ? " btn-primary" : "", model.C_ID);
                    html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=4\">四审</a>", steps == 4 ? " btn-primary" : "", model.C_ID);
                    break;
            }
            html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=99\">终审通过</a>", steps == 99 ? " btn-primary" : "", model.C_ID);
            if (model.C_WORKFLOWID > 0)
            {
                html.AppendFormat("<a class=\"btn btn-xs{0}\" href=\"/Article/Index?cid={1}&steps=0\">退稿</a>", steps == 0 ? " btn-primary" : "", model.C_ID);
            }

            ViewBag.NavHtml = html.ToString();
            ViewBag.Steps = steps;
            return View(model);
        }

        [HttpPost] 
        public async Task<IActionResult> GetGridJson(ContentSearchParam param)
        {
            BasePagedListModel<Content2StatusNameEntity> pageDataModel = await contentService.GetListAsync(param);
            var data = new
            {
                total = pageDataModel.Total,
                rows = pageDataModel.Data
            };
            return Json(data);
        }

        #endregion

        #region 新增|编辑文章

        [HttpGet]
        [HandlerAuthorize("BH-add|BH-edit")]
        public async Task<IActionResult> Form(int id = 0, int catid = 0, string catName = "")
        {
            ContentEntity model =await contentService.GetModelByIdAsync(id) ?? new ContentEntity();
            if (id <= 0)
            { 
                model.CAT_ID = catid;
                model.C_WEIGHT = contentService.GetMaxOrderId(catid);
            }
            ViewBag.CatName = catName;
            return View(model);
        }

        [HttpPost] 
        [HandlerAuthorize("BH-add|BH-edit")]
        [ValidateAntiForgeryToken] 
        public async Task<IActionResult> SubmitForm(ContentEntity model)
        {
            var userClaim = await workContext.GetCurrentUserClaim();
            Messages messages = contentService.Save(model, userClaim);
            return Json(messages);
        }

        #endregion

        #region 删除

        [HttpPost]
        [ValidateAntiForgeryToken]
        [HandlerAuthorize("BH-del")]
        public async Task<IActionResult> BatchDeleteForm(List<int> ids) => Json(await contentService.DeleteByIdsAsync(ids));

        #endregion

        #region 更新排序

        [HttpPost]
        [HandlerAuthorize("BH-order")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateOrder(List<int> ids, List<int> orderids) => Json(contentService.UpdateOrderId(ids, orderids));

        #endregion

        #region 设置

        [HttpPost]
        [HandlerAuthorize("BH-edit")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> SetByIds(List<int> ids, string action, int steps = 0, int wfid = 0) => Json(await contentService.SetByIdsAsync(ids, action, steps, wfid));

        #endregion

        #endregion

    }
}