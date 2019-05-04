using JW.Web.Framework.Strategy;
using JW.Web.Manage.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace JW.Web.Manage.Controllers
{
    /// <summary>
    /// 上传/删除图片控制器
    /// </summary>
    public class UploaderController : BaseAdminController
    {
        #region Fields

        private IUploadStrategy uploadStrategy;

        #endregion

        public UploaderController(IWorkContext workContext,
            IUploadStrategy uploadStrategy)
            : base(workContext)
        {
            this.uploadStrategy = uploadStrategy;
        }

        /// <summary>
        /// 上传图片
        /// </summary>
        /// <param name="key">秘钥</param>
        /// <param name="nodeDir">目录</param>
        /// <returns></returns>
        public ActionResult UploadImg(string key = "", string nodeDir = "")
        {
            return Content(uploadStrategy.UploadImg(Request.Form.Files[0], key, nodeDir));
        }

        /// <summary>
        /// 上传商品图片
        /// </summary>
        /// <param name="key">秘钥</param> 
        /// <returns></returns>
        public ActionResult UploadProductImg(string key = "")
        {
            return Content(uploadStrategy.UploadProductImg(Request.Form.Files[0], key));
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="key">秘钥</param>
        /// <param name="url">加密的URL地址（guid）</param>
        /// <returns></returns>
        public ActionResult DelImg(string key = "", string url = "")
        {
            return Content(uploadStrategy.DelImg(key, url));
        }
    }
}