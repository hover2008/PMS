using JW.Core.NLog;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace JW.Web.Manage
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = WebHost.CreateDefaultBuilder(args)
                            .UseUrls("http://*:8001")
                            .UseKestrel(options => options.AddServerHeader = false)
                            .UseNLogWeb()
                            .UseStartup<Startup>()
                            .Build();

            host.Run(); 
        }
    }
}
