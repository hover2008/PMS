using JW.Domain.CMS.Entity;
using JW.Services.CMS.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JW.Web.Manage.Areas.CMS.Controllers
{
    /// <summary>
    /// 内容管理模块框架控制器
    /// </summary>
    [Area(AreaNames.CMSManage)]
    public class SubstanceController : BaseAdminController
    {
        #region Fields

        private readonly IColumnService<ColumnEntity> columnService; 
        protected StringBuilder navHtml = new StringBuilder();

        #endregion

        #region Ctor

        public SubstanceController(IWorkContext workContext,
            IColumnService<ColumnEntity> columnService)
            :base(workContext)
        {
            this.columnService = columnService; 
        }

        #endregion

        #region Methods

        // GET: Substance
        [HttpGet]
        [HandlerAuthorize("BH")]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Column2ModelEntity> list = await columnService.GetAllColumn2ModelListAsync();
            var data = list.ToList();
            List<Column2ModelEntity> entitys = data.FindAll(t => t.C_PARENTID == 0);
            int i = 0;
            foreach (var item in entitys)
            {
                if (item.C_CHILDREN > 0)
                {
                    navHtml.AppendFormat("<li{0}>", i > 0 ? " class=\"closed\"" : "");
                    navHtml.AppendFormat("<span class=\"folder\">{0}</span>", item.C_NAME);
                    ResolveSubTree(data, item.C_ID);
                }
                else
                {
                    navHtml.Append("<li>");
                    string link = String.Empty;
                    if (item.C_TYPEID == 10002)
                    {
                        if (item.M_ID > 0)
                        {
                            link = item.M_MANAGEURL + "?cid=" + +item.C_ID;
                        }
                    }
                    navHtml.AppendFormat("<a href=\"{0}\" target=\"myFrameName\"><span class=\"file\">{1}</span></a>", link, item.C_NAME);
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
        private void ResolveSubTree(List<Column2ModelEntity> data, int pid)
        {
            IList<Column2ModelEntity> entitys = data.FindAll(t => t.C_PARENTID == pid);
            navHtml.Append("<ul>");
            int i = 0;
            foreach (var item in entitys)
            {
                if (item.C_CHILDREN > 0)
                {
                    navHtml.AppendFormat("<li{0}>", i > 0 ? " class=\"closed\"" : "");
                    navHtml.AppendFormat("<span class=\"folder\">{0}</span>", item.C_NAME);
                    ResolveSubTree(data, item.C_ID);
                }
                else
                {
                    navHtml.Append("<li>");
                    string link = String.Empty;
                    if (item.C_TYPEID == 10002)
                    {
                        if (item.M_ID > 0)
                        {
                            link = item.M_MANAGEURL + "?cid=" + +item.C_ID;
                        }
                    }
                    navHtml.AppendFormat("<a href=\"{0}\" target=\"myFrameName\"><span class=\"file\">{1}</span></a>", link, item.C_NAME);
                }
                navHtml.Append("</li>");
                i++;
            }
            navHtml.Append("</ul>");
        }

        public IActionResult Info()
        {
            return View();
        }

        #endregion
    }
}