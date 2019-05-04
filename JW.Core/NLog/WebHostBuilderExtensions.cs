using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;
using System.IO;

namespace JW.Core.NLog
{
    public static class WebHostBuilderExtensions
    {
        /// <summary>
        /// 在Web项目中使用NLog
        /// </summary>
        /// <param name="builder">IWebHostBuilder</param>
        /// <returns></returns>
        public static IWebHostBuilder UseNLogWeb(this IWebHostBuilder builder)
        {
            builder.ConfigureLogging((hostingContext, logging) =>
            {
                logging.ClearProviders();
            }).UseNLog();

            builder.ConfigureAppConfiguration((context, configuration) =>
            {
                var environment = context.HostingEnvironment;
                environment.ConfigureNLog($"{environment.ContentRootPath}{Path.DirectorySeparatorChar}NLog.config");
                LogManager.Configuration.Variables["configDir"] = environment.ContentRootPath;
            });

            return builder;
        }
    }
}
