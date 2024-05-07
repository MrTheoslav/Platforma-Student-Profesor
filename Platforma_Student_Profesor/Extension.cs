﻿using API.Interfaces;
using API.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using MODEL.DTO;
using MODEL.DTO.Validators;
using MODEL.Models;


namespace Platforma_student_profesor
{
    public static class Extension
    {
        public static IServiceCollection AddInjection (this IServiceCollection services)
        {
            services.AddScoped<IRepositoryService,RepositoryService> ();
            services.AddScoped<IRoleService,RoleService>();
            services.AddScoped<IAccountService,AccountService> ();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator >();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
