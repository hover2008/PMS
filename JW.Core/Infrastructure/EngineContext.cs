using System.Runtime.CompilerServices;

namespace JW.Core.Infrastructure
{
    /// <summary>
    /// 提供对引擎单实例的访问
    /// </summary>
    public class EngineContext
    {
        #region Methods

        /// <summary>
        /// 创建引擎的静态实例
        /// </summary>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Create()
        { 
            if (Singleton<IEngine>.Instance == null)
                Singleton<IEngine>.Instance = new CKEngine();

            return Singleton<IEngine>.Instance;
        }

        /// <summary>
        /// 将静态引擎实例设置为所提供的引擎。使用此方法来提供自己的引擎实现
        /// </summary>
        /// <param name="engine">引擎</param>
        /// <remarks></remarks>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取用于访问服务的单例引擎
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Create();
                }

                return Singleton<IEngine>.Instance; }
        }

        #endregion
    }
}
