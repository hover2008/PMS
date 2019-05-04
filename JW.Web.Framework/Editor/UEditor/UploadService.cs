using JW.Core.Extensions;
using JW.Web.Framework.UEditor.Handlers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;

namespace JW.Web.Framework.UEditor
{
    /// <summary>
    /// 上传服务
    /// </summary>
    public class UploadService
    {
        public UploadService(IHostingEnvironment env)
        {
            if (Config.ContentRootPath.IsNullOrWhiteSpace())
            {
                Config.ContentRootPath = env.ContentRootPath;
            }
            if (Config.WebRootPath.IsNullOrWhiteSpace())
            {
                Config.WebRootPath = env.WebRootPath;
            }
            Config.EnvName = env.EnvironmentName;
        }

        /// <summary>
        /// 上传并返回结果，已处理跨域Jsonp请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public UploadResponse UploadAndGetResponse(HttpContext context)
        { 
             var action = context.Request.Query["action"];
            object result;
            if (ActionConsts.Config.Equals(action, StringComparison.OrdinalIgnoreCase))
            {
                var configHandle = new ConfigHandler();
                result = configHandle.Process();
            }
            else
            {
                var handle = HandelFactory.GetHandler(action, context);
                result = handle.Process();
            }
            string resultJson = JsonConvert.SerializeObject(result, new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            });
            string contentType = "text/plain";
             string jsonpCallback = context.Request.Query["callback"];
            if (!jsonpCallback.IsNullOrWhiteSpace())
            {
                contentType = "application/javascript";
                resultJson = string.Format("{0}({1});", jsonpCallback, resultJson);
                UploadResponse response = new UploadResponse(contentType, resultJson);
                return response;
            }
            else
            {
                UploadResponse response = new UploadResponse(contentType, resultJson);
                return response;
            }
        }

        /// <summary>
        /// 单纯的上传并返回结果，未处理跨域Jsonp请求
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Upload(HttpContext context)
        { 
             var action = context.Request.Query["action"];
            object result;
            if (ActionConsts.Config.Equals(action, StringComparison.OrdinalIgnoreCase))
            {
                result = new ConfigHandler();
            }
            else
            {
                var handle = HandelFactory.GetHandler(action, context);
                result = handle.Process();
            }
            return result;
        }
    }
}