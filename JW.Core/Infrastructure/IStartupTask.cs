namespace JW.Core.Infrastructure
{
    /// <summary>
    /// 启动时应执行的接口
    /// </summary>
    public interface IStartupTask 
    {
        /// <summary>
        /// 执行任务
        /// </summary>
        void Execute();

        /// <summary>
        /// 获取此启动任务执行的顺序
        /// </summary>
        int Order { get; }
    }
}
