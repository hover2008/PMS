using System;
using System.Runtime.Serialization;

namespace JW.Core
{
    /// <summary>
    /// 框架自定义异常信息
    /// </summary>
    [Serializable]
    public class CKException : Exception
    {
        #region Ctor

        /// <summary>
        /// 初始化异常类的新实例
        /// </summary>
        public CKException()
        {
        }

        /// <summary>
        /// 用指定的错误消息初始化异常类的新实例
        /// </summary>
        /// <param name="message">错误消息</param>
        public CKException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// 用指定的错误消息初始化异常类的新实例
        /// </summary>
		/// <param name="messageFormat">异常消息格式</param>
		/// <param name="args">异常消息参数</param>
        public CKException(string messageFormat, params object[] args)
			: base(string.Format(messageFormat, args))
		{
		}

        /// <summary>
        /// 用序列化的数据初始化异常类的新实例
        /// </summary>
        /// <param name="info">包含序列化的对象数据的序列化信息，关于抛出的异常</param>
        /// <param name="context">包含关于源或目的地的上下文信息的流上下文</param>
        protected CKException(SerializationInfo
            info, StreamingContext context)
            : base(info, context)
        {
        }

        /// <summary>
        /// 用指定的错误消息初始化异常类的新实例，并引用内部异常，这是该异常的原因
        /// </summary>
        /// <param name="message">错误消息解释了异常的原因</param>
        /// <param name="innerException">唯一的例外是当前异常的原因，或者是null引用，如果没有内部异常，则是当前异常的原因，或者没有指定内部异常的null引用</param>
        public CKException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        #endregion
    }
}
