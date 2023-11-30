using AbsManagementAPI.Core;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Core.Exceptions;
using AbsManagementAPI.Core.HubSignalR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Reflection;

namespace AbsManagementAPI
{
    public class Startup
    {
        private readonly string AllOrigins = "AllOrigins";
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            var appsetting = Configuration.Get<AppSetting>();
            CurrentOption.AuthenticationString = appsetting.AuthenticationStrings;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCore();
            services.AddSignalR();
            services.AddControllers(options => options.Filters.Add(new ApiExceptionFilter()));

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "AbsManagement", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "AbsManagement API v1"));
            app.UseHttpsRedirection();

            app.UseCors(builder =>
            {
                builder.WithOrigins("http://localhost:3000")
                    .AllowAnyHeader()
                    .WithMethods("GET", "POST")
                    .AllowCredentials();
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<NotifyHub>("/notify");
            });
        }
    }
}
