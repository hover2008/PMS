using Newtonsoft.Json;
using System.Collections.Generic;

namespace JW.Web.Framework.UEditor
{
    /// <summary>
    /// 上传结果
    /// </summary>
    public class UploadResult
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("list")]
        public IEnumerable<FileList> List { get; set; }

        [JsonProperty("start")]
        public int? Start { get; set; }

        [JsonProperty("size")]
        public int? Size { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("total")]
        public int? Total { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("original")]
        public string Original { get; set; }

        [JsonProperty("error")]
        public string Error { get; set; }
    }

    public class FileList
    {
        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("source")]
        public string Source { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
