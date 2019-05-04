using JW.Core.Infrastructure;
using JW.Core.IO;
using JW.Web.Framework.UEditor;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using AS = JW.Services.CMS.Singleton.AttachmentSettingsSingleton;

namespace Cba.Web.Framework.UEditor.Handlers
{
    /// <summary>
    /// 上传文件处理者
    /// </summary>
    public class UploadHandler : Handler
    {
        #region Properties

        public UploadConfig UploadConfig { get; private set; }
        public ResponseResult Result { get; private set; }

        #endregion

        #region Ctor

        public UploadHandler(HttpContext context, 
            UploadConfig config)
            : base(context)
        {
            this.UploadConfig = config;
            this.Result = new ResponseResult() { State = UploadState.Unknown };
        }

        #endregion

        #region Methods

        public override UploadResult Process()
        {
            UploadResult uploadResult;
            try
            {
                var fileProvider = EngineContext.Current.Resolve<ICKFileProvider>();
                var file = Request.Form.Files[UploadConfig.UploadFieldName];
                if (file != null)
                {
                    long fileSize = file.Length;
                    if (fileSize > AS.Singleton.UploadImgMaxSize * 1024)
                    {
                        Result.State = UploadState.SizeLimitExceed;
                    }
                    else
                    {
                        string uploadFileName = file.FileName;
                        //获取文件扩展名
                        string extension = Path.GetExtension(uploadFileName).ToLowerInvariant();
                        if (AS.Singleton.UploadImgExt.Contains(extension.TrimStart('.')))
                        {
                            string path = PathFormatter.Format(AS.Singleton.UploadFilePathRule, "image");
                            string fileName = FileNameFormatter.Format(uploadFileName, AS.Singleton.FileNameRule); 
                            string localPath = AS.Singleton.UploadDir + "\\" + path.Replace("/", "\\");
                            //访问地址
                            string url = $"{AS.Singleton.UploadUrl}/";
                            //若设置为上传至共享目录，否则上传至当前服务目录中
                            if (AS.Singleton.EnabledUploadShare == "false")  
                            {
                                localPath = fileProvider.Combine(Config.WebRootPath, localPath);
                                url += $"{AS.Singleton.UploadDir}/";
                            }
                            //创建目录  
                            fileProvider.CreateDirectory(localPath);
                            //文件全名（包括路径和文件名）
                            string fileFullName = fileProvider.Combine(localPath, fileName);
                            using (FileStream fs = File.Create(fileFullName))
                            {
                                file.CopyTo(fs);
                                fs.Flush();
                            }
                            url += $"{path}/{fileName}";  
                            Result.Url = url;
                            Result.State = UploadState.Success;
                        }
                        else
                        {
                            Result.State = UploadState.TypeNotAllow;
                        }
                    }
                }
                else
                {
                    Result.State = UploadState.NetworkError;
                }
            }
            catch (Exception)
            {
                Result.State = UploadState.FileAccessError;
            }
            finally
            {
                uploadResult = WriteResult();
            }
            return uploadResult;
        }

        private UploadResult WriteResult()
        {
            return new UploadResult
            {
                State = GetStateMessage(Result.State),
                Url = Result.Url,
                Title = Result.OriginFileName,
                Original = Result.OriginFileName,
                Error = Result.ErrorMessage
            };
        }

        private string GetStateMessage(UploadState state)
        {
            switch (state)
            {
                case UploadState.Success:
                    return "SUCCESS";
                case UploadState.FileAccessError:
                    return "文件访问出错，请检查写入权限";
                case UploadState.SizeLimitExceed:
                    return "文件大小超出服务器限制";
                case UploadState.TypeNotAllow:
                    return "不允许的文件格式";
                case UploadState.NetworkError:
                    return "网络错误";
            }
            return "未知错误";
        }

        #endregion
    }

    #region Interior Class
    public class UploadConfig
    {
        /// <summary>
        /// 文件命名规则
        /// </summary>
        public string PathFormat { get; set; }

        /// <summary>
        /// 上传表单域名称
        /// </summary>
        public string UploadFieldName { get; set; }

        /// <summary>
        /// 上传大小限制
        /// </summary>
        public int SizeLimit { get; set; }

        /// <summary>
        /// 上传允许的文件格式
        /// </summary>
        public string[] AllowExtensions { get; set; }

        /// <summary>
        /// 文件是否以 Base64 的形式上传
        /// </summary>
        public bool Base64 { get; set; }

        /// <summary>
        /// Base64 字符串所表示的文件名
        /// </summary>
        public string Base64Filename { get; set; }
    }

    public class ResponseResult
    {
        public UploadState State { get; set; }
        public string Url { get; set; }
        public string OriginFileName { get; set; }

        public string ErrorMessage { get; set; }
    }

    public enum UploadState
    {
        Success = 0,
        SizeLimitExceed = -1,
        TypeNotAllow = -2,
        FileAccessError = -3,
        NetworkError = -4,
        Unknown = 1,
    }

    #endregion

}
