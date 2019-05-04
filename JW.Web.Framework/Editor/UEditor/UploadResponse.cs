namespace JW.Web.Framework.UEditor
{
    /// <summary>
    /// 上传响应
    /// </summary>
    public class UploadResponse
    {
        #region Ctor

        public UploadResponse(string contentType, string result)
        {
            ContentType = contentType;
            Result = result;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 内容类型
        /// </summary>
        public string ContentType { get; set; }
        /// <summary>
        /// 结果
        /// </summary>
        public string Result { get; set; }

        #endregion
    }
}
