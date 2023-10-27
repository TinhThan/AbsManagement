using AbsManagementAPI;
using Microsoft.AspNetCore;

namespace AbsManagementAPI
{
    /// <summary>
    /// Program
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// main run application
        /// </summary>
        /// <param name="args"></param>
        public static void Main(string[] args)
        {
            var builder = BuilderWebHost(args);
            builder.Build().Run();
        }

        static IWebHostBuilder BuilderWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>();
    }
}