using API.Interfaces;
using API.Services;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;


namespace Platforma_student_profesor
{
    public static class Extension
    {
        public static IServiceCollection AddInjection (this IServiceCollection services)
        {
            services.AddScoped<IRepositoryService,RepositoryService> ();
            services.AddScoped<IRoleService,RoleService>();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            return services;
        }
    }
}
