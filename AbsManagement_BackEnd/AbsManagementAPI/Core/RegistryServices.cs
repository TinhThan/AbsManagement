using AbsManagementAPI.Validations;
using FluentValidation;
using MediatR;
using System.Reflection;

namespace AbsManagementAPI.Core
{
    public static class RegistryServices
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            return services;
        }
    }
}
