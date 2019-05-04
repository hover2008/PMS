using System;
using System.Collections.Generic;
using System.Reflection;

namespace JW.Core.Infrastructure
{
    /// <summary>
    /// 在当前的web应用程序中提供关于类型的信息 
    /// 可选的这个类可以查看bin文件夹中的所有程序集
    /// </summary>
    public class WebAppTypeFinder : AppDomainTypeFinder
    {
        #region Fields

        private bool _ensureBinFolderAssembliesLoaded = true;
        private bool _binFolderAssembliesLoaded;

        #endregion

        #region Ctor

        public WebAppTypeFinder(ICKFileProvider fileProvider = null) : base(fileProvider)
        {
        }

        #endregion

        #region Properties

        /// <summary>
        /// 获取或设置是否在Web应用程序的bin文件夹中的组件应具体检查被装上应用负载。这是需要的情况下，插件需要加载在AppDomain应用被加载后
        /// </summary>
        public bool EnsureBinFolderAssembliesLoaded
        {
            get { return _ensureBinFolderAssembliesLoaded; }
            set { _ensureBinFolderAssembliesLoaded = value; }
        }

        #endregion

        #region Methods

        /// <summary>
        /// 获取\bin目录的物理磁盘路径
        /// </summary>
        /// <returns>物理路径. E.g. "c:\inetpub\wwwroot\bin"</returns>
        public virtual string GetBinDirectory()
        {
            return AppContext.BaseDirectory;
        }

        /// <summary>
        /// 获取程序集
        /// </summary>
        /// <returns>IList<Assembly></returns>
        public override IList<Assembly> GetAssemblies()
        {
            if (this.EnsureBinFolderAssembliesLoaded && !_binFolderAssembliesLoaded)
            {
                _binFolderAssembliesLoaded = true;
                var binPath = GetBinDirectory();
                //binPath = _webHelper.MapPath("~/bin");
                LoadMatchingAssemblies(binPath);
            }

            return base.GetAssemblies();
        }

        #endregion
    }
}
