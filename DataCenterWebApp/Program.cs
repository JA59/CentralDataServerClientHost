using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using NLog.Web;
using Microsoft.Extensions.Logging;
using System;





namespace iCDataCenterClientHost
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = CreateWebHostBuilder(args.Where(arg => arg != "--console").ToArray());

            var logger = NLog.Web.NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();

            if (StartupConstants.RunMode == RunMode.Service)
            {
                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                var pathToContentRoot = Path.GetDirectoryName(pathToExe);
                builder.UseContentRoot(pathToContentRoot);
            }

            var host = builder.Build();

            if (StartupConstants.RunMode == RunMode.Service)
            {
                host.RunAsCustomService();
            }
            else
            {
                try
                {
                    if (logger != null)
                        logger.Debug("Calling Run");
                    host.Run();
                }
                catch (Exception ex)
                {
                    if (logger != null)
                        logger.Error(ex, "Stopped program because of exception");
                }
                finally
                {
                    // Ensure to flush and stop internal timers/threads before application-exit (Avoid segmentation fault on Linux)
                    if (logger != null)
                        NLog.LogManager.Shutdown();
                }
            }
        }

        /*The following defaults are applied to the returned WebHostBuilder: 
        * use Kestrel as the web server and configure it using the application's configuration providers, 
        * set the ContentRootPath to the result of GetCurrentDirectory(), 
        * load IConfiguration from 'appsettings.json' and 'appsettings.[EnvironmentName].json', 
        * load IConfiguration from User Secrets when EnvironmentName is 'Development' using the entry assembly, 
        * load IConfiguration from environment variables, load IConfiguration from supplied command line args, 
        * configures the ILoggerFactory to log to the console and debug output, 
        * enables IIS integration, 
        * enables the ability for frameworks to bind their options to their default configuration sections.
        * */
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace);
                })
                .UseNLog();  

    }
}
