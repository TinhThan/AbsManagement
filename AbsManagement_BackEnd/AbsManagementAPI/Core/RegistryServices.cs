﻿using AbsManagementAPI.Core.Entities;
using AbsManagementAPI.Validations;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AbsManagementAPI.Core
{
    public static class RegistryServices
    {
        public static IServiceCollection AddCore(this IServiceCollection services)
        {
            services.AddDbContext<DataContext>();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            #region Config automapper

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MapperProfile());
            });

            mappingConfig.AssertConfigurationIsValid();

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);

            #endregion

            return services;
        }
    }
}
