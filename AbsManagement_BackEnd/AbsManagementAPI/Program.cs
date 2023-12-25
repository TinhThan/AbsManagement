using AbsManagementAPI;
using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;

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
            if (EF.IsDesignTime)
            {
                new HostBuilder().Build().Run();
                return;
            }

            var builder = BuilderWebHost(args);
            builder.Build().Run();
        }

        static IWebHostBuilder BuilderWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                    .UseStartup<Startup>();
    }
}