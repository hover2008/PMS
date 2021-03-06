﻿using JW.Core.Infrastructure;
using JW.Domain.CMS.Enum;
using JW.Domain.CMS.Settings;
using JW.Services.CMS.IService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;

namespace JW.Services.CMS.Singleton
{
    /// <summary>
    /// 附件配置单实例
    /// </summary>
    public sealed class AttachmentSettingsSingleton : AttachmentSettings
    {
        private static AttachmentSettingsSingleton singleton;
        private static readonly object padlock = new object();
        public static AttachmentSettingsSingleton Singleton
        {
            get
            {
                if (singleton == null)
                {
                    lock (padlock)
                    {
                        if (singleton == null)
                        {
                            singleton = new AttachmentSettingsSingleton();
                        }
                    }
                }
                return singleton;
            }
            set
            {
                singleton = value;
            }
        }
        public AttachmentSettingsSingleton()
        {
            var settingService = EngineContext.Current.Resolve<ISettingService>();
            Dictionary<string, string> dic = settingService.GetConfigByGroupId((int)SettingEnum.Attachment);
            if (dic != null && dic.Count > 0)
            {
                foreach (string key in dic.Keys)
                {
                    string value = dic[key];
                    PropertyInfo property = GetType().GetProperty(key);
                    if (property == null)
                    {
                        continue;
                    }
                    else
                    {
                        property.SetValue(this, Convert.ChangeType(value, property.PropertyType, CultureInfo.CurrentCulture), null);
                    }
                }
            }
        }
    }
}
