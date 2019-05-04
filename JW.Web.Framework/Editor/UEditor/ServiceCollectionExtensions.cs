using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace JW.Web.Framework.UEditor
{
    /// <summary>
    /// 服务扩展方法
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddUEditor(this IServiceCollection services, string configFileRelativePath = "ueditor.json", bool isCacheConfig = true, string basePath = "")
        {
            Config.ConfigFile = configFileRelativePath;
            Config.NoCache = isCacheConfig;
            Config.ContentRootPath = basePath;
            services.TryAddSingleton<UploadService>();
            return services;
        }
    }
}
