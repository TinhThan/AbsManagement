using AbsManagementAPI.Core;
using AbsManagementAPI.Core.Constants;
using AbsManagementAPI.Core.Exceptions;
using Microsoft.AspNetCore.Authentication;
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
            services.AddControllers(options => options.Filters.Add(new ApiExceptionFilter()));

            services.AddEndpointsApiExplorer();

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo { Title = "SakilaAPI", Version = "v1" });
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                s.IncludeXmlComments(xmlPath);
            });

            services.AddCors(options =>
            {
                options.AddPolicy(AllOrigins,
                    builder =>
                    {
                        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                    });
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(s => s.SwaggerEndpoint("/swagger/v1/swagger.json", "Sakila API v1"));
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors("AllOrigins");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
