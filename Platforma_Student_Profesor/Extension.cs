using API.Authorization;
using API.Interfaces;
using API.Services;
using API.Validators;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using MODEL.DTO;
using MODEL.DTO.Validators;
using MODEL.Models;


namespace Platforma_student_profesor
{
    public static class Extension
    {
        public static IServiceCollection AddInjection(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryService, RepositoryService>();
            services.AddScoped<IAssigmentService, AssigmentService>();
            services.AddScoped<IFileService, FileService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddScoped<IValidator<RegisterUserDto>, RegisterUserDtoValidator>();
            services.AddScoped<IValidator<LoginDto>, LoginUserDtoValidator>();
            services.AddScoped<IAuthorizationHandler, ResourceOperationRequirementHandler>();
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddCors();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
