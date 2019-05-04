

namespace JW.Core.ResponseResult
{
    public class MessagesDataCode<T> : MessagesCode
    { 
        /// <summary>
        /// 数据
        /// </summary>
        public T Data { get; set; } = default(T);
    }
}
