using JW.Core.Encrypt;
using JW.Core.ResponseResult;
using JW.Domain.CMS.Enum;
using JW.Domain.CMS.Settings;
using JW.Services.CMS.IService;
using JW.Web.Framework;
using JW.Web.Manage.Controllers;
using JW.Web.Manage.Infrastructure;
using JW.Web.Manage.Infrastructure.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.DrawingCore;
using System.DrawingCore.Text;

namespace JW.Web.Manage.Areas.CMS.Controllers
{
    /// <summary>
    /// 系统设置控制器
    /// </summary>
    [Area(AreaNames.CMSManage)]
    public class SettingController : BaseAdminController
    {
        #region Fields

        private readonly Messages messages;
        private readonly DESEncrypt desEncrypt;
        private readonly ISettingService settingService; 

        #endregion

        #region Ctor

        public SettingController(IWorkContext workContext,
            Messages messages,
            DESEncrypt desEncrypt,
            ISettingService settingService )
            : base(workContext)
        {
            this.messages = messages;
            this.desEncrypt = desEncrypt;
            this.settingService = settingService; 
        }

        #endregion

        #region 1-站点设置

        [HttpGet]
        [HandlerAuthorize("BA")]
        public IActionResult Site()
        {
            SiteSettings model = new SiteSettings();
            Dictionary<string, string> dic = settingService.GetConfigByGroupId((int)SettingEnum.Site);
            if (dic != null)
            {
                model = new SiteSettings(dic);
            }
            return View(model);
        }

        [HttpPost] 
        [HandlerAuthorize("BA-save")]
        [ValidateAntiForgeryToken]
        public IActionResult Site(SiteSettings model)
        { 
            if (model != null)
            {
                bool result = settingService.SaveSite(model);
                messages.Msg = result ? "保存成功" : "保存失败";
                messages.Success = result;
            }
            else
            {
                messages.Msg = "请填写完整";
            }
            return Json(messages);
        }

        #endregion

        #region 2-上传设置

        /// <summary>
        /// 加载字体
        /// </summary>
        private void LoadFont()
        {
            List<SelectListItem> itemList = new List<SelectListItem>();
            InstalledFontCollection fontList = new InstalledFontCollection();
            foreach (FontFamily family in fontList.Families)
            {
                itemList.Add(new SelectListItem() { Text = family.Name, Value = family.Name });
            }
            ViewData["fontList"] = itemList;
        }

        [HttpGet]
        [HandlerAuthorize("BB")]
        public IActionResult Attachment()
        {
            LoadFont();
            AttachmentSettings model = new AttachmentSettings();
            model.WatermarkTextSize = 12;
            model.WatermarkType = 0;
            model.WatermarkPosition = 9;
            Dictionary<string, string> dic = settingService.GetConfigByGroupId((int)SettingEnum.Attachment);
            if (dic != null)
            {
                model = new AttachmentSettings(dic);
            }
            return View(model);
        }

        [HttpPost]
        [HandlerAuthorize("BB-save")]
        [ValidateAntiForgeryToken]
        public IActionResult Attachment(AttachmentSettings model)
        {
            if (model != null)
            {
                bool result = settingService.SaveAttachment(model);
                messages.Msg = result ? "保存成功" : "保存失败";
                messages.Success = result;
            }
            else
            {
                messages.Msg = "请填写完整";
            }
            return Json(messages);
        }

        [HttpPost]
        public IActionResult GetKey()
        {
            messages.Success = true;
            messages.Msg = Guid.NewGuid().ToString("N");
            return Json(messages);
        }

        #endregion

        #region 3-邮箱设置

        [HttpGet]
        [HandlerAuthorize("BC")]
        public IActionResult Email()
        {
            EmailSettings model = new EmailSettings();
            Dictionary<string, string> dic = settingService.GetConfigByGroupId((int)SettingEnum.Email);
            if (dic != null)
            {
                model = new EmailSettings(dic);
                model.SMTPUserPassword = desEncrypt.Decrypt(model.SMTPUserPassword);
            }
            return View(model);
        }

        [HttpPost] 
        [HandlerAuthorize("BC-save")]
        [ValidateAntiForgeryToken]
        public IActionResult Email(EmailSettings model)
        { 
            if (model != null)
            {
                model.SMTPUserPassword = desEncrypt.Encrypt(model.SMTPUserPassword);
                bool result = settingService.SaveEmail(model);
                messages.Msg = result ? "保存成功" : "保存失败";
                messages.Success = result;
            }
            else
            {
                messages.Msg = "请填写完整";
            }
            return Json(messages);
        }
        #endregion

        #region 4-短信设置

        [HttpGet]
        [HandlerAuthorize("BD")]
        public IActionResult SMS()
        {
            SMSSettings model = new SMSSettings();
            Dictionary<string, string> dic = settingService.GetConfigByGroupId((int)SettingEnum.SMS);
            if (dic != null)
            {
                model = new SMSSettings(dic);
                model.Password = desEncrypt.Decrypt(model.Password);
            }
            return View(model);
        }

        [HttpPost] 
        [HandlerAuthorize("BD-save")]
        [ValidateAntiForgeryToken]
        public IActionResult SMS(SMSSettings model)
        { 
            if (model != null)
            {
                model.Password = desEncrypt.Encrypt(model.Password);
                bool result = settingService.SaveSMS(model);
                messages.Msg = result ? "保存成功" : "保存失败";
                messages.Success = result;
            }
            else
            {
                messages.Msg = "请填写完整";
            }
            return Json(messages);
        }

        #endregion
    }
}